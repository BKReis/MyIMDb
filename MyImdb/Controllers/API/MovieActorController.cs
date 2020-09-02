using Api.Genre;
using Api.Movie;
using Api.MovieActor;
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
    public class MovieActorController : ApiController {

        [HttpGet]
        [Route("Api/MovieActors/{id}")]
        [ResponseType(typeof(MovieActorModel))]
        public IHttpActionResult Get(Guid id) {
            using (var db = new ApplicationDbContext()) {
                var movieActor = MovieActor.SelectById(id, db);
                return Ok(movieActor.ToModel());
            }
        }

        #region POST
        [HttpPost]
        [Route("Api/MovieActors")]
        public IHttpActionResult Post(MovieActorPostRequest request) {
            using (var db = new ApplicationDbContext()) {
                MovieActor.Create(request.Character, request.SelectedMovieId, request.SelectedActorId, db);
                return Ok();
            }
        }
        #endregion
        #region PUT
        [HttpPut]
        [Route("Api/MovieActors/{id}")]
        public IHttpActionResult Put(Guid id, MovieActorPostRequest request) {
            using (var db = new ApplicationDbContext()) {
                MovieActor.Update(id, request.Character, request.SelectedMovieId, request.SelectedActorId, db);
                return Ok();
            }
        }
        #endregion
        #region DELETE
        [HttpDelete]
        [Route("Api/MovieActors/{id}")]
        public IHttpActionResult Delete(Guid id) {
            using (var db = new ApplicationDbContext()) {
                MovieActor.Delete(id, db);
                return Ok();
            }
        }
        #endregion
    }
}
