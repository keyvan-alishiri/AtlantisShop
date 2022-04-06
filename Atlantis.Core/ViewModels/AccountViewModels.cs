using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AtlantisShop.Core.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name ="نام")]
        [Required(ErrorMessage ="فیلد {0} نمی‌تواند خالی باشد")]
        [MaxLength(250,ErrorMessage ="فیلد {0} حداکثر 250کاراکتر می‌باشد")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "فیلد {0} نمی‌تواند خالی باشد")]
        [MaxLength(250, ErrorMessage = "فیلد {0} حداکثر 250کاراکتر می‌باشد")]
        public string LastName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "فیلد {0} نمی‌تواند خالی باشد")]
        [MaxLength(250, ErrorMessage = "فیلد {0} حداکثر 250کاراکتر می‌باشد")]
        [EmailAddress(ErrorMessage ="ایمیل وارد شده نامعتبر است")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "فیلد {0} نمی‌تواند خالی باشد")]
        [MaxLength(250, ErrorMessage = "فیلد {0} حداکثر 250کاراکتر می‌باشد")]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "فیلد {0} نمی‌تواند خالی باشد")]
        [MaxLength(250, ErrorMessage = "فیلد {0} حداکثر 250کاراکتر می‌باشد")]
        [Compare("Password",ErrorMessage ="تکرار کلمه عبور صحیح نیست")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "فیلد {0} نمی‌تواند خالی باشد")]
        [MaxLength(11, ErrorMessage = "فیلد {0} حداکثر  11 کاراکتر می‌باشد")]
        public string Mobile { get; set; }

        [Display(Name = "تلفن ثابت")]
        [MaxLength(11, ErrorMessage = "فیلد {0} حداکثر  11 کاراکتر می‌باشد")]
        public string Tell { get; set; }

        [Display(Name = "استان")]
        [Required(ErrorMessage = "فیلد {0} نمی‌تواند خالی باشد")]
        [MaxLength(250, ErrorMessage = "فیلد {0} حداکثر  250 کاراکتر می‌باشد")]
        public string State { get; set; }

        [Display(Name = "شهر")]
        [Required(ErrorMessage = "فیلد {0} نمی‌تواند خالی باشد")]
        [MaxLength(250, ErrorMessage = "فیلد {0} حداکثر  250 کاراکتر می‌باشد")]
        public string City { get; set; }

        [Display(Name = "کد پستی")]
        [Required(ErrorMessage = "فیلد {0} نمی‌تواند خالی باشد")]
        [MaxLength(250, ErrorMessage = "فیلد {0} حداکثر  250 کاراکتر می‌باشد")]
        public string PostalCode { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "فیلد {0} نمی‌تواند خالی باشد")]
        [MaxLength(650, ErrorMessage = "فیلد {0} حداکثر  650 کاراکتر می‌باشد")]
        public string Address1 { get; set; }

    }

   public class LoginViewModel
   {
      [Display(Name = "نام کاربری")]
      [Required(ErrorMessage = "فیلد {0} نمی‌تواند خالی باشد ")]
      public string Username { get; set; }

      [Display(Name = "کلمه عبور")]
      [Required(ErrorMessage = "فیلد {0} نمی‌تواند خالی باشد ")]
      public string Password { get; set; }

      [Display(Name = "مرا به خاطر بسپار")]
      public bool RememberMe { get; set; }
   }

   public class ForgetPasswordViewModel
   {
      [Display(Name = "ایمیل")]
      [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
      [EmailAddress(ErrorMessage = "ایمیل وارد شده صحیح نیست")]
      public string Email { get; set; }

   }

   public class ResetPassswordViewModel
   {
      public string ActivateCode { get; set; }

      [Display(Name = "کلمه عبور")]
      [Required(ErrorMessage = "فیلد {0} نباید خالی باشد")]
      [MaxLength(250, ErrorMessage = "در فیلد {0} بیشتر از حد مجاز وارد شده است")]
      public string Password { get; set; }

      [Display(Name = "تکرار کلمه عبور")]
      [Required(ErrorMessage = "فیلد {0} نباید خالی باشد")]
      [MaxLength(250, ErrorMessage = "در فیلد {0} بیشتر از حد مجاز وارد شده است")]
      [Compare("Password", ErrorMessage = "تکرار کلمه عبور صحیح نیست")]
      public string ConfirmPassword { get; set; }
   }
}
