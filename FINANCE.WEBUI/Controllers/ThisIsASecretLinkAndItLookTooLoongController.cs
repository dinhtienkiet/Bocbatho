using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FINANCE.WEBUI.Controllers
{
    public class ThisIsASecretLinkAndItLookTooLoongController : Controller
    {
        public IActionResult DoNotOpenThis()
        {
            return View();
        }
    }
}