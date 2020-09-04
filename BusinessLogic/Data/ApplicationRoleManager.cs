using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data {
    public class ApplicationRoleManager : RoleManager<ApplicationRole, Guid> {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, Guid> store) : base(store) {
        }
        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context) {
            return new ApplicationRoleManager(new RoleStore<ApplicationRole, Guid, ApplicationUserRole>(context.Get<ApplicationDbContext>()));
        }
    }
}
