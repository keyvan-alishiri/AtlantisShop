using System;
using System.Collections.Generic;
using System.Text;

namespace AtlantisShop.Core.Convertors
{
   public static class FixedText
   {
	  public static string FixedEmail(string email)
	  {
		 return email.Replace(" ", "");
	  }
   }
}
