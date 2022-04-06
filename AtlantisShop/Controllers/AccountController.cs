using AtlantisShop.Core.Senders.Email;
using AtlantisShop.Core.Services.Interfaces;
using AtlantisShop.Core.ViewModels;
using AtlantisShop.DataLayer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace AtlantisShop.Web.Controllers
{
   public class AccountController : Controller
   {
	  private IUserService US;
	  public AccountController(IUserService _US)
	  {
		 US = _US;
	  }

	  #region Register
	  [Route("Register")]
	  public IActionResult Register()
	  {
		 return View();
	  }

	  [HttpPost]
	  [ValidateAntiForgeryToken]
	  [Route("Register")]
	  public IActionResult Register(RegisterViewModel model)
	  {
		 if (!ModelState.IsValid)
		 {
			return View(model);
		 }

		 if (US.IsExistMobile(model.Mobile))
		 {
			ModelState.AddModelError("Mobile", "موبایل وارد شده تکراری است");
			return View(model);
		 }
		 if (US.IsExistEmail(model.Email))
		 {
			ModelState.AddModelError("Email", "ایمیل وارد شده تکراری است");
			return View(model);
		 }

		 if (US.RegisterUser(model))
		 {
			return RedirectToAction("Login", new { Message = "حساب کاربری شما ایجاد شد لطفا برای فعالسازی ایمیل خود را بررسی کنید" });
		 }

		 ViewBag.Message = "ثبت با خطا مواجه شد";
		 return View(model);
	  }
	  #endregion

	  #region Login
	  [Route("Login")]

	  public IActionResult Login(string Message,string ReturnUrl)
	  {
		 if(User.Identity.IsAuthenticated)
		 {
			return Redirect("/UserPanel");
		 }
		 ViewBag.ReturnUrl = ReturnUrl;
		 ViewBag.Message = Message;
		 return View();
	  }

	  [HttpPost]
	  [ValidateAntiForgeryToken]
	  [Route("Login")]
	  public IActionResult Login(LoginViewModel model, string ReturnUrl)
	  {
		 if (!ModelState.IsValid)
		 {
			return View(model);
		 }

		 Users user = US.Login(model.Username, model.Password);

		 if (user == null)
		 {
			ViewBag.Message = "کاربر با این مشخصات یافت نشد!";
			return View();
		 }

		 var claim = new List<Claim>()
		 {
			new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
			new Claim(ClaimTypes.Name,user.FirstName + " " + user.LastName),
			new Claim(ClaimTypes.Email,user.Email)
		 };
		 var Identity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
		 var Principal = new ClaimsPrincipal(Identity);
		 var properties = new AuthenticationProperties
		 {
			IsPersistent = model.RememberMe,
		 };
		 HttpContext.SignInAsync(Principal, properties);

		 if(!string.IsNullOrEmpty(ReturnUrl))
		 {
			return Redirect(ReturnUrl);
		 }

		 else
		 {
			return Redirect("/UserPanel");
		 }	
	  }


	  #endregion

	  #region Activate User
	  public IActionResult ActiveUser(string Id)
	  {
		 if (string.IsNullOrEmpty(Id))
		 {
			return RedirectToAction("Login", new { Message = "کاربر با این مشخصات یافت نشد" });
		 }

		 if (US.ActiveUser(Id) == true)
		 {
			return RedirectToAction("Login", new { Message = "حساب کاربری شما با موفقیت فعال شد" });
		 }
		 else
		 {
			return RedirectToAction("Login", new { Message = "حساب کاربری با این مشخصات یافت نشد و یا از قبل فعال شده است" });
		 }

	  }
	  #endregion

	  #region Logout
	  [Route("Logout")]
	  [Authorize]
	  public IActionResult Logout()
	  {
		 HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		 return RedirectToAction("Login", new { Message = "شما با موفقیت از حساب خود خارج شدید" });
	  }
	  #endregion

	  #region Forget Password
	  [Route("ForgetPassword")]
	  public IActionResult ForgetPassword(string Message)
	  {
		 ViewBag.Message = Message;
		 return View();
	  }
	  [Route("ForgetPassword")]
	  [ValidateAntiForgeryToken]
	  [HttpPost]
	  public IActionResult ForgetPassword(ForgetPasswordViewModel model)
	  {
		 if (!ModelState.IsValid)
		 {
			 ViewBag.Message = "لطفا ورودی های خود را بررسی کنید";
			return View(model);
		 }
		 Users user = US.GetUserByEmail(model.Email);
		 if (user == null)
		 {
			ViewBag.Message = "کاربر با این ایمیل یافت نشد";
			return View(model);
		 }

		 //===============================Email ===============================//
		 string Body = "<p>کاربر گرامی" + " " + user.FirstName + " " + user.LastName + "<br>" +
			   "برای بازیابی کلمه عبور خود روی لینک زیر کلیک کنید" + "<br>" +
			   "<a href='https://localhost:44319/Account/ResetPassword/" + user.ActivateCode + "' >" +
			   "بازیابی رمز عبور" + "</a>" + "</p>";

		 SendEmail.Send("بازیابی رمز عبور", Body, user.Email);
		 //====================================================================//


		 ViewBag.Message = "برای بازیابی رمز عبور به ایمیل خود مراجعه کنید";
		 return View();


		 

	  }
	 
	  public IActionResult ResetPassword(string id)
	  {
		 if (string.IsNullOrEmpty(id))
		 {
			return RedirectToAction("ForgetPassword", new { Message = "کد وارد شده صحیح نیست" });
		 }
			

		 Users user = US.GetUserByActivateCode(id);
		 if(user == null)
		 {
			return RedirectToAction("ForgetPassword", new { Message = "کاربر با این مشحصات یافت نشد" });
		 }

		 ViewBag.ActivateCode = id;
		 return View();


	  }

	  [HttpPost]
	  [ValidateAntiForgeryToken]
	  public IActionResult ResetPassword(ResetPassswordViewModel model)
	  {
		 if(!ModelState.IsValid)
		 {
			return View(model);
		 }
		 if(US.ResetPassword(model))
		 {
			return RedirectToAction("Login", new { Message = "کلمه عبور شما با موفقیت تغییر یافت" });
	     }
		 ViewBag.Message = "خطایی رخ داده است.";
		 return View(model);

	  }
		  #endregion
   }
}
