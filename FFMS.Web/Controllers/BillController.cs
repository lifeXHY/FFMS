using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FFMS.Application.Bill;
using FFMS.Application.Bill.Dto;
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
    public class BillController : Controller
    {
        private readonly IAccountBillService _accountBillService;
        private readonly IMapper _mapper;
        private readonly IRepository<BasUser> _userRepository;
        private readonly IRepository<BasItems> _itemsRepository;
        public BillController(IAccountBillService accountBillService, IMapper mapper, IRepository<BasUser> userRepository, IRepository<BasItems> itemsRepository)
        {
            _accountBillService = accountBillService;
            _mapper = mapper;
            _userRepository = userRepository;
            _itemsRepository = itemsRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region 加载账簿列表
        public async Task<IActionResult> GetBillsList(int? page, int? limit, string searchInput)
        {
            SearchAccountBillDto search = string.IsNullOrWhiteSpace(searchInput) ? new SearchAccountBillDto() : JsonConvert.DeserializeObject<SearchAccountBillDto>(searchInput);
            search.UserID = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Sid));
            var lst = await _accountBillService.GetAllBillsQuery(search).ToPagedListAsync(page ?? 0, limit ?? int.MaxValue);

            var result = new ReturnViewModel<AccountBill>()
            {
                count = lst.Total,
                data = lst.Items
            };
            return Json(result);
        }
        #endregion

        public async Task<IActionResult> Create(int Id)
        {
            UpdateAccountBillDto model = new UpdateAccountBillDto();
            IList<SelectListItem> BillTypelst = new List<SelectListItem>
            {
                new SelectListItem { Value = BillTypeEnum.Income.ToString(), Text = "收入" },
                new SelectListItem { Value = BillTypeEnum.Expense.ToString(), Text = "支出" }
            };

            var items = _itemsRepository.GetAll();
            List<SelectListItem> ItmsTypelst = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "" }
            };
            foreach (var item in items)
            {
                ItmsTypelst.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.ItemType });
            }
            if (Id > 0)
            {
                var item = await _accountBillService.GetBillEntity(Convert.ToInt32(Id));
                model = _mapper.Map(item, model);
                ViewBag.Id = item.Id;

            }
            else
            {
                ViewBag.Id = 0;
            }
            ViewBag.BillTypelst = BillTypelst;
            ViewBag.ItmsTypelst = ItmsTypelst;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewBill(AccountBillDto input)
        {
            input.CreateUserID = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Sid)); ;
            input.CreateDisPlayName = User.Identity.Name;
            var model = await _accountBillService.Create(input);
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBill(UpdateAccountBillDto input)
        {
            input.CreateUserID = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Sid)); ;
            input.CreateDisPlayName = User.Identity.Name;
            var model = await _accountBillService.Update(input);
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBill(int Id)
        {
            var model = await _accountBillService.Delete(Id);
            return Json(model);
        }
    }
}
