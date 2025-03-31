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
                    // Lấy thông tin người dùng từ repository
                    var user = await _userRepository.GetById(paymentRequestDTO.Id);
                    if (user == null)
                    {
                        return NotFound("User not found.");
                    }

                    // Tạo dữ liệu thanh toán cho PayOS
                    var baseUrl = "https://localhost:7015/";

                    // Tạo dữ liệu thanh toán cho PayOS
                    var paymentData = new PaymentData(
                        user.Id,
                        (int)paymentRequestDTO.Amount,
                        $"Thanh toán cho người dùng {user.Id}",
                        new List<ItemData> { new ItemData("Payment", 1, (int)paymentRequestDTO.Amount) },
                        $"{baseUrl}api/payment/cancel",  // Cập nhật đường dẫn hủy
                        $"{baseUrl}api/payment/success?userId={user.Id}&amount={paymentRequestDTO.Amount}"  // Cập nhật đường dẫn thành công
                    );

                    // Gọi PayOS API để tạo liên kết thanh toán
                    var paymentLink = await _payOS.createPaymentLink(paymentData);
                    Console.WriteLine($"Thanh toán thành công, đường dẫn thanh toán: {paymentLink.checkoutUrl}");

                    // Trả về URL thanh toán cho client
                    return Ok(new { checkoutUrl = paymentLink.checkoutUrl });
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error creating payment link: {ex.Message}");
                }
            }

            [HttpGet("success")]
            public async Task<IActionResult> PaymentSuccess([FromQuery] int userId, [FromQuery] double amount)
            {
                try
                {
                    // Lấy thông tin người dùng từ userId
                    var user = await _userRepository.GetById(userId);
                    if (user == null)
                    {
                        return NotFound("User not found.");
                    }

                    // Cập nhật trạng thái thanh toán của người dùng và số tiền đã thanh toán
                    var updateUserDTO = new UpdateUserDTO
                    {
                        PaymentStatus = "Yes",
                        AmountPaid = (decimal)amount
                    };

                    // Cập nhật người dùng
                    var updateUser = _mapper.Map<UpdateUserDTO, User>(updateUserDTO);
                    await _userRepository.Update(updateUser);

                    return Ok(new { message = $"Thanh toán thành công cho người dùng {userId}", amountPaid = amount });
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error processing payment success: {ex.Message}");
                }
            }

            [HttpGet("cancel")]
            public IActionResult PaymentCancel()
            {
                // Xử lý khi thanh toán bị hủy
                return Ok(new { message = "Thanh toán bị hủy" });
            }
        }
    }
