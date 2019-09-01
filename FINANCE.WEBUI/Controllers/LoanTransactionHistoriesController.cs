using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FINANCE.CORE.Models;
using FINANCE.INFRA.Entities;
using FINANCE.SERVICE.Core;
using Microsoft.AspNetCore.Authorization;

namespace FINANCE.WEBUI.Controllers
{
    [Authorize]
    public class LoanTransactionHistoriesController : Controller
    {
        private readonly ILoanTransactionHistoryService loanTransactionHistoryService;

        public LoanTransactionHistoriesController(ILoanTransactionHistoryService loanTransactionHistoryService)
        {
            this.loanTransactionHistoryService = loanTransactionHistoryService;
        }
        void GetViewBagUser(int? ID = null)
        {
            var create = loanTransactionHistoryService.GetAllUser().ToList();
            ViewBag.UserID = new SelectList(create, "UserID", "Username", ID);
        }
        void GetViewBagLoanContract(int? ID = null)
        {
            var create = loanTransactionHistoryService.GetAllLoanContract().ToList();
            ViewBag.LoanContractID = new SelectList(create, "ContractID", "ContractID", ID);
        }

        // GET: TransactionHistories
        public IActionResult Index()
        {
            var transact = loanTransactionHistoryService.GetAllLoanTransactionHistories();
            return View(transact);
        }

        // GET: TransactionHistories/Details/5
        public IActionResult Details(int ID)
        {
            var transact = loanTransactionHistoryService.GetLoanTransactionHistoryByContractID(ID);
            return View(transact);
        }

        // GET: TransactionHistories/Create
        public IActionResult Create()
        {
            GetViewBagUser();
            GetViewBagLoanContract();
            return View();
        }

        // POST: TransactionHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LoanTransactionHistory loanTransactionHistory)
        {
            loanTransactionHistoryService.Create(loanTransactionHistory);
            return RedirectToAction("Index");
        }

        // GET: TransactionHistories/Edit/5
        public IActionResult Edit(int ID)
        {
            var transact = loanTransactionHistoryService.GetLoanTransactionHistoryByContractID(ID);
            GetViewBagUser();
            GetViewBagLoanContract();
            return View(transact);
        }

        // POST: TransactionHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(LoanTransactionHistory loanTransactionHistory)
        {
            loanTransactionHistoryService.Edit(loanTransactionHistory);
            return RedirectToAction("Index");
        }

        // GET: TransactionHistories/Delete/5
        public IActionResult Delete(int ID)
        {
            var transact = loanTransactionHistoryService.GetLoanTransactionHistoryByContractID(ID);
            loanTransactionHistoryService.Delete(transact);
            return RedirectToAction("Index");
        }
    }
}
