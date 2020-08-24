using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FFMS.Application.Items;
using FFMS.Application.Items.Dto;
using FFMS.EntityFrameWorkCore.Entitys;
using FFMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Wei.Repository;

namespace FFMS.Web.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private readonly IBasIemsService _basIemsService;
        private readonly IMapper _mapper;
        private readonly IRepository<BasUser> _userRepository;
        public ItemsController(IBasIemsService basIemsService, IMapper mapper, IRepository<BasUser> userRepository)
        {
            _basIemsService = basIemsService;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region 加载收支项目列表
        public async Task<IActionResult> GetItemsList(int? page, int? limit,string searchInput)
        {
            SearchItemsDto search = string.IsNullOrWhiteSpace(searchInput) ? new SearchItemsDto() : JsonConvert.DeserializeObject<SearchItemsDto>(searchInput);
            var lst = await _basIemsService.GetAllItemsQuery(search).ToPagedListAsync(page ?? 0, limit ?? int.MaxValue);

            var result = new ReturnViewModel<BasItems>()
            {
                count = lst.Total,
                data = lst.Items
            };
            return Json(result);
        }
        #endregion


        public async Task<IActionResult> Create(int Id)
        {
            UpdateItemsDto model = new UpdateItemsDto();
            if (Id > 0)
            {
                var item = await _basIemsService.GetItemsEntity(Convert.ToInt32(Id));
                model = _mapper.Map(item, model);
                ViewBag.Id = item.Id;
                ViewBag.ItemType = item.ItemType;
            }
            else
            {
                ViewBag.Id = 0;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewItem(BasItemsDto input)
        {
            var user = await _userRepository.FirstOrDefaultAsync(x => x.DisPlayName == User.Identity.Name);
            input.CreateUserID = user.Id;
            input.CreateDisPlayName = user.DisPlayName;
            var model = await _basIemsService.Create(input);
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateItem(UpdateItemsDto input)
        {
            int UserID = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Sid));
            input.CreateDisPlayName = (await _userRepository.FirstOrDefaultAsync(x => x.Id == UserID)).DisPlayName;
            input.CreateUserID = UserID;
            var model = await _basIemsService.Update(input);
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteItem(int Id)
        {
            var model = await _basIemsService.Delete(Id);
            return Json(model);
        }
    }
}
