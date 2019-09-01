using Microsoft.AspNetCore.Mvc;
using FINANCE.SERVICE.Core;
using FINANCE.CORE.Models;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace FINANCE.WEBUI.Controllers
{
    [Authorize]
    public class UsersController : Controller

    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet]
        public IActionResult Edit()
        {
            var ID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string imagePath = "/assets/images/img2.jpg";
            var user = userService.GetUserByUserID(Convert.ToInt32(ID));
            var usertest = userService.Login(user);
            if (usertest.Avatar == null || usertest.Avatar.Equals(""))
            {
                usertest.Avatar = imagePath;
            }
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), usertest.Avatar);
            FileInfo fileInfo = new FileInfo($"../FINANCE.WEBUI/wwwroot{filePath}");
            if (!fileInfo.Exists)
            {
                usertest.Avatar = imagePath;
            }

            return View(usertest);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(User user, IFormFile fileUpload)
        {
            if (fileUpload != null)
            {
                var imageUser = userService.GetUserByUserID(user.UserID);
                string imageUsers = "/assets/images/users/";
                string pathSave = $"../FINANCE.WEBUI/wwwroot{imageUsers}";
                if (!Directory.Exists(pathSave))
                {
                    Directory.CreateDirectory(pathSave);
                }
                string pattern = "/assets/images/users/";
                string oldfileName = imageUser.Avatar;

                if (oldfileName != null)
                {
                    var image = Regex.Replace(oldfileName, pattern, String.Empty);
                    var oldpath = Path.Combine(Directory.GetCurrentDirectory(), pathSave, image);
                    FileInfo fileInfo = new FileInfo(oldpath);
                    if (fileInfo.Exists)
                    {
                        fileInfo.Delete();
                    }
                }
                string[] fileTypes = { ".jpeg", ".jpg", ".png", ".gif" };
                string extFile = System.IO.Path.GetExtension(fileUpload.FileName);
                if (!((IList<string>)fileTypes).Contains(extFile.ToLower()))
                {
                    ModelState.AddModelError("errFileUpload", "Please upload file having extensions .jpeg/.jpg/.png/.gif only.");
                    return View(user);
                }
                string fileName = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".jpg";
                var path = Path.Combine(Directory.GetCurrentDirectory(), pathSave, fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await fileUpload.CopyToAsync(stream);
                }
                user.Avatar = imageUsers + fileName;
                userService.Edit(user);
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                    new Claim(ClaimTypes.Uri,user.Avatar.ToString())
                };
                ClaimsIdentity userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                 userPrincipal, new AuthenticationProperties
                 {
                     ExpiresUtc = DateTime.UtcNow.AddMinutes(90),
                     IsPersistent = false,
                     AllowRefresh = false
                 }
                  );

                return RedirectToAction("Index", "Home");
            }
            else
            {              
                string imagePath = User.FindFirst(ClaimTypes.Uri).Value;
                userService.Edit(user);
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                    new Claim(ClaimTypes.Uri,imagePath)
                };
                ClaimsIdentity userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                 userPrincipal, new AuthenticationProperties
                 {
                     ExpiresUtc = DateTime.UtcNow.AddMinutes(90),
                     IsPersistent = false,
                     AllowRefresh = false
                 }
                  );

                return RedirectToAction("Index", "Home");
            }
           
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            string image = "/assets/images";
            string imagePath = "/assets/images/img2.jpg";
            var usertest = userService.Login(user);
            if (usertest != null)
            {
                var imagesss = usertest.Avatar;
                if (usertest.Avatar == null || usertest.Avatar.Equals(""))
                {
                    usertest.Avatar = imagePath;
                }
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), usertest.Avatar);
                FileInfo fileInfo = new FileInfo($"../FINANCE.WEBUI/wwwroot{filePath}");
                if (!fileInfo.Exists)
                {
                    usertest.Avatar = imagePath;
                }
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, usertest.Username),
                    new Claim(ClaimTypes.NameIdentifier, usertest.UserID.ToString()),
                    new Claim(ClaimTypes.Uri,usertest.Avatar.ToString())
                };
                ClaimsIdentity userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                 userPrincipal, new AuthenticationProperties
                 {
                     ExpiresUtc = DateTime.UtcNow.AddMinutes(90),
                     IsPersistent = false,
                     AllowRefresh = false
                 }
                  );

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["line"] = "ban nhap sai ten tai khoan";
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Users");
        }
    }


}