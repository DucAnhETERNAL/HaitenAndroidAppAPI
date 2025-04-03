using AutoMapper;
using BussinessLayer;
using HaitenWebAPI.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Net.payOS;
using Repository;

namespace HaitenWebAPI.Controllers.Payment
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly PayOS _payOS;
        private readonly IMapper _mapper;

        public PaymentController(
            PayOS payOS,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _payOS = payOS;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePaymentLink([FromBody] PaymentRequestDTO paymentRequestDTO)
        {
            try
            {
                var user = await _userRepository.GetById(paymentRequestDTO.Id);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                // Kiểm tra nếu đơn hàng đã bị huỷ và tạo lại đơn mới
                if (user.PaymentStatus == "No" || user.PaymentStatus == null)
                {
                    await _userRepository.UpdatePaymentStatus(paymentRequestDTO.Id, "Pending", 0); // Đặt lại trạng thái là 'Pending'
                }

                var fixedAmount = 10000;  // Số tiền cố định

                // Tạo dữ liệu thanh toán cho PayOS
                var baseUrl = "https://localhost:7016/";

                var paymentData = new PaymentData(
                    user.Id,
                    fixedAmount,
                    $"Thanh toán #{user.Id.GetHashCode()}",
                    new List<ItemData> { new ItemData("Payment", 1, fixedAmount) },
                    $"{baseUrl}api/payment/cancel",  // Đường dẫn hủy
                    $"{baseUrl}api/payment/success?userId={user.Id}&amount={fixedAmount}"  // Đường dẫn thành công
                );

                var paymentLink = await _payOS.createPaymentLink(paymentData);
                return Ok(new { checkoutUrl = paymentLink.checkoutUrl });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating payment link: {ex.Message}");
            }
        }


        [HttpGet("success")]
        public async Task<IActionResult> PaymentSuccess([FromQuery] int userId)
        {
            try
            {
                var user = await _userRepository.GetById(userId);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var fixedAmount = 10000;

                var updateUserDTO = new UpdateUserDTO
                {
                    PaymentStatus = "Yes",
                    AmountPaid = fixedAmount
                };

                var updateUser = _mapper.Map<UpdateUserDTO, User>(updateUserDTO);

                await _userRepository.UpdatePaymentStatus(userId, "Yes", fixedAmount);

                return Redirect("https://localhost:7245/");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error processing payment success: {ex.Message}");
            }
        }

        [HttpGet("cancel")]
        public async Task<IActionResult> PaymentCancel([FromQuery] int userId)
        {
            try
            {
                // Lấy thông tin người dùng từ userId
                var user = await _userRepository.GetById(userId);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                // Kiểm tra trạng thái thanh toán của người dùng, nếu thanh toán chưa được thực hiện
                if (user.PaymentStatus == "Yes" || user.PaymentStatus == "No" || user.PaymentStatus == null)
                {
                    // Thực hiện huỷ thanh toán và cập nhật trạng thái lại là 'Pending'
                    await _userRepository.UpdatePaymentStatus(userId, "Pending", 0);
                    return Ok(new { message = "Thanh toán đã bị hủy và đơn hàng đã được đặt lại." });
                }
                else
                {
                    return BadRequest("Không thể hủy thanh toán vì trạng thái thanh toán chưa hoàn tất.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error processing cancel payment: {ex.Message}");
            }
        }


    }
}
