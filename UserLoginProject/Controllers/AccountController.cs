using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

public class AccountController : Controller
{
    // ذخیره کاربران در حافظه
    private static List<User> users = new List<User>();
    private static User currentUser;

    // صفحه ورود
    public IActionResult Login()
    {
        if (currentUser != null)
        {
            return RedirectToAction("Profile");
        }

        return View();
    }

    // عملیات ورود
    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);
        if (user != null)
        {
            currentUser = user;
            return RedirectToAction("Profile");
        }
        else
        {
            ViewBag.ErrorMessage = "نام کاربری یا رمز عبور اشتباه است.";
            return View();
        }
    }

    // صفحه ثبت نام
    public IActionResult Register()
    {
        return View();
    }

    // عملیات ثبت نام
    [HttpPost]
    public IActionResult Register(string username, string password, string fullName, string name, string email, string phoneNumber)
    {
        // بررسی اینکه کاربر قبلاً ثبت نام نکرده باشد
        if (users.Any(u => u.Username == username))
        {
            ViewBag.ErrorMessage = "کاربر با این نام کاربری وجود دارد.";
            return View();
        }

        // افزودن کاربر جدید
        var newUser = new User 
        {
            Username = username,
            Password = password,
            FullName = fullName,
            Name = name,
            Email = email,
            PhoneNumber = phoneNumber
        };

        users.Add(newUser);

        // ست کردن کاربر فعلی و هدایت به پروفایل
        currentUser = newUser;
        return RedirectToAction("Profile");
    }

    // صفحه پروفایل
    public IActionResult Profile()
    {
        if (currentUser == null)
        {
            return RedirectToAction("Login");
        }

        return View(currentUser);
    }

    // عملیات خروج
    public IActionResult Logout()
    {
        currentUser = null;
        return RedirectToAction("Login");
    }
}
