using System.Collections.Generic;
using System.Threading.Tasks;
using FFMS.Application.Report;
using FFMS.Application.Report.Dto;
using FFMS.EntityFrameWorkCore.Entitys;
using FFMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Wei.Repository;

namespace FFMS.Web.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IRepository<BasUser> _userRepository;
        public ReportController(IReportService reportService, IRepository<BasUser> userRepository)
        {
            _reportService = reportService;
            _userRepository = userRepository;
        }
        #region 加载家庭成员收支明细列表
        public async Task<IActionResult> Details()
        {
            var User = await _userRepository.GetAllAsync(x => !x.IfAdmin);
            List<SelectListItem> UserList = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "" }
            };
            foreach (var item in User)
            {
                UserList.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.DisPlayName });
            }
            ViewBag.UserList = UserList;
            return View();
        }

        public async Task<IActionResult> GetAllUserBillsList(int? page, int? limit, string searchInput)
        {
            SearchReport_DetailsDto search = string.IsNullOrWhiteSpace(searchInput) ? new SearchReport_DetailsDto() : JsonConvert.DeserializeObject<SearchReport_DetailsDto>(searchInput);
            var lst = await _reportService.GetDetailsViewData(search).ToPagedListAsync(page ?? 0, limit ?? int.MaxValue);
            
            var result = new ReturnViewModel<AccountBill>()
            {
                count = lst.Total,
                data = lst.Items
            };
            return Json(result);
        }
        #endregion
    }
}
