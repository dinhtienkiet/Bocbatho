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
    public class ContributorsController : Controller
    {
        public readonly IContributoService contributoService;
        public ContributorsController(IContributoService contributoService)
        {
            this.contributoService = contributoService;
        }
        /* public IActionResult Index()
         {
             var contributor = contributoService.GetContributors();

             return View(contributor);
         }*/
        [HttpGet]
        public IActionResult Index(string text)
        {
            var contributor = contributoService.SearchContributor(text);
            return View(contributor);
        }
        /* public IActionResult Delete(int ID)
         {
             var contributor = contributoService.GetContributorByID(ID);
             return View(contributor);
         }*/
        /* public IActionResult Delete(int ContributorID)
         {
             var contributor = contributoService.GetContributorByID(ContributorID);
             return View();
             //var ContribuotrTest = contributoService.DeleteContributor(contributor.ContributorID);           
             if (contributor != null)
             {
                 ViewBag.Message = "không thể xóa phần tử này!!!";                      
                 return RedirectToAction("Edit");
             }
             ViewBag.Message = "Xóa thành công !!!";
             return RedirectToAction("Index");
         }*/
        public IActionResult Delete(int ID)
        {
            var ContribuotrTest = contributoService.DeleteContributor(ID);
            if (ContribuotrTest == null)
            {
                ViewData["inforSuccess"] = "Xóa thành công !!!";
                return RedirectToAction("Index");
            }
            ViewData["inforFail"] = "không thể xóa phần tử này!!!";
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int ID)
        {
            var contributor = contributoService.GetContributorByID(ID);
            return View(contributor);
        }
        [HttpPost]
        public IActionResult Edit(Contributor contributor)
        {
            contributoService.EditContributor(contributor);
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Contributor contributor)
        {
            contributoService.CreateContributor(contributor);
            return RedirectToAction("Index");
        }
        public IActionResult Details(int ID)
        {
            var ContributionContracts = contributoService.GetContributionContracts(ID);
            return View(ContributionContracts);
        }
    }
}
