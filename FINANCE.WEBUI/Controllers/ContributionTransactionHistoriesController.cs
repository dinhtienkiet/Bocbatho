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
    public class ContributionTransactionHistoriesController : Controller
    {
        private readonly IContributionTransactionHistoryService contributionTransactionHistoryService;

        public ContributionTransactionHistoriesController(IContributionTransactionHistoryService contributionTransactionHistoryService)
        {
            this.contributionTransactionHistoryService = contributionTransactionHistoryService;
        }

        void GetViewBagUser(int? ID = null)
        {
            var create = contributionTransactionHistoryService.GetAllUser().ToList();
            ViewBag.UserID = new SelectList(create, "UserID", "Username", ID);
        }
        void GetViewBagContributionContract(int? ID = null)
        {
            var create = contributionTransactionHistoryService.GetAllContributionContract().ToList();
            ViewBag.ContributionContractID = new SelectList(create, "ContractID", "ContractID", ID);
        }

        // GET: TransactionHistories
        public IActionResult Index()
        {
            var transact = contributionTransactionHistoryService.GetAllContributionTransactionHistories();
            return View(transact);
        }

        // GET: TransactionHistories/Details/5
        public IActionResult Details(int ID)
        {
            var transact = contributionTransactionHistoryService.GetContributionTransactionHistoryByContractID(ID);
            return View(transact);
        }

        // GET: TransactionHistories/Create
        public IActionResult Create()
        {
            GetViewBagUser();
            GetViewBagContributionContract();
            return View();
        }

        // POST: TransactionHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ContributionTransactionHistory contributionTransactionHistory)
        {
            contributionTransactionHistoryService.Create(contributionTransactionHistory);
            return RedirectToAction("Index");
        }

        // GET: TransactionHistories/Edit/5
        public IActionResult Edit(int ID)
        {
            GetViewBagUser();
            GetViewBagContributionContract();
            var transact = contributionTransactionHistoryService.GetContributionTransactionHistoryByContractID(ID);
            return View(transact);
        }

        // POST: TransactionHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ContributionTransactionHistory contributionTransactionHistory)
        {
            contributionTransactionHistoryService.Edit(contributionTransactionHistory);
            return RedirectToAction("Index");
        }

        // GET: TransactionHistories/Delete/5
        public IActionResult Delete(int ID)
        {
            var transact = contributionTransactionHistoryService.GetContributionTransactionHistoryByContractID(ID);
            return View(transact);
        }

        // POST: TransactionHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ContributionTransactionHistory contributionTransactionHistory)
        {
            contributionTransactionHistoryService.Delete(contributionTransactionHistory);
            return RedirectToAction("Index");
        }
    }
}
