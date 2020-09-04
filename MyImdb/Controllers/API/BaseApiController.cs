using Microsoft.AspNet.Identity.Owin;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System;
using System.Net.Http;
using BusinessLogic.Data;

public class BaseApiController : ApiController {
    private ApplicationDbContext _dbContext;
    protected ApplicationDbContext DbContext {
        get {
            if (_dbContext == null) {
                _dbContext = Request.GetOwinContext().Get<ApplicationDbContext>();
            }
            return _dbContext;
        }
    }
    private ApplicationUserManager _userManager;
    protected ApplicationUserManager UserManager {
        get {
            if (_userManager == null) {
                _userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            return _userManager;
        }
        set {
            _userManager = value;
        }
    }
    private ApplicationRoleManager _roleManager;
    protected ApplicationRoleManager RoleManager {
        get {
            if (_roleManager == null) {
                _roleManager = Request.GetOwinContext().Get<ApplicationRoleManager>();
            }
            return _roleManager;
        }
        set {
            _roleManager = value;
        }
    }
    private ApplicationSignInManager _signInManager;
    protected ApplicationSignInManager SignInManager {
        get {
            if (_signInManager == null) {
                _signInManager = Request.GetOwinContext().Get<ApplicationSignInManager>();
            }
            return _signInManager;
        }
        set {
            _signInManager = value;
        }
    }
    protected Guid GetUserId() {

    return Guid.Parse(User.Identity.GetUserId());
    }
    protected string GetUsername() {
        return User.Identity.GetUserName();
    }
}