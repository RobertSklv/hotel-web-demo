using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebDemo.Areas.Admin.Controllers;

[Authorize(AuthenticationSchemes = "AdminCookie")]
[Area("Admin")]
public abstract class AdminController : Controller
{
}
