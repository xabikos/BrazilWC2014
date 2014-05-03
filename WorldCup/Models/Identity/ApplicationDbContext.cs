using System.Data.Entity;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using WorldCup.Common.DataAccess;
using WorldCup.Common.Entities;

namespace WorldCup.Models.Identity
{
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new LongRunningPredictionConfiguration());
            modelBuilder.Configurations.Add(new FootballTeamConfiguration());
            modelBuilder.Configurations.Add(new MatchConfiguration());
            modelBuilder.Configurations.Add(new MatchPredictionConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        #region DbSets

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }
        
        #endregion
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