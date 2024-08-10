using Microsoft.AspNetCore.Mvc;

namespace HotelWebDemo.Areas.Admin.Controllers;

public class HomeController : AdminController
{
    [Route("/Admin")]
    [Route("/Admin/Home")]
    public IActionResult Index()
    {
        return View();
    }
}
