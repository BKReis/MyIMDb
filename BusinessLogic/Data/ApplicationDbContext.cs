using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Lacuna.CommonEntityFramework;
using BusinessLogic.Exceptions;
using System.Security.Claims;

namespace BusinessLogic.Data {
    //public class ApplicationDbContext : DbContext {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, 
    ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim> {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public ApplicationDbContext() : base("DefaultConnection") {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            //Impedir que sejam gerados tabelas no padrão AspNet<Tabela>
            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUsers");
            modelBuilder.Entity<ApplicationRole>().ToTable("ApplicationRoles");
            modelBuilder.Entity<ApplicationUserClaim>().ToTable("ApplicationUserClaims");
            modelBuilder.Entity<ApplicationUserLogin>().ToTable("ApplicationUserLogins");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("ApplicationUserRoles");
        }

        public static DatabaseStates CheckDatabase() {
            using (var dbContext = new ApplicationDbContext()) {
                return DbMigrationHelper.CheckDatabase(dbContext, new Migrations.Configuration(),
                db => db.Users.Any(u => u.Claims.Count(c => c.ClaimType == ApplicationUserManager.IsAdminClaim &&
                c.ClaimValue == ApplicationUserManager.IsAdminClaimValue) > 0));
            }
        }
        public static bool UpdateDatabase(UpdateDatabaseModel model, out string validationError) {
            using (var dbContext = new ApplicationDbContext()) {
                return DbMigrationHelper.UpdateDatabase(new Migrations.Configuration(), dbContext, model, out validationError);
            }
        }
        public async static Task InitializeAsync(string email, string password, ApplicationDbContext db, ApplicationUserManager userManager) {
            var user = await ApplicationUser.RegisterAsync(email, password, db, userManager);
            var result = await userManager.AddClaimAsync(user.Id, new Claim(ApplicationUserManager.IsAdminClaim, ApplicationUserManager.IsAdminClaimValue, ClaimValueTypes.Boolean));
            if (!result.Succeeded) {
                throw new ErrorModelException(ErrorCodes.UserRegisterError, result.Errors.First());
            }
        }

    }

}
