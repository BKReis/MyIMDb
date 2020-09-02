using Api.Genre;
using BusinessLogic;
using BusinessLogic.Data;
using BusinessLogic.Exceptions;
using MyImdb.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace MyImdb.Controllers.API
{   [ValidateModelState]
    public class GenreController : ApiController {
        #region GET
        [HttpGet]
        [Route("Api/Genres")]
        [ResponseType(typeof(List<GenreModel>))]
        public IHttpActionResult List() {
            using (var db = new ApplicationDbContext()) {
                //throw new ErrorModelException(ErrorCodes.GenreAlreadyExists);
                var genres = Genre.SelectAll(db).ConvertAll(g => g.ToModel());
                return Ok(genres);
            }
        }

        [HttpGet]
        [Route("Api/Genres/{id}")]
        [ResponseType(typeof(GenreModel))]
        public IHttpActionResult Get(Guid id) {
            using (var db = new ApplicationDbContext()) {
                var genre = Genre.SelectById(id, db);
                return Ok(genre.ToModel());
            }
        }

        [HttpGet]
        [Route("Api/Genres/{id}/MovieTitles")]
        [ResponseType(typeof(List<string>))]
        public IHttpActionResult MovieTitles(Guid id) {
            using (var db = new ApplicationDbContext()) {
                var movieTitles = Genre.MovieTitles(id, db);
                return Ok(movieTitles);
            }
        }

        [HttpGet]
        [Route("Api/Actors/{id}/Characters")]
        [ResponseType(typeof(List<string>))]
        public IHttpActionResult Characters(Guid id) {
            using (var db = new ApplicationDbContext()) {
                var characters = Actor.CharactersRelation(id, db);
                return Ok(characters);
            }
        }
        #endregion
        #region POST
        [HttpPost]
        [Route("Api/Genres")]
        public IHttpActionResult Post(GenrePostRequest request) {
            using (var db = new ApplicationDbContext()) {
            Genre.Create(request.Name, db);
                return Ok();
            }
        }
        #endregion
        #region PUT
        [HttpPut]
        [Route("Api/Genres/{id}")]
        public IHttpActionResult Put(Guid id, GenrePostRequest request) {
            using (var db = new ApplicationDbContext()) {
                Genre.Update(id, request.Name, db);
                return Ok();
            }
        }
        #endregion
        #region DELETE
        [HttpDelete]
        [Route("Api/Genres/{id}")]
        public IHttpActionResult Delete(Guid id) {
            using (var db = new ApplicationDbContext()) {
                Genre.Delete(id, db);
                return Ok();
            }
        }
        #endregion
    }
}
