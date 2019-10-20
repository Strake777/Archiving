using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Archiving.Models;
using Archiving.BLL.Dto;
using Archiving.BLL.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace Archiving.Controllers
{
    public class AccountController : Controller
    {
        private const string DEFAULT_DIRECTORY = "C:\\";
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn(LoginModel loginModel)
        {            
            var accountDto = new AccountDto(loginModel.Login, loginModel.Password);
            if(_accountService.Authorize(accountDto))
            {
                var claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultNameClaimType, loginModel.Login) };
                var claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                _logger.LogInformation("User {0} successfully sign in", loginModel.Login);
                return RedirectToAction("ArchivingFiles", "Archiving", new { directoryName = DEFAULT_DIRECTORY });
            }
            else
            {
                _logger.LogError("Login attempt: invalid login or password");
                return View();
            }            
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel registerModel)
        {
            var accountDto = new AccountDto(registerModel.Login, registerModel.Password);
            if(_accountService.CreateAccount(accountDto))
            {
                _logger.LogInformation("User {0} successfully register", registerModel.Login);
                return RedirectToAction("SignIn", "Account");
            }
            _logger.LogError("Register: user exists");
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _logger.LogError("Successfully Log out");
            return RedirectToAction("SignIn", "Account");
        }
    }
}