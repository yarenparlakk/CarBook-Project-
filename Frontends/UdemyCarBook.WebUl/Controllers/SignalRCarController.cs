using Microsoft.AspNetCore.Mvc;

namespace UdemyCarBook.WebUl.Controllers
{
    public class SignalRCarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
