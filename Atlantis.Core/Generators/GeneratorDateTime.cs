using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AtlantisShop.Core.Generators
{
   public static class GeneratorDateTime
   {
	  public static string GetNowShamsiDate()
	  {
		 PersianCalendar pc = new PersianCalendar();
		 DateTime value = DateTime.Now;
		 string year = pc.GetYear(value).ToString("0000");
		 string month = pc.GetMonth(value).ToString("00");
		 string day = pc.GetDayOfMonth(value).ToString("00");

		 return year + "/" + month + "/" + day;
	  }
   }
	  
}
