using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace WorldCup.Models.Identity
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Dummy { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        public IQueryable<ApplicationUser> AllUsers
        {
            get
            {
                var userStore = Store as UserStore<ApplicationUser>;
                return userStore != null ? userStore.Users : Enumerable.Empty<ApplicationUser>().AsQueryable();
            }
        }

        public async Task<ApplicationUser> ChangeUserStatus(string userId, bool activateUser)
        {
            var user = await FindByIdAsync(userId);
            if (user != null)
            {
                user.EmailConfirmed = activateUser;
                await Store.UpdateAsync(user);
                return user;
            }
            
            throw new InvalidOperationException("The requested user was not found");
        }
        
    }

    // Configure the RoleManager used in the application. RoleManager is defined in the ASP.NET Identity core assembly
    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationRoleManager(new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>()));

            return manager;
        }
    }
    
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        static ApplicationDbContext()
        {
            Database.SetInitializer(new ApplicationDatabaseInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    internal class ApplicationDatabaseInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            InitializeAdmins();
            InitializeUsers();
            base.Seed(context);
        }

        private void InitializeAdmins()
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();

            const string xabikosName = "c.karypidis@niposoftware.com";
            const string rutgerName = "r.dejong@niposoftware.com";
            const string password = "Admin@123456";
            const string roleName = "Admin";

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                roleManager.Create(role);
            }

            var xabikosUser = userManager.FindByName(xabikosName);
            if (xabikosUser == null)
            {
                xabikosUser = new ApplicationUser
                {
                    UserName = xabikosName,
                    Email = xabikosName,
                    FirstName = "Charalampos",
                    LastName = "Karypidis"
                };
                userManager.Create(xabikosUser, password);
                userManager.SetLockoutEnabled(xabikosUser.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(xabikosUser.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                userManager.AddToRole(xabikosUser.Id, role.Name);
            }

            var rutgerUser = userManager.FindByName(rutgerName);
            if (rutgerUser == null)
            {
                rutgerUser = new ApplicationUser
                {
                    UserName = rutgerName,
                    Email = rutgerName,
                    FirstName = "Rutger",
                    LastName = "de Jong"
                };
                userManager.Create(rutgerUser, password);
                userManager.SetLockoutEnabled(rutgerUser.Id, false);
            }

            // Add user admin to Role Admin if not already added
            rolesForUser = userManager.GetRoles(rutgerUser.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                userManager.AddToRole(rutgerUser.Id, role.Name);
            }

        }

        private void InitializeUsers()
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

            const string password = "Pa$$w0rd12";

            var user = new ApplicationUser
            {
                UserName = "h.tuncer@niposoftware.com",
                Email = "h.tuncer@niposoftware.com",
                FirstName = "Hakan",
                LastName = "Tuncer"
            };
            userManager.Create(user, password);
            userManager.SetLockoutEnabled(user.Id, false);

            user = new ApplicationUser
            {
                UserName = "p.lieverest@niposoftware.com",
                Email = "p.lieverest@niposoftware.com",
                FirstName = "Paul",
                LastName = "Lieverest"
            };
            userManager.Create(user, password);
            userManager.SetLockoutEnabled(user.Id, false);

            user = new ApplicationUser
            {
                UserName = "i.metin@niposoftware.com",
                Email = "i.metin@niposoftware.com",
                FirstName = "Irfan",
                LastName = "Metin"
            };
            userManager.Create(user, password);
            userManager.SetLockoutEnabled(user.Id, false);

            user = new ApplicationUser
            {
                UserName = "m.rijsk@niposoftware.com",
                Email = "m.rijsk@niposoftware.com",
                FirstName = "Martin",
                LastName = "Rijsk"
            };
            userManager.Create(user, password);
            userManager.SetLockoutEnabled(user.Id, false);
        }

    }
}