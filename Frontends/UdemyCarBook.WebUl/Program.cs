using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Container'a servisleri ekle.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

// Kimlik doğrulama ayarları
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie
    (JwtBearerDefaults.AuthenticationScheme, opt =>
    {
        opt.LoginPath = "/Login/Index";
        opt.LogoutPath = "/Login/LogOut";

        // --- KANKA KRİTİK DÜZELTME BURASI ---
        // Manager veya yetkisiz biri girince artık 404 vermeyecek, ana sayfaya atacak.
        opt.AccessDeniedPath = "/Default/Index/";

        opt.Cookie.SameSite = SameSiteMode.Strict;
        opt.Cookie.HttpOnly = true;
        opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        opt.Cookie.Name = "CarBookJwt";
        opt.ExpireTimeSpan = TimeSpan.FromMinutes(2); // İsteğe göre süreyi uzatabilirsin kanka
        opt.SlidingExpiration = true; // Oturum aktifse süreyi otomatik uzatır
    });

var app = builder.Build();

// HTTP istek boru hattını yapılandır.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Önce kimlik kontrolü
app.UseAuthorization();  // Sonra yetki kontrolü

// Route yapılandırması
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}"); // Başlangıcı Default yaptık kanka

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}" // Dashboard odaklı area
    );
});

app.Run();