using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace SelfHelperRE.Controllers
{
    public class HomeController : Controller
    {

        [Authorize]
        public IActionResult Index() => View();

        [Authorize]
        public IActionResult Diary()
        {

            DateTime date = DateTime.Now;
            ViewBag.Date = DateTime.Now.ToString("yyyy-MM-dd");

            return View();
        }

        [Authorize]
        public IActionResult Note() => View();

        [Authorize]
        public IActionResult Target() => View();

        [Authorize]
        public IActionResult Board() => View();

    }
}
