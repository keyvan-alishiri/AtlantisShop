using System;
using System.Collections.Generic;
using System.Text;

namespace AtlantisShop.Core.Security
{
   public static class ExtensionChecker
   {
	  public static bool ImageChecker(string extension)
	  {
		 if(extension == (".jpg") || extension == (".png") || extension == (".jpeg"))
		 {
			return true;
		 }
		 else
		 {
			return false;
		 }
	  }
   }
}
