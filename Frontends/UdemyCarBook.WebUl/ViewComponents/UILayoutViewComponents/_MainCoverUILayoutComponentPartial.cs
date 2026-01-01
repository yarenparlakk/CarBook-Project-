using Microsoft.AspNetCore.Mvc;

namespace UdemyCarBook.WebUl.ViewComponents.UILayoutViewComponents
{
    public class _MainCoverUILayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();  
        }
    }
}
