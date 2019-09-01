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
    public class InstallmentLoanTransactionHistoriesController : Controller
    {
        private readonly IInstallmentLoanTransactionHistoryService installmentLoanTransactionHistoryService;

        public InstallmentLoanTransactionHistoriesController(IInstallmentLoanTransactionHistoryService installmentLoanTransactionHistoryService)
        {
            this.installmentLoanTransactionHistoryService = installmentLoanTransactionHistoryService;
        }

        void GetViewBagUser(int? ID = null)
        {
            var create = installmentLoanTransactionHistoryService.GetAllUser().ToList();
            ViewBag.UserID = new SelectList(create, "UserID", "Username", ID);
        }
        void GetViewBagInstallmentLoanContract(int? ID = null)
        {
            var create = installmentLoanTransactionHistoryService.GetAllInstallmentLoanContract().ToList();
            ViewBag.InstallmentLoanContractID = new SelectList(create, "ContractID", "ContractID", ID);
        }

        // GET: TransactionHistories
        public IActionResult Index()
        {
            var transact = installmentLoanTransactionHistoryService.GetAllInstallmentLoanTransactionHistories();
            return View(transact);
        }

        // GET: TransactionHistories/Details/5
        public IActionResult Details(int ID)
        {
            var transact = installmentLoanTransactionHistoryService.GetInstallmentLoanTransactionHistoryByContractID(ID);
            return View(transact);
        }

        // GET: TransactionHistories/Create
        public IActionResult Create()
        {
            GetViewBagUser();
            GetViewBagInstallmentLoanContract();
            return View();
        }

        // POST: TransactionHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InstallmentLoanTransactionHistory installmentLoanTransactionHistory)
        {
            installmentLoanTransactionHistoryService.Create(installmentLoanTransactionHistory);
            return RedirectToAction("Index");
        }

        // GET: TransactionHistories/Edit/5
        public IActionResult Edit(int ID)
        {
            GetViewBagUser();
            GetViewBagInstallmentLoanContract();
            var transact = installmentLoanTransactionHistoryService.GetInstallmentLoanTransactionHistoryByContractID(ID);
            return View(transact);
        }

        // POST: TransactionHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(InstallmentLoanTransactionHistory installmentLoanTransactionHistory)
        {
            installmentLoanTransactionHistoryService.Edit(installmentLoanTransactionHistory);
            return RedirectToAction("Index");
        }

        // GET: TransactionHistories/Delete/5
        public IActionResult Delete(int ID)
        {
            var transact = installmentLoanTransactionHistoryService.GetInstallmentLoanTransactionHistoryByContractID(ID);
            return View(transact);
        }

        // POST: TransactionHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(InstallmentLoanTransactionHistory installmentLoanTransactionHistory)
        {
            installmentLoanTransactionHistoryService.Delete(installmentLoanTransactionHistory);
            return RedirectToAction("Index");
        }
    }
}
