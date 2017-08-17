using eWellmanShoppingApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace eWellmanShoppingApp.Migrations {
	internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>{
		public Configuration() {
			AutomaticMigrationsEnabled = true;
		}
		protected override void Seed(ApplicationDbContext context) {
			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
			if (!context.Roles.Any(r => r.Name == "Admin")) {
				roleManager.Create(new IdentityRole { Name = "Admin" });
			}
			var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
			if (!context.Users.Any(u => u.Email == "Eric.J.Wellman@gmail.com")) {
				userManager.Create(new ApplicationUser {
					UserName = "Eric.J.Wellman@gmail.com",
					Email = "Eric.J.Wellman@gmail.com",
					firstName = "Eric",
					lastName = "Wellman"
				}, "***REMOVED***");
			}
			var userId = userManager.FindByEmail("Eric.J.Wellman@gmail.com").Id;
			userManager.AddToRole(userId, "Admin");
		}
	}
}