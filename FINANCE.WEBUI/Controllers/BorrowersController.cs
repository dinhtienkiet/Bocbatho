using Microsoft.AspNetCore.Mvc;
using FINANCE.SERVICE.Core;
using FINANCE.CORE.Models;
using Microsoft.AspNetCore.Authorization;

namespace FINANCE.WEBUI.Controllers
{
    [Authorize]
    public class BorrowersController : Controller
    {
        private readonly IBorrowerService borrowerService;
        public BorrowersController(IBorrowerService _borrowerService)
        {
            this.borrowerService = _borrowerService;
        }
        // GET: Borrower
        public IActionResult Index()
        {
            var borrower = borrowerService.GetAllUsers();
            return View(borrower);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Borrower borrower)
        {
            borrowerService.CreateBorrower(borrower);
            return RedirectToAction("Index");
        }

        /*public IActionResult Delete(int ID)
        {
            var borrower = BorrowerService.GetBorrowerByBorrowerID(ID);
            return View(borrower);
        }*/

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var borrower = borrowerService.GetBorrowerByBorrowerID(id);
            borrowerService.Delete(borrower);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int ID)
        {
            var borrower = borrowerService.GetBorrowerByBorrowerID(ID);
            return View(borrower);
        }
        [HttpPost]
        public IActionResult Edit(Borrower borrower)
        {
            borrowerService.Edit(borrower);
            return RedirectToAction("Index");
        }
        public IActionResult Details(int ID)
        {
            var borrower = borrowerService.GetBorrowerByBorrowerID(ID);
            return View(borrower);
        }
    }
}