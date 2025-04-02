using HaitenWebClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HaitenWebClient.Controllers
{
    public class Manga : Controller
    {
        public IActionResult MangaDetails()
        {
            return View();
        }
        public IActionResult ReaddingMangaText()
        {
            return View();
        }
        public IActionResult ListMangaImage()
        {
            return View();
        }

    }
}
