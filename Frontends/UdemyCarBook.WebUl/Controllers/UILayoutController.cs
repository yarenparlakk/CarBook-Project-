using Microsoft.AspNetCore.Mvc;

namespace UdemyCarBook.WebUl.Controllers
{
    public class UILayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
