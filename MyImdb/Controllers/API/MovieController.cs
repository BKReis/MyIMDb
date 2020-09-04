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
    public class MovieController : BaseApiController {
        #region GET
        [HttpGet]
        [Route("Api/Movies")]
        [ResponseType(typeof(List<MovieSimpleModel>))]
        public IHttpActionResult List() {
            //using (var db = new ApplicationDbContext()) {
                var movies = Movie.SelectAll(DbContext).ConvertAll(m => m.ToModel());
                return Ok(movies);
            //}
        }

        [HttpGet]
        [Route("Api/Movies/{id}")]
        [ResponseType(typeof(MovieSimpleModel))]
        public IHttpActionResult Get(Guid id) {
            //using (var db = new ApplicationDbContext()) {
                var movie = Movie.SelectById(id, DbContext);
                return Ok(movie.ToModel());
            //}
        }

        [HttpGet]
        [Route("Api/Movies/{id}/Detailed")]
        [ResponseType(typeof(MovieDetailedModel))]
        public IHttpActionResult GetDetailed(Guid id) {
            //using (var db = new ApplicationDbContext()) {
                var movie = Movie.SelectById(id, DbContext);
                return Ok(movie.ToDetailedModel());
            //}
        }

        [HttpGet]
        [Route("Api/Movies/{id}/Characters")]
        [ResponseType(typeof(List<string>))]
        public IHttpActionResult Characters(Guid id) {
            //using (var db = new ApplicationDbContext()) {
                var characters = Movie.CharactersRelation(id, DbContext);
                return Ok(characters);
            //}
        }

        #endregion

        #region POST
        [HttpPost]
        [Route("Api/Movies")]
        public IHttpActionResult Post(MoviePostRequest request) {
            //using (var db = new ApplicationDbContext()) {
                Movie.Create(request.Rank, request.Title, request.Year, request.StoryLine, request.SelectedGenreId, DbContext);
                return Ok();
            //}
        }
        #endregion
        #region PUT
        [HttpPut]
        [Route("Api/Movies/{id}")]
        public IHttpActionResult Put(Guid id, MoviePostRequest request) {
            //using (var db = new ApplicationDbContext()) {
                Movie.Update(id, request.Rank, request.Title, request.Year, request.StoryLine, request.SelectedGenreId, DbContext);
                return Ok();
            //}
        }
        #endregion
        #region DELETE
        [HttpDelete]
        [Route("Api/Movies/{id}")]
        public IHttpActionResult Delete(Guid id) {
            //using (var db = new ApplicationDbContext()) {
                Movie.Delete(id, DbContext);
                return Ok();
            //}
        }
        #endregion
    }
}
