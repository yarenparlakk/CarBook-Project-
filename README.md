# 🚗 Udemy CarBook - Araç Kiralama Projesi

Bu proje, Udemy üzerinden takip edilen kurs kapsamında geliştirilmiş, uçtan uca bir araç kiralama platformunun backend ve frontend süreçlerini kapsamaktadır. Projede modern yazılım mimarileri ve tasarım desenleri kullanılarak sürdürülebilir bir yapı hedeflenmiştir.

## 🚀 Kullanılan Teknolojiler ve Mimari

### Backend
- **Framework:** .NET 8.0 Web API
- **Mimari:** Onion Architecture (Core, Infrastructure, Presentation, Persistence)
- **Tasarım Desenleri:** CQRS (Command Query Responsibility Segregation), Mediator Pattern (MediatR)
- **Veri Erişimi:** Entity Framework Core, Dapper (Performans gerektiren yerlerde)
- **Güvenlik:** JWT (JSON Web Token) Authentication & Authorization
- **Veritabanı:** MSSQL

### Frontend
- **Framework:** ASP.NET Core MVC (WebUI)
- **UI Kit:** Bootstrap & Özel Admin Teması
- **Veri İletişimi:** HttpClient ile API tüketimi

## 🌟 Kurs Dışı Özgün Geliştirmelerim

Eğitim serisindeki standart yapıya ek olarak, projeyi daha profesyonel ve gerçek hayat senaryolarına uygun hale getirmek için şu geliştirmeleri kendim kurguladım:

- **Gelişmiş Rol Bazlı Yetkilendirme (RBAC):** Sadece tek bir Admin tipi yerine, sisteme `Manager` rolü entegre edildi.
- **Dinamik Erişim Kısıtlama:** Manager rolündeki kullanıcıların Admin paneline erişimi sağlandı ancak bu kullanıcılara panel içerisinde belirli alanlarda (Silme/Güncelleme gibi kritik işlemler) kısıtlamalar getirildi.
- **Hata Yönetimi ve Kullanıcı Deneyimi:** API'den veri dönmediğinde veya yetkisiz erişim denendiğinde (401 Unauthorized/404 Not Found), kullanıcının boş bir sayfa yerine anlamlı bilgilendirme mesajları görmesi için View tarafında özel kontroller (If-Else blokları ve PartialView yönetimleri) geliştirildi.
- **Esnek Token Yönetimi:** Ana sayfa gibi herkese açık alanlarda, token bulunmasa bile uygulamanın çökmeden çalışmaya devam etmesi için asenkron InvokeAsync süreçleri optimize edildi.

## ✨ Öne Çıkan Özellikler
- **Dinamik Dashboard:** Araç sayıları, lokasyonlar ve anlık istatistikler.
- **Araç Yönetimi:** Marka, model ve özellik bazlı araç listeleme ve filtreleme.
- **JWT Yetkilendirme:** Admin ve Manager rollerine göre kısıtlanmış panel erişimi.
- **API Entegrasyonu:** Tamamen ayrıştırılmış API ve WebUI katmanları.

## 🛠️ Kurulum ve Çalıştırma

1. Bu repoyu bilgisayarınıza indirin (clone).
2. `UdemyCarBook.sln` dosyasını Visual Studio ile açın.
3. API projesindeki `appsettings.json` dosyasında yer alan **Connection String** bilgisini kendi yerel MSSQL sunucunuza göre düzenleyin.
4. Package Manager Console üzerinden `Update-Database` komutunu çalıştırarak tabloları oluşturun.
5. Visual Studio'da **Solution Explorer** üzerinde sağ tıklayarak **Set Startup Projects** seçeneğinden hem `WebApi` hem de `WebUI` projelerini aynı anda "Start" olarak ayarlayın.

---
✅ **Proje Durumu:** Bu çalışma başarıyla tamamlanmış ve planlanan tüm fonksiyonel özellikler (Backend & Frontend) entegre edilmiştir. Udemy CarBook eğitim serisindeki kazanımlar, özgün yetkilendirme ve rol yönetimi geliştirmeleriyle birleştirilerek nihai haline getirilmiştir.
