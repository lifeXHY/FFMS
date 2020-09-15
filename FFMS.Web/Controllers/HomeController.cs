using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FFMS.Application.Bill;
using FFMS.Application.Bill.Dto;
using FFMS.EntityFrameWorkCore.Entitys;
using FFMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FFMS.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAccountBillService _accountBillService;
        public HomeController(IAccountBillService accountBillService)
        {
            _accountBillService = accountBillService;
        }

        
        public IActionResult Index()
        {
            //if(_session.)
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public string GetEchartsData()
        {
            SearchAccountBillDto search = new SearchAccountBillDto()
            {
                UserID = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Sid)),
                BegDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
            };

            var lst = _accountBillService.GetAllBillsQuery(search).ToList();
            string json = string.Empty;
            if (lst.Count() == 0)
            {
                AccountBill entity = new AccountBill()
                {
                    AccountMoney = 0
                };
                lst.Add(entity);
            }
            return JsonConvert.SerializeObject(lst, Formatting.Indented);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
