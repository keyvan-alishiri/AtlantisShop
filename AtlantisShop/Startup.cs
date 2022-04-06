using AtlantisShop.Core.Services;
using AtlantisShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtlantisShop_DBContext.Web
{
   public class Startup
   {
	  public Startup(IConfiguration configuration)
	  {
		 Configuration = configuration;
	  }

	  public IConfiguration Configuration { get; }

	  // This method gets called by the runtime. Use this method to add services to the container.
	  public void ConfigureServices(IServiceCollection services)
	  {
		 #region DataBase
		 services.AddDbContext<AtlantisShop.DataLayer.Models.Atlantis_DBContext>(option =>
		 {
			option.UseSqlServer(Configuration.GetConnectionString("AtlantisConnection"));
		 });
		 #endregion

		 #region IOC
		 services.AddTransient<IUserService, UserService>();
		 services.AddTransient<IWalletService, WalletService>();
		 #endregion

		 #region Authentication
		 services.AddAuthentication(options =>
			{
			   options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			   options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			   options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

			}).AddCookie(options =>
			{
			   options.LoginPath = "/Login";
			   options.LogoutPath = "/Logout";
			   options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
			});
		 #endregion

		 services.AddRazorPages();
		
		 services.AddMvc(options => options.EnableEndpointRouting = false);
		 services.AddControllersWithViews();
	  }

	  // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
	  public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	  {
		 if (env.IsDevelopment())
		 {
			app.UseDeveloperExceptionPage();
		 }
		 else
		 {
			app.UseExceptionHandler("/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		 }
		 app.UseAuthentication();
		 app.UseHttpsRedirection();
		 app.UseStaticFiles();
		 

		 app.UseRouting();
		 app.UseMvc(routes =>
		 {
			routes.MapRoute(
			  name: "areas",
			  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
			);
			routes.MapRoute(
			  name: "Default",
			  template: "{controller=Home}/{action=Index}/{id?}"
			);
		 });
		 app.UseAuthorization();
		
		 app.UseEndpoints(endpoints =>
		 {
			endpoints.MapRazorPages();
		 });
	  }
   }
}
