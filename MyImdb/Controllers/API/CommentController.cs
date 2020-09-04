using Api.Actor;
using Api.Comment;
using Api.Genre;
using Api.Movie;
using BusinessLogic;
using BusinessLogic.Data;
using MyImdb.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace MyImdb.Controllers.API {
    [ValidateModelState]
    [Authorize]
    public class CommentController : BaseApiController {
        #region GET
        [HttpGet]
        [Route("Api/Comments/{id}")]
        [ResponseType(typeof(CommentModel))]
        public IHttpActionResult Get(Guid id) {
            //using (var db = new ApplicationDbContext()) {
            var comment = Comment.SelectById(id, DbContext);
            return Ok(comment.ToModel());    
            //}
        }
        #endregion
        #region POST
        [HttpPost]
        [Route("Api/Comments")]
        public IHttpActionResult Post(CommentPostRequest request) {
            //using (var db = new ApplicationDbContext()) {
            Comment.Create(request.Value, request.SelectedMovieId, GetUserId(), DbContext);
            return Ok();
            //}
        }
        #endregion
        #region PUT
        [HttpPut]
        [Route("Api/Comments/{id}")]
        public IHttpActionResult Put(Guid id, CommentPostRequest request) {
            //using (var db = new ApplicationDbContext()) {
            Comment.Update(id, request.Value, request.SelectedMovieId, GetUserId(), DbContext); ;
            return Ok();
            //}
        }
        #endregion
        #region DELETE
        [HttpDelete]
        [Route("Api/Comments/{id}")]
        public IHttpActionResult Delete(Guid id) {
            //using (var db = new ApplicationDbContext()) {
            Comment.Delete(id, DbContext);
            return Ok();
            //}
        }
        #endregion
    }
}
