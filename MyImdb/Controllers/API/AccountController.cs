using Api.Account;
using BusinessLogic;
using BusinessLogic.Exceptions;
using Microsoft.AspNet.Identity.Owin;
using MyImdb.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace MyImdb.Controllers {
    [ValidateModelState]
    public class AccountController : BaseApiController {
        #region GET
        [HttpGet]
        [Authorize]
        [Route("Api/Account/Info")]
        [ResponseType(typeof(AccountInfoResponse))]
        public IHttpActionResult Info() {
            var response = new AccountInfoResponse() {
                Id = GetUserId(),
                Username = GetUsername()
            };
            return Ok(response);
        }
        #endregion
        #region POST
        [HttpPost]
        [Route("Api/Account/Register")]
        public async Task<IHttpActionResult> Post(AccountRegisterRequest request) {
            var user = await ApplicationUser.RegisterAsync(request.Email, request.Password, DbContext, UserManager);
            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            return Ok();
        }

        [HttpPost]
        [Route("Api/Account/Login")]
        public async Task<IHttpActionResult> Login(AccountLoginRequest request) {
            var result = await SignInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, shouldLockout: false);
            if (result != SignInStatus.Success) {
                throw new ErrorModelException(ErrorCodes.LoginError);
            }
            return Ok();
        }
        #endregion
    }
}