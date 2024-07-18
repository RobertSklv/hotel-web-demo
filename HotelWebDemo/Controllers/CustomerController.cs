using HotelWebDemo.Models.ViewModels;
using HotelWebDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebDemo.Controllers;

public class CustomerController : Controller
{
    private readonly ICustomerService service;

    public CustomerController(ICustomerService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IActionResult ResetPassword(int userId, string token)
    {
        if (service.CompareResetPasswordToken(userId, token, ModelState))
        {
            ResetPasswordModel model = new()
            {
                UserId = userId,
                Token = token
            };

            return View(model);
        }

        return Redirect("/");
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
    {
        if (ModelState.IsValid)
        {
            bool success = await service.ResetPassword(model, ModelState);

            if (success)
            {
                return Redirect("/");
            }
        }

        return View(model);
    }
}
