using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UdemyCarBook.Dto.LoginDtos;
using UdemyCarBook.WebUl.Models;

namespace UdemyCarBook.WebUl.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateLoginDto createLoginDto)
        {
            var client = _httpClientFactory.CreateClient();

            var content = new StringContent(
                JsonSerializer.Serialize(createLoginDto),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("https://localhost:7003/api/Login", content);

            if (!response.IsSuccessStatusCode)
                return View();

            var jsonData = await response.Content.ReadAsStringAsync();
            var tokenModel = JsonSerializer.Deserialize<JwtResponseModel>(jsonData,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            if (tokenModel == null || tokenModel.Token == null)
                return View();

            // JWT token oku
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(tokenModel.Token);

            var claims = jwtToken.Claims.ToList();

            // accessToken ekle
            claims.Add(new Claim("accessToken", tokenModel.Token));

            // Authentication
            var claimsIdentity = new ClaimsIdentity(
                claims,
                JwtBearerDefaults.AuthenticationScheme);

            var authProps = new AuthenticationProperties
            {
                ExpiresUtc = tokenModel.ExpireDate,
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                JwtBearerDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProps);

            // 🔥 ROLE OKUMA (Admin + Manager)
            var userRoles = claims
                .Where(x => x.Type == ClaimTypes.Role
                         || x.Type.EndsWith("/role")
                         || x.Type.Equals("role", StringComparison.OrdinalIgnoreCase)
                         || x.Type.Equals("roles", StringComparison.OrdinalIgnoreCase))
                .SelectMany(x => x.Value.Split(',')) // array/virgül varsa ayır
                .Select(r => r.Trim())
                .ToList();

            if (userRoles.Any(r =>
                    r.Contains("Admin", StringComparison.OrdinalIgnoreCase) ||
                    r.Contains("Manger", StringComparison.OrdinalIgnoreCase)))
            {
                return RedirectToAction("Index", "AdminDashboard", new { area = "Admin" });
            }
            return RedirectToAction("Index", "Default");

        }

        public async Task<IActionResult> LogOut()
        {
            // Kullanıcının JWT bazlı oturumunu sonlandırır
            await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);

            // Kanka burayı "Index", "Default" yerine kendi bulunduğu sayfa olan "Index", "Login" yapıyoruz
            return RedirectToAction("Index", "Login");
        }
    }
}


