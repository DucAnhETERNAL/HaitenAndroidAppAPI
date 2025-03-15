using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BussinessLayer;  // Assuming you have business logic in this layer
using Repository;  // Assuming you have repository layer for database interaction
using HaitenWebAPI.DTOs;
using HaitenWebAPI.DTOs.ReadingHistoryDT;  // Assuming DTOs are in this namespace

namespace HaitenWebAPI.Controllers.PersonalList
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalListController : ControllerBase
    {
        
    }
}
