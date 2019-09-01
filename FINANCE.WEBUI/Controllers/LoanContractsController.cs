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

namespace FINANCE.WEBUI.Controllers
{
    public class LoanContractsController : Controller
    {
        private readonly ILoanContractService loanContractService;

        public LoanContractsController(ILoanContractService loanContractService)
        {
            this.loanContractService = loanContractService;
        }
        public IActionResult Index()
        {
            GetViewBag();
            var Contract = loanContractService.GetAll();
            return View(Contract);
        }

        public IActionResult Create()
        {
            GetViewBag();
            return View();
        }
        [HttpPost]
        public IActionResult Create(LoanContract loanContract)
        {
            GetViewBag();
            if(loanContract.ExpireDate > loanContract.ContractSignDate)
            {
                loanContractService.Create(loanContract);
                ViewData["CreateSuccuss"] = "Tao moi thanh cong";
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["CreateFail"] = "Create Fail";
                return View();
            }
        }
        void GetViewBag(int? ID = null)
        {
            var create = loanContractService.GetAllBorrowers().ToList();
            ViewBag.BorrowerID = new SelectList(create, "BorrowerID", "Name", ID);

        }
        public IActionResult Details(int ID)
        {
            var Contract = loanContractService.GetByID(ID);
            return View(Contract);

        }
        [HttpGet]
        public IActionResult Delete(int ID)
        {
            var Contract = loanContractService.GetByID(ID);
            loanContractService.Delete(Contract);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Edit(int ID)
        {
            GetViewBag();
            var Contract = loanContractService.GetByID(ID);
            return View(Contract);
        }
        [HttpPost]
        public IActionResult Edit(LoanContract loanContract)
        {
            GetViewBag();
            if(loanContract.ExpireDate > loanContract.ContractSignDate)
            {
                loanContractService.Edit(loanContract);
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["EditError"] = "Ngay het han phai lon hon ngay tao hop dong";
                return View();
            }
        }
    }
}
