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
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace FINANCE.WEBUI.Controllers
{
    [Authorize]
    public class InstallmentLoanContractsController : Controller
    {

        private readonly IInstallmentLoanContractService installmentLoanContractService;
        private readonly IInstallmentLoanTransactionHistoryService installmentLoanTransactionHistoryService;
        public InstallmentLoanContractsController(IInstallmentLoanContractService installmentLoanContractService, IInstallmentLoanTransactionHistoryService installmentLoanTransactionHistoryService)
        {
            this.installmentLoanContractService = installmentLoanContractService;
            this.installmentLoanTransactionHistoryService = installmentLoanTransactionHistoryService;
        }
        public IActionResult Index()
        {
            GetViewBag();
            var Contract = installmentLoanContractService.GetAll();
            foreach(var iterm in Contract)
            {
                DateTime now = DateTime.Now;
                TimeSpan kiho = (now - iterm.ContractSignDate)/iterm.InterestCycle;
                int chuKy = kiho.Days;
                int soNgay = chuKy * iterm.InterestCycle;
                Decimal sum = 0;
                TimeSpan ngay = iterm.Term - iterm.ContractSignDate;
                int ngay1 = ngay.Days;
                iterm.DailyInterest = iterm.Amount / ngay1;
                var historyLoanContract = installmentLoanContractService.GetTransactionHistories(iterm.ContractID);
                foreach(var itermHistory in historyLoanContract)
                {
                    sum += itermHistory.Amount;
                }
                Decimal tienHo = soNgay * iterm.DailyInterest;
                if(tienHo >= sum && tienHo < iterm.Amount)
                {
                    iterm.Status = 0;
                }
                else if (tienHo < sum && sum < iterm.Amount)
                {
                    iterm.Status = 1;
                }
                else if (sum == iterm.Amount)
                {
                    iterm.Status = 3;
                }
                else
                {
                    iterm.Status = 2;
                }
                iterm.Paid = sum;
                iterm.Unpaid = iterm.Amount - sum;
            }         
            return View(Contract);
        }
        /*public IActionResult Create()
        {
            GetViewBag();
            return View();
        }*/
        [HttpPost]
        public IActionResult Create(InstallmentLoanContract InstallmentLoanContract)
        {
            GetViewBag();
            if(InstallmentLoanContract.Term > InstallmentLoanContract.ContractSignDate)
            {
                installmentLoanContractService.Create(InstallmentLoanContract);
                ViewData["CreateSuccess"] = "Add Success";
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["CreateError"] = "Error";
                return RedirectToAction("Index");
            }
        }
        void GetViewBag(int? ID = null)
        {
            var create = installmentLoanContractService.GetAllBorrowers().ToList();
            ViewBag.BorrowerID = new SelectList(create, "BorrowerID", "Name", ID);

        }
        public IActionResult Details(int ID)
        {
            var Contract = installmentLoanContractService.GetByID(ID);
            return View(Contract);

        }
        [HttpGet]
        public IActionResult Delete(int ID)
        {
            var Contract = installmentLoanContractService.GetByID(ID);
            installmentLoanContractService.Delete(Contract);
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult Delete(InstallmentLoanContract InstallmentLoanContract)
        {
            /*var TransactionHistory = installmentLoanContractService.GetTransactionHistories(InstallmentLoanContract.ContractID);
            if (TransactionHistory == null)
            {
                installmentLoanContractService.Delete(InstallmentLoanContract);
                ViewData["DeleteSuccess"] = "Delete Contract";
            }
            else
            {
                ViewData["DeleteFail"] = "Cannot delete this team";
                return RedirectToAction("Index");
            }*/
            installmentLoanContractService.Delete(InstallmentLoanContract);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int ID)
        {
            GetViewBag();
            var Contract = installmentLoanContractService.GetByID(ID);
            return View(Contract);
        }
        [HttpPost]
        public IActionResult Edit(InstallmentLoanContract installmentLoanContract)
        {
            GetViewBag();
            if(installmentLoanContract.Term > installmentLoanContract.ContractSignDate)
            {
                installmentLoanContractService.Edit(installmentLoanContract);
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["EditError"] = "Error";
                return View();
            }
        }

    }
}
