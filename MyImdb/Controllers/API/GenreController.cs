using Api.Genre;
using BusinessLogic;
using BusinessLogic.Data;
using BusinessLogic.Exceptions;
using Microsoft.Ajax.Utilities;
using MyImdb.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace MyImdb.Controllers.API
{   [ValidateModelState]
    public class GenreController : BaseApiController {
        #region GET
        [HttpGet]
        [Route("Api/Genres")]
        [ResponseType(typeof(List<GenreModel>))]
        public IHttpActionResult List() {
            //using (var db = new ApplicationDbContext()) {
                //throw new ErrorModelException(ErrorCodes.GenreAlreadyExists);
                var genres = Genre.SelectAll(DbContext).ConvertAll(g => g.ToModel());
                return Ok(genres);
            //}
        }

        [HttpGet]
        [Route("Api/Genres/{id}")]
        [ResponseType(typeof(GenreModel))]
        public IHttpActionResult Get(Guid id) {
            //using (var db = new ApplicationDbContext()) {
                var genre = Genre.SelectById(id, DbContext);
                return Ok(genre.ToModel());
            //}
        }

        [HttpGet]
        [Route("Api/Genres/{id}/MovieTitles")]
        [ResponseType(typeof(List<string>))]
        public IHttpActionResult MovieTitles(Guid id) {
            //using (var db = new ApplicationDbContext()) {
                var movieTitles = Genre.MovieTitles(id, DbContext);
                return Ok(movieTitles);
            //}
        }

        [HttpGet]
        [Route("Api/Actors/{id}/Characters")]
        [ResponseType(typeof(List<string>))]
        public IHttpActionResult Characters(Guid id) {
            //using (var db = new ApplicationDbContext()) {
                var characters = Actor.CharactersRelation(id, DbContext);
                return Ok(characters);
            //}
        }
        #endregion
        #region POST

        [AuthorizationAttribute(ApplicationUserManager.IsAdminClaim, ApplicationUserManager.IsAdminClaimValue)]
        [HttpPost]
        [Route("Api/Genres")]
        public async Task<IHttpActionResult> Post(GenrePostRequest request) {
            //using (var db = new ApplicationDbContext()) {
            await Genre.CreateAsync(request.Name, GetUserId(), DbContext, UserManager);
            return Ok();
            //}
        }
        #endregion
        #region PUT
        [HttpPut]
        [Route("Api/Genres/{id}")]
        public IHttpActionResult Put(Guid id, GenrePostRequest request) {
            //using (var db = new ApplicationDbContext()) {
                Genre.Update(id, request.Name, DbContext);
                return Ok();
            //}
        }
        #endregion
        #region DELETE
        [HttpDelete]
        [Route("Api/Genres/{id}")]
        public IHttpActionResult Delete(Guid id) {
            //using (var db = new ApplicationDbContext()) {
                Genre.Delete(id, DbContext);
                return Ok();
            //}
        }
        #endregion
    }
}
