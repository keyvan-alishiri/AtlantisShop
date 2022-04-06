using AtlantisShop.Core.ViewModels;
using AtlantisShop.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtlantisShop.Core.Services.Interfaces
{
   public interface IUserService
   {
	  bool RegisterUser(RegisterViewModel register);
	  
	  int AddUser(RegisterViewModel user);
	  bool IsExistEmail(string email);
	  bool IsExistMobile(string mobile);
	  Users Login(string Username, string Password);

	  bool ActiveUser(string activateCode);
	  Users GetUserByEmail(string email);
	  Users GetUserByActivateCode(string activateCode);
	  bool ResetPassword(ResetPassswordViewModel model);

	  #region UserPanel
	  UserInfoViewModel GetUserInfo(string email);
	  EditUserViewModel GetUserDataForEdit(string email);
	  bool SaveUserEdit(string email,EditUserViewModel model);
	  bool CheckOldPassword(string email,string password);
	  bool ChangePassword(string email,string newPassword);
	  #endregion

   }
}
