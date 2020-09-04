using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data {
    public class ApplicationSignInManager : SignInManager<ApplicationUser, Guid> {
        public ApplicationSignInManager(UserManager<ApplicationUser, Guid> userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager) {
        }
        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context) {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
