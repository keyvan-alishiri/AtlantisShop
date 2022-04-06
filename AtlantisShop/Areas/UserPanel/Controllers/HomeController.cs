using AtlantisShop.Core.Generators;
using AtlantisShop.Core.Security;
using AtlantisShop.Core.Senders.Email;
using AtlantisShop.Core.Services.Interfaces;
using AtlantisShop.Core.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace AtlantisShop.Web.Areas.UserPanel.Controllers
{
   [Area("UserPanel")]
   [Authorize]
   public class HomeController : Controller
   {
      private IUserService US;
      private IWalletService WS;
      public HomeController(IUserService _US, IWalletService _WS)
      {
         WS = _WS;
         US = _US;
      }
      public IActionResult Index()
      {
         ViewBag.UserInfo = US.GetUserInfo(User.Identity.GetEmail());
         
         return View();
      }

      public IActionResult Edit()
      {
         EditUserViewModel model = US.GetUserDataForEdit(User.Identity.GetEmail());
         return View(model);
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public IActionResult Edit(EditUserViewModel model)
      {
         if (!ModelState.IsValid)
         {
            ViewBag.Message = "لطفا ورودی های خود را بررسی کنید";
            ViewBag.Class = "alert alert-warning";
            return View(model);
         }

         if (model.UserProfile != null)
         {
            if (model.UserProfile.Length / 1024 > 1024)
            {
               ModelState.AddModelError("UserProfile", "حجم فایل انتخابی بیشتر از 1 مگابایت است");
               return View(model);
            }

            string extention = Path.GetExtension(model.UserProfile.FileName);

            if (ExtensionChecker.ImageChecker(extention) == false)
            {
               ModelState.AddModelError("UserProfile", "فقط فرمت های jpg,png,jpeg مجاز هستند");
               return View(model);
            }
         }

         if (US.SaveUserEdit(User.Identity.GetEmail(), model))
         {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/Login?Message=اطلاعات شما با موفقیت تغییر یافت، لطفا جهت ادامه کار مجدد وارد شوید");

         }

         ViewBag.Message = "خطایی رخ داد لطفا مجدد تلاش کنید";
         ViewBag.Class = "alert alert-warning";
         return View(model);
      }


      #region Change Password


      public IActionResult ChangePassword()
      {
         return View();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public IActionResult ChangePassword(ChangePasswordViewModel model)
      {
         if (!ModelState.IsValid)
         {
            ViewBag.Message = "لطفا ورودی های خود را بررسی کنید";
            ViewBag.Class = "alert alert-warning";
            return View(model);
         }

         if (US.CheckOldPassword(User.Identity.GetEmail(), model.OldPassword) == false)

         {
            ModelState.AddModelError("OldPassword", "کلمه عبور فعلی صحیح نیست");
            return View(model);
         }

         if (US.ChangePassword(User.Identity.GetEmail(), model.Password))
         {
            ViewBag.Message = "کلمه عبور با موفقیت تغییر یافت";
            ViewBag.Class = "alert alert-success";

            //------------------------------SendEmail -----------------------------------//
            string body = "کلمه عبور شما در تاریخ" + GeneratorDateTime.GetNowShamsiDate() + "با موفقیت تغییر یافت" +
               "<hr>" + "کلمه عبور جدید" + model.Password;
            SendEmail.Send("تغییر کلمه عبور ", body, User.Identity.GetEmail());
            //---------------------------------------------------------------------------//

            return View();
         }
         else
         {
            ViewBag.Message = "خطایی رخ داد لطفا مجدد تلاش کنید";
            ViewBag.Class = "alert alert-danger";
            return View(model);
         }



      }



      #endregion

      #region Wallet
      public IActionResult Wallet(int pageId = 1, int take=5)
	  {
         ViewBag.WalletAmount = WS.GetUserWalletAmount(User.Identity.GetEmail());
         ViewBag.Take=take;
         return View(WS.GetUserWallets(User.Identity.GetEmail(), pageId, take));
	  }
      #endregion
   }



}
