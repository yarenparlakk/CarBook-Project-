using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UdemyCarBook.WebUl.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Manger")]
    [Area("Admin")]
    [Route("Admin/AdminDashboard")]
    public class AdminDashboardController : Controller
    {

        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
