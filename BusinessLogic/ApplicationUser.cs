using BusinessLogic.Data;
using BusinessLogic.Exceptions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic {
    public class ApplicationUser : IdentityUser<Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim> {
        public virtual ICollection<Comment> Comments { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager userManager) {
            var userIdentity = await userManager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public static async Task<ApplicationUser> RegisterAsync(string email, string password, ApplicationDbContext context, ApplicationUserManager userManager) {
            var existingUser = await userManager.FindByNameAsync(email);
            if (existingUser != null) {
                throw new ErrorModelException(ErrorCodes.UserAlreadyExists);
            }
            var user = new ApplicationUser() {
                Id = Guid.NewGuid(),
                UserName = email,
                Email = email,
            };
            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded) {
            throw new ErrorModelException(ErrorCodes.UserRegisterError, result.Errors.First());
            }
            return user;
        }
    }
}
