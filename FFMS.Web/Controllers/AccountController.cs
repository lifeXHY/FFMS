using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using FFMS.Application.Account;
using FFMS.Application.Account.Dto;
using FFMS.EntityFrameWorkCore.Entitys;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace FFMS.Web.Controllers
{
    public class AccountController: Controller
    {
        private readonly IAccountService _accountService;
        private readonly IMemoryCache _memoryCache;

        public AccountController(IAccountService accountService, IMemoryCache memoryCache)
        {
            _accountService = accountService;
            _memoryCache = memoryCache;
        }
        public IActionResult Index()
        {
            return View("login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountDto input)
        {
            var result = await _accountService.Login(input);
            if (result.IfSuccess)
            {
                BasUser entity = result.Model;

                //用Claim来构造一个ClaimsIdentity，然后调用 SignInAsync 方法。
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, entity.DisPlayName),
                    new Claim(ClaimTypes.Sid, entity.Id.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                //登录
                await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.Now.AddHours(1),
                    IsPersistent = false,
                    AllowRefresh = false
                });
            }
            return Json(result);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index");
        }

    }
}
