var builder = WebApplication.CreateBuilder(args);

// افزودن سرویس‌های MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// فعال‌سازی فایل‌های استاتیک
app.UseStaticFiles();

// تنظیمات مسیریابی
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
