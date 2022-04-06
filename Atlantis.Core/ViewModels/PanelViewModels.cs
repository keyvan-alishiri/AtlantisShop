using AtlantisShop.DataLayer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AtlantisShop.Core.ViewModels
{
   public class UserInfoViewModel
   {
      public string FirstName { get; set; }

      public string LastName { get; set; }

      public string Mobile { get; set; }

      public string Email { get; set; }

      public string RegisterDate { get; set; }

      public string UserAvatar { get; set; }

      public int Wallet { get; set; }
   }

   public class EditUserViewModel
   {
      [Display(Name = "نام")]
      [Required(ErrorMessage = "فیلد {0} نمی‌تواند خالی باشد")]
      [MaxLength(250, ErrorMessage = "فیلد {0} حداکثر 250کاراکتر می‌باشد")]
      public string FirstName { get; set; }

      [Display(Name = "نام خانوادگی")]
      [Required(ErrorMessage = "فیلد {0} نمی‌تواند خالی باشد")]
      [MaxLength(250, ErrorMessage = "فیلد {0} حداکثر 250کاراکتر می‌باشد")]
      public string LastName { get; set; }

      [Display(Name = "موبایل")]
      [Required(ErrorMessage = "فیلد {0} نمی‌تواند خالی باشد")]
      [MaxLength(11, ErrorMessage = "فیلد {0} حداکثر  11 کاراکتر می‌باشد")]
      public string Mobile { get; set; }

      [Display(Name = "تلفن ثابت")]
      [MaxLength(11, ErrorMessage = "فیلد {0} حداکثر  11 کاراکتر می‌باشد")]
      public string Tell { get; set; }

      [Display(Name = "استان")]
      [MaxLength(250, ErrorMessage = "فیلد {0} حداکثر  250 کاراکتر می‌باشد")]
      public string State { get; set; }

      [Display(Name = "شهر")]
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

	  [Display(Name = "تصویر پروفایل")]
	  public IFormFile UserProfile { get; set; }

	  public string UserOldAvatar { get; set; }
   }

   public class ChangePasswordViewModel
   {
      [Display(Name = "کلمه عبور فعلی")]
      [Required(ErrorMessage = "فیلد {0} نمی‌تواند خالی باشد")]
      [MaxLength(250, ErrorMessage = "فیلد {0} حداکثر  250 کاراکتر می‌باشد")]
      public string OldPassword { get; set; }

      [Display(Name = "کلمه عبور جدید")]
      [Required(ErrorMessage = "فیلد {0} نمی‌تواند خالی باشد")]
      [MaxLength(250, ErrorMessage = "فیلد {0} حداکثر  250 کاراکتر می‌باشد")]
      public string Password { get; set; }

      [Display(Name = "تکرار کلمه عبور جدید")]
      [Required(ErrorMessage = "فیلد {0} نمی‌تواند خالی باشد")]
      [MaxLength(250, ErrorMessage = "فیلد {0} حداکثر  250 کاراکتر می‌باشد")]
      [Compare("Password", ErrorMessage = "تکرار کلمه عبور صحیح نیست")]
      public string ConfirmPassword { get; set; }
   }


   public class WalletListViewModel
   {
	  public List<Wallets> Wallets { get; set; }
	  public int PageId { get; set; }
	  public int CurrentPage { get; set; }
	  public int PageCount { get; set; }
	  public int Count { get; set; }
   }
}
