using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using eWellmanShoppingApp.Models.CodeFirst;
using System.Collections.Generic;

namespace eWellmanShoppingApp.Models{
	// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
	public class ApplicationUser : IdentityUser {

		public string firstName { get; set; }
		public string lastName { get; set; }
		public string fullName {
			get {
				return firstName + " " + lastName;
			}
		}

		public ApplicationUser(){
			orders = new HashSet<Order>();
			cartItems = new HashSet<CartItem>();
		}

	public virtual ICollection<Order> orders { get; set; }
	public virtual ICollection<CartItem> cartItems { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager){
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>{
        public ApplicationDbContext()
            : base("ewellman-shoppingapp", throwIfV1Schema: false){
        }

        public static ApplicationDbContext Create(){
            return new ApplicationDbContext();
        }

		public DbSet<Item> items { get; set; }
		public DbSet<CartItem> cartItems { get; set; }
		public DbSet<Order> orders { get; set; }
		public DbSet<OrderItem> orderItems { get; set; }
	}
}