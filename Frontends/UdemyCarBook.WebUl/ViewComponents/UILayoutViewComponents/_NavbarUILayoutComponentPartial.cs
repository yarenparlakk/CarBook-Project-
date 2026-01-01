//using Microsoft.AspNetCore.Mvc;

//namespace UdemyCarBook.WebUl.ViewComponents.UILayoutViewComponents
//{
//    public class _NavbarUILayoutComponentPartial : ViewComponent
//    {
//        public IViewComponentResult Invoke()
//        {
//            return View();
//        }
//    }
//}


using Microsoft.AspNetCore.Mvc;
using System.Security.Claims; // Claim types için gerekli

namespace UdemyCarBook.WebUl.ViewComponents.UILayoutViewComponents
{
    public class _NavbarUILayoutComponentPartial : ViewComponent
    {
        // Token ve API işlemleri için Invoke'u InvokeAsync'e çevirmek daha sağlıklıdır kanka
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // 1. Kullanıcının giriş yapıp yapmadığını ve token'ını kontrol ediyoruz
            var token = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;

            // 2. Eğer Navbar'da giriş yapanın ismini veya rolünü göstermek istersen buradan çekebilirsin
            var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserName")?.Value;
            var userRole = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role || x.Type == "role")?.Value;

            // ViewBag ile View tarafına taşıyabilirsin kanka, tasarımda lazım olur
            ViewBag.UserName = userName;
            ViewBag.UserRole = userRole;

            // 3. API'den menü çekmeyeceksen şimdilik sadece View'ı dönüyoruz
            // Not: API'ye bağlanacak olursan Banner bileşenindeki gibi HttpClient ekleriz
            return View();
        }
    }
}