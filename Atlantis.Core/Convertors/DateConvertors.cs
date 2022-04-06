using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AtlantisShop.Core.Convertors
{
   public static class DateConvertors
   {
public static string ToShamsi(this DateTime value)
	  {
		 PersianCalendar pc= new PersianCalendar();
		 string year=pc.GetYear(value).ToString("0000");
		 string month = pc.GetMonth(value).ToString("00");
		 string day = pc.GetDayOfMonth(value).ToString("00");

		 return year + "/" + month +"/"+ day;	 
	  }
   }
}
