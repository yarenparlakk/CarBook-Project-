using Microsoft.AspNetCore.Mvc;

namespace UdemyCarBook.WebUl.ViewComponents.BlogViewComponents
{
    public class _BlogDetailsParagraphComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
