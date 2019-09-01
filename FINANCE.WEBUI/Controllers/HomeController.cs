using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FINANCE.WEBUI.Models;
using Microsoft.AspNetCore.Authorization;
using FINANCE.SERVICE.Core;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace FINANCE.WEBUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService userService;
        public HomeController(IUserService userService)
        {
            this.userService = userService;
        }
        public IActionResult Users()
        {
            ViewBag.Message = "Users";
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var ID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var UserName = User.Identity.Name;
            var UserTest = userService.CheckDatabase(Convert.ToInt32(ID),UserName);
            if(UserTest == null)
            {
                await HttpContext.SignOutAsync();
                return RedirectToAction("Login", "Users");
            }
            ViewBag.Message = "Index";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
