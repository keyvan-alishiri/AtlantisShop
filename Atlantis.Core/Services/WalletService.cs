using AtlantisShop.Core.Services.Interfaces;
using AtlantisShop.Core.ViewModels;
using AtlantisShop.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtlantisShop.Core.Services
{
   public class WalletService :  IWalletService
   {
	  private Atlantis_DBContext db;
	  public WalletService(Atlantis_DBContext _db)
	  {
		 db = _db;
	  }


	  public int GetUserWalletAmount(string email)
	  {
		 int UserId = db.Users.Where(c => c.Email == email && c.IsDelete == false).FirstOrDefault().UserId;
		 int Varizi = db.Wallets.Where(c => c.UserId == UserId && c.WalletTypeId == 1 && c.IsPay == true).Sum(c=>c.Amount);
		 int Bardasht = db.Wallets.Where(c => c.UserId == UserId && c.WalletTypeId == 2 && c.IsPay == true).Sum(c => c.Amount);
		 return (Varizi - Bardasht);

	  }


	  public WalletListViewModel GetUserWallets(string email, int PageId = 1, int Take = 5)
	  {
		 WalletListViewModel list = new WalletListViewModel();
		 int UserId = db.Users.Where(c => c.Email == email).FirstOrDefault().UserId;
		 list.Wallets = db.Wallets.Include(c=>c.WalletType).Where(c=>c.UserId == UserId).ToList();
		 int skip = (PageId - 1) * Take;

		 list.CurrentPage = PageId;
		 list.Count = list.Wallets.Count;
		 list.PageCount = (int)Math.Ceiling(list.Count/(double)Take);
		 list.Wallets = list.Wallets.Skip(skip).Take(Take).ToList();

		 return list;

	  }
   }
}
