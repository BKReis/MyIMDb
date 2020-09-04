using BusinessLogic.Data;
using BusinessLogic.Exceptions;
using Lacuna.CommonEntityFramework;
using Microsoft.AspNet.Identity.Owin;
using MyImdb;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

public class MaintenanceController : Controller {
    private ApplicationDbContext _dbContext;
    protected ApplicationDbContext DbContext {
        get {
            if (_dbContext == null) {
                _dbContext = Request.GetOwinContext().Get<ApplicationDbContext>();
            }
            return _dbContext;
        }
    }
    protected override void OnActionExecuting(ActionExecutingContext filterContext) {
        base.OnActionExecuting(filterContext);
        if (!MvcApplication.InMaintenanceMode) {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            ViewData["Message"] = "Error: The server is not in maintenance mode";
            filterContext.Result = View("Forbidden");
        }
        else if (!
            
            
            
            
            Request.IsLocal && !Request.IsSecureConnection) {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            ViewData["Message"] = "Error: This resource can only be accessed over HTTPS or locally";
            filterContext.Result = View("Forbidden");
        }
    }
    public ActionResult Index() {
        switch (MvcApplication.DatabaseState) {

            case DatabaseStates.NoMetadata:
            case DatabaseStates.Outdated:
            return View("DatabaseOutdated");
            case DatabaseStates.NoAccess:
            return View("NoAccess");
            case DatabaseStates.NotInitialized:
            return View("Configure");
        default:
            throw new NotSupportedException();
        }
        }
    [HttpPost]
    public ActionResult UpdateDatabase(UpdateDatabaseModel model) {
        string validationError;
        if (!ApplicationDbContext.UpdateDatabase(model, out validationError)) {
            ViewBag.Message = validationError;
            return View("DatabaseOutdated");
        }
        return CheckDatabase();
    }
    [HttpPost]
    public async Task<ActionResult> Configure(ConfigureModel model) {
        if (!ModelState.IsValid) {
            return View(model);
        }
        using (var transaction = DbContext.Database.BeginTransaction()) {
            var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            try {
                await ApplicationDbContext.InitializeAsync(model.Email, model.Password, DbContext, userManager);
                transaction.Commit();
                return CheckDatabase();
            }
            catch (ErrorModelException e) {
                ViewBag.Message = e.ErrorModel.Message;
            }
            catch (Exception ex) {
                ViewBag.Message = ex.ToString();
            }
            return View(model);
        }
    }
    [HttpPost]
    public ActionResult CheckDatabase() {
        MvcApplication.CheckDatabase();
        if (MvcApplication.InMaintenanceMode) {
            return RedirectToAction("Index");
        }
        else {
            return RedirectToAction("Index", "Home");
        }
    }
    }

    public class ConfigureModel {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }