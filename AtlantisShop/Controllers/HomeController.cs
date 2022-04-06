using Microsoft.AspNetCore.Mvc;

namespace AtlantisShop_DBContext.Web.Controllers
{
   public class HomeController : Controller
   {
	  public IActionResult Index()
	  {
		 return View();
	  }
   }
}
