using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FFMS.Application.User;
using FFMS.Application.User.Dto;
using FFMS.EntityFrameWorkCore.Entitys;
using FFMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wei.Repository;

namespace FFMS.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRepository<BasUser> _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IRepository<BasUser> userRepository, IMapper mapper)
        {
            _userService = userService;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region 加载用户列表
        [HttpGet]
        public async Task<IActionResult> GetUserListAsync(int? page, int? limit)
        {
            var lst = await _userRepository.Query().ToPagedListAsync(page ?? 0, limit ?? int.MaxValue);

            var result = new ReturnViewModel<BasUser>()
            {
                count = lst.Total,
                data = lst.Items
            };
            return Json(result);
        }
        #endregion

        public async Task<IActionResult> Create(string Id)
        {
            BasUserDto model = new BasUserDto();
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var user = await _userRepository.GetAsync(Convert.ToInt32(Id));
                model = _mapper.Map(user, model);
                ViewBag.Id = user.Id;
            }
            else
            {
                ViewBag.Id = 0;
                model.IsActive = true;
            }
            return View(model);
        }

        #region 管理员账号修改用户信息（不包括密码）
        [HttpPost]
        public async Task<IActionResult> UpdateUserData(UpdateBasUserDto input)
        {
            var model = await _userService.UpdateUser(input);
            return Json(model);
        }
        #endregion

        #region 新增用户
        
        [HttpPost]
        public async Task<IActionResult> CreateNewUser(BasUserDto input)
        {
            var model = await _userService.CreateNewUser(input);
            return Json(model);
        }
        #endregion

        #region 用户更新密码

        public IActionResult OpenChangeUserPasswordView()
        {
            return View("ChangePassword", new ChangePasswordDto());
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserPassword(ChangePasswordDto input)
        {
            input.Id  = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Sid));
            var model = await _userService.ChangeUserPassword(input);
            return Json(model);
        }
        #endregion

    }
}
