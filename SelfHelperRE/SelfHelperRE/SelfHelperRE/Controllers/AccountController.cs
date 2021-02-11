using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using SelfHelperRE.Models;
using SelfHelperRE.ViewModels;
using Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SelfHelperRE.Controllers
{
    public class AccountController : Controller
    {
        UserService<RegisterModel> userServiceRegistration;
        UserService<LoginModel> userServiceLogin;
        UserService<CheckEmail> userServiceEmail;

        public AccountController()
        {
            userServiceLogin = new UserService<LoginModel>();
            userServiceRegistration = new UserService<RegisterModel>();
            userServiceEmail = new UserService<CheckEmail>();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {

            if (ModelState.IsValid)
            {
                if (await userServiceLogin.CheckUser(model, "login") == true)
                {
                    await Authenticate(model.Login); 

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {

            if (ModelState.IsValid)
            {

                if (await userServiceRegistration.CheckUser(model, "registration") == false)
                {

                    await userServiceRegistration.AddUser(model);

                    await Authenticate(model.Login); 

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Такой логин и (или) почта уже существуют");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Recovery()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Recovery(CheckEmail model)
        {
            userServiceEmail = new UserService<CheckEmail>();
            if (ModelState.IsValid)
            {
                if (await userServiceEmail.CheckUser(model, "email") == true)
                {
                    UserService<UserData> userServiceData = new UserService<UserData>();
                    UserData user = new UserData();
                    user.Email = model.Email;
                    user = await userServiceData.GetData(user);
                    var emailMessage = new MimeMessage();

                    emailMessage.From.Add(new MailboxAddress("Администрация сайта", "self.helper@mail.ru"));
                    emailMessage.To.Add(new MailboxAddress("", model.Email));
                    emailMessage.Subject = "Данные авторизации вашего аккаунта";
                    emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                    {
                        Text = "<h2>Данны вашего аккаунта: </h2>" +
                        "<p>Логин: " + user.Login + "</p>"+
                        "<p>Пароль: " + user.Password + "</p>" +
                        "<h5>В целях безопасности после прочтения удалите письмо</h5>"
                    };

                    using (var client = new SmtpClient())
                    {
                        await client.ConnectAsync("smtp.mail.ru", 465, true);
                        await client.AuthenticateAsync("self.helper@mail.ru", "12#QWEasd");
                        await client.SendAsync(emailMessage);

                        await client.DisconnectAsync(true);
                    }

                    return RedirectToAction("Login", "Account");
                }
                else
                    ModelState.AddModelError("", "Такой почты не зарегестрированно");
            }
            return View(model);
        }
    }
}
