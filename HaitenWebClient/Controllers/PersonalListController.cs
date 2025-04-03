using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using HaitenWebAPI.DTOs.UserMangaList;

namespace HaitenMVC.Controllers
{
    public class PersonalListController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }

    }
}
