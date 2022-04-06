using AtlantisShop.Core.Convertors;
using AtlantisShop.Core.Security;
using AtlantisShop.Core.Services.Interfaces;
using AtlantisShop.Core.ViewModels;

using AtlantisShop.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace AtlantisShop.Core.Services
{
   public class UserService : IUserService
   {
	  private Atlantis_DBContext db;
	  private IHostingEnvironment hosting;
	  private IWalletService ws;
	  public UserService(Atlantis_DBContext _db,IHostingEnvironment _hosting,IWalletService _ws)
	  {
		 hosting = _hosting;
		 db = _db;
		 ws = _ws;
	  }
	  public int AddUser(RegisterViewModel register)
	  {
		 Users users = new Users()
		 {
			ActivateCode = Generators.GeneratorGuid.GetNewGuid(),
			Email = FixedText.FixedEmail(register.Email),
			FirstName = register.FirstName,
			IsActive = false,
			IsDelete = false,
			LastName = register.LastName,
			Mobile = register.Mobile,
			Password =PasswordHelper.EncodePasswordMd5(register.Password),
			RegisterDate = DateTime.Now,
			Tell = register.Tell,
			UserAvatar = "NoPhoto.png",
		 };

		 // ===================================== Send Email ==================================//
		 string Body = "<p>کاربر گرامی" +" "+ users.FirstName + " " + users.LastName + "<br>" +
			   "برای فعال سازی حساب کاربری خود روی لینک  زیر کلیک کنید" + "<br>" +
			   "<a href='https://localhost:44319/Account/ActiveUser/" + users.ActivateCode + "' >" +
			   "فعالسازی حساب" + "</a>" + "</p>";
		 //====================================================================================//

		 Senders.Email.SendEmail.Send("فعالسازی حساب کاربری",Body,users.Email);

		 db.Users.Add(users);
		 db.SaveChanges();
		 return users.UserId;
	  }

	  public bool IsExistMobile(string mobile)
	  {
		 return db.Users.Any(c=>c.Mobile == FixedText.FixedEmail(mobile) && c.IsDelete == false );
	  }

	  public bool IsExistEmail(string email)
	  {
		 return db.Users.Any(c => c.Email == FixedText.FixedEmail(email) && c.IsDelete == false);
	  }

	  public bool RegisterUser(RegisterViewModel register)
	  {
		 int UserId = AddUser(register);
		 Address address = new Address()
		 {
			UserId = UserId,

			Address1 = register.Address1,
			City = register.City,
			PostalCode = register.PostalCode,
			State = register.State,

		 };
		 db.Address.Add(address);
		 return Convert.ToBoolean(db.SaveChanges());
	  }

	  #region Login
	  public Users Login(string Username, string Password)
	  {
		 Password = PasswordHelper.EncodePasswordMd5(Password);
		 if (Username.Contains("@"))
		 {
			return db.Users.Where(c => c.Email == Username && c.Password == Password
			&& c.IsActive == true && c.IsDelete == false).FirstOrDefault();
		 }
		 else if (Username.StartsWith("09"))
		 {
			return  db.Users.Where(c => c.Mobile == Username && c.Password == Password
			&& c.IsActive == true && c.IsDelete == false).FirstOrDefault();

			
		 }
		 else
		 {
			return null;
		 }
	  }
	  #endregion

	  #region Activate User
	  public bool  ActiveUser(string ActivateCode)
	  {
		 Users user = db.Users.Where(c => c.ActivateCode == ActivateCode).FirstOrDefault();	 
		 if(user == null || user.IsActive == true)
		 {
			return false;
		 }

		 user.IsActive = true;
		 user.ActivateCode = Generators.GeneratorGuid.GetNewGuid();
		 db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
		 return Convert.ToBoolean(db.SaveChanges());
	  }
	  #endregion

	  #region Forget Password
	 public Users GetUserByEmail(string email)
	  {
		 email = FixedText.FixedEmail(email);
		 return db.Users.Where(c => c.Email == email && c.IsDelete == false && c.IsActive == true).FirstOrDefault();
		 
	  }

	  public Users GetUserByActivateCode(string activateCode)
	  {
		 return db.Users.Where(c => c.ActivateCode == activateCode).FirstOrDefault();
	  }

	  public bool  ResetPassword(ResetPassswordViewModel model)
	  {
		 Users user = GetUserByActivateCode(model.ActivateCode);
		 if(user == null)
		 {
			return false; 
		 }

		 model.Password = PasswordHelper.EncodePasswordMd5(model.Password);


		 user.Password = model.Password;
		 user.ActivateCode=Generators.GeneratorGuid.GetNewGuid();

		 db.Entry(user).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
		 return Convert.ToBoolean(db.SaveChanges());


	  }
	  #endregion

	  #region UserPanel
	 public UserInfoViewModel GetUserInfo(string email)
	  {
		 return db.Users.Where(c => c.Email == email && c.IsDelete == false).Select(c => new UserInfoViewModel()
		 {
			Email = c.Email,
			FirstName = c.FirstName,
			LastName = c.LastName,
			Mobile = c.Mobile,
			RegisterDate = Convert.ToDateTime(c.RegisterDate).ToShamsi(),
			UserAvatar=c.UserAvatar,   
			Wallet	 =ws.GetUserWalletAmount(email),

			
		 }).FirstOrDefault();
	  }

	  public EditUserViewModel GetUserDataForEdit(string email)
	  {

		 return db.Users.Include(c => c.Address).Where(c => c.Email == email && c.IsDelete == false).Select(c => new EditUserViewModel()
		 {
			Address1 = c.Address.Where(a => a.UserId == c.UserId).FirstOrDefault().Address1,
			City = c.Address.Where(a => a.UserId == c.UserId).FirstOrDefault().City,
			FirstName = c.FirstName,
			LastName = c.LastName,
			Mobile = c.Mobile,
			PostalCode = c.Address.Where(a => a.UserId == c.UserId).FirstOrDefault().PostalCode,
			State = c.Address.Where(a => a.UserId == c.UserId).FirstOrDefault().State,
			Tell = c.Tell,
			UserOldAvatar = c.UserAvatar
		 }).FirstOrDefault();

	  }

	  public bool SaveUserEdit(string email, EditUserViewModel model)
	  {
		 Users user = db.Users.Where(c => c.Email == email && c.IsDelete == false).FirstOrDefault();

		 if (model.UserProfile != null)
		 {
			if (model.UserProfile.Length / 1024 > 1024)
			{
			   return false;
			}
			if (model.UserProfile.FileName.EndsWith("jpg") || model.UserProfile.FileName.EndsWith("png"))
			{
			   if (!model.UserOldAvatar.Contains("NoPhoto.png"))
			   {
				  string DeleteFilePath = Path.Combine(hosting.WebRootPath, "PanelContent/UserAvatars", model.UserOldAvatar);

				  System.IO.File.Delete(DeleteFilePath);
			   }

			   string Filename = Generators.GeneratorGuid.GetNewGuid() + Path.GetExtension(model.UserProfile.FileName);
			   string path = Path.Combine(hosting.WebRootPath, @"PanelContent\UserAvatars", Filename);

			   using (var stream = new FileStream(path, FileMode.Create))
			   {
				  model.UserProfile.CopyTo(stream);
				  model.UserOldAvatar = Filename;
			   }
			}
			else
			{
			   return false;
			}
		 }

		 Address UserAddress = db.Address.Where(c => c.UserId == user.UserId).FirstOrDefault();

		 if (!string.IsNullOrEmpty(model.State) && model.State != "empty")
		 {

			UserAddress.State = model.State;
			UserAddress.City = model.City;


		 }

		 UserAddress.Address1 = model.Address1;
		 UserAddress.PostalCode = model.PostalCode;

		 db.Entry(UserAddress).State = EntityState.Modified;

		 user.FirstName = model.FirstName;
		 user.LastName = model.LastName;
		 user.Mobile = model.Mobile;
		 user.Tell = model.Tell;
		 user.UserAvatar = model.UserOldAvatar;

		 db.Entry(user).State = EntityState.Modified;

		 return Convert.ToBoolean(db.SaveChanges());
	  }

	  public bool CheckOldPassword(string email, string password)
	  {
		 Users user = GetUserByEmail(email);

		 password=PasswordHelper.EncodePasswordMd5(password);
		 if (user.Password == password)
			return true;
		 else
			return false;

	  }

	  public bool ChangePassword(string email, string newPassword)
	  {
		 Users user = GetUserByEmail(email);

		 try
		 {
			user.Password = PasswordHelper.EncodePasswordMd5(newPassword);
			db.Entry(user).State = EntityState.Modified;
			return Convert.ToBoolean(db.SaveChanges());
		 }
		 catch 
		 {

			return false;
		 }
	  }
	  #endregion

   }
}
