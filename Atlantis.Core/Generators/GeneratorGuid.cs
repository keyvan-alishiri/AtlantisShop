using System;
using System.Collections.Generic;
using System.Text;

namespace AtlantisShop.Core.Generators
{
   public static class GeneratorGuid
   {
	  public static string GetNewGuid()
	  {
		 return Guid.NewGuid().ToString().Replace("-","");
	  }
   }
}
