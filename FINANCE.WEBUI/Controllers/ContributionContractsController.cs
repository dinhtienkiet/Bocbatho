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
    public class ContributionContractsController : Controller
    {
        private readonly FINANCEEntities _context;
        public readonly IContributionContractService ContributionContract_Service;
        public ContributionContractsController(IContributionContractService CService)
        {
            ContributionContract_Service = CService;
        }
        // GET: ContributionContracts
        public IActionResult Index()
        {
            var list = ContributionContract_Service.GetAllContributionContract();
            return View(list);
        }

        // GET: ContributionContracts/Details/5
        public IActionResult Details(int id)
        {
            var contributionContract = ContributionContract_Service.GetContributionContractByContractID(id);
            return View(contributionContract);
        }

        // GET: ContributionContracts/Create
        /* public IActionResult Create(int ID)
         {
             var contributor = ContributionContract_Service.GetContributionContractByContractID(ID);
             return View();
         }

         [HttpPost]
         public async Task<IActionResult> Create([Bind("ContributorID,ContractID,Amount,InterestRate,ContractSignDate,ExpireDate,InterestCycle,ThisTermInterest,NotReceivedInterest,Status,Note")] ContributionContract contributionContract)
         {
             ContributionContract_Service.CreateContributionContract(contributionContract);
             return RedirectToAction("Index");
         }*/
        public IActionResult Create()
        {
            TempData["CreateSuccess"] = "Add Success";
            GetViewBag();
            return View();
        }
        [HttpPost]
        public IActionResult Create(ContributionContract contract)
        {
            try
            {
                GetViewBag();
                ContributionContract_Service.CreateContributionContract(contract);
                TempData["CreateSuccess"] = "Add Success";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["CreateFail"] = e.Message;
                return RedirectToAction("Index");
            }

        }
        void GetViewBag(int? ID = null)
        {
            var create = ContributionContract_Service.GetAllContributor().ToList();
            ViewBag.ContributorID = new SelectList(create, "ContributorID", "Name", ID);

        }

        // GET: ContributionContracts/Edit/5
        public IActionResult Edit(int id)
        {
            var contract = ContributionContract_Service.GetContributionContractByContractID(id);
            return View(contract);
        }

        [HttpPost]
        public IActionResult Edit(ContributionContract contributionContract)
        {
            ContributionContract_Service.EditContributionContract(contributionContract);
            return RedirectToAction("Index");
        }

        // GET: ContributionContracts/Delete/5
        public IActionResult Delete(int id)
        {
            var contract = ContributionContract_Service.GetContributionContractByContractID(id);
            return View(contract);
        }

        // POST: ContributionContracts/Delete/5
        [HttpPost]
        public IActionResult Delete(ContributionContract contract)
        {
            ContributionContract_Service.DeleteContributionContract(contract);
            return RedirectToAction("Index");
        }
    }
}
