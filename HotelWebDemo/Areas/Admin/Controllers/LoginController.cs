using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using HotelWebDemo.Models.Users;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class LoginController : AdminController
{
    private readonly IAdminAuthService adminAuthService;

    public LoginController(IAdminAuthService adminAuthService)
    {
        this.adminAuthService = adminAuthService;
    }

    [AllowAnonymous]
    public IActionResult Index(string? returnUrl = null)
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            return Redirect("/Admin");
        }
        ViewData["ReturnUrl"] = returnUrl;

        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Auth(LoginCredentials credentials, string? returnUrl = null)
    {
        AdminUser? user = adminAuthService.Authenticate(credentials);

        if (user != null)
        {
            await AttemptAuthenticate(user);

            if (returnUrl != null)
            {
                return Redirect(WebUtility.UrlDecode(returnUrl));
            }

            return Redirect("/Admin");
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("AdminCookie");

        return RedirectToAction("Index");
    }

    private async Task AttemptAuthenticate(AdminUser user)
    {
        ClaimsPrincipal principal = adminAuthService.CreateClaimsPrincipal(user);
        await HttpContext.SignInAsync("AdminCookie", principal);
    }
}
