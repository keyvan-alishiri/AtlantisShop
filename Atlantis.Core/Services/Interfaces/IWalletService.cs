using AtlantisShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtlantisShop.Core.Services.Interfaces
{
   public interface IWalletService
   {
	  int GetUserWalletAmount(string email);
	  WalletListViewModel GetUserWallets(string email , int PageId=1,int Take = 3 );
   }
}
