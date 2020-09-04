using Api.Actor;
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
    public class ActorController : BaseApiController {
        #region GET
        [HttpGet]
        [Route("Api/Actors")]
        [ResponseType(typeof(List<ActorModel>))]
        public IHttpActionResult List() {
            //using (var db = new ApplicationDbContext()) {
                var actors = Actor.SelectAll(DbContext).ConvertAll(a => a.ToModel());
                return Ok(actors);
            //}
        }

        [HttpGet]
        [Route("Api/Actors/{id}")]
        [ResponseType(typeof(ActorModel))]
        public IHttpActionResult Get(Guid id) {
            //using (var db = new ApplicationDbContext()) {
                var actor = Actor.SelectById(id, DbContext);
                return Ok(actor.ToModel());
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
        [HttpPost]
        [Route("Api/Actors")]
        public IHttpActionResult Post(ActorPostRequest request) {
            //using (var db = new ApplicationDbContext()) {
                Actor.Create(request.Name, request.Birthplace, DbContext);
                return Ok();
            //}
        }
        #endregion
        #region PUT
        [HttpPut]
        [Route("Api/Actors/{id}")]
        public IHttpActionResult Put(Guid id, ActorPostRequest request) {
            //using (var db = new ApplicationDbContext()) {
                Actor.Update(id, request.Name, request.Birthplace, DbContext);
                return Ok();
            //}
        }
        #endregion
        #region DELETE
        [HttpDelete]
        [Route("Api/Actors/{id}")]
        public IHttpActionResult Delete(Guid id) {
            //using (var db = new ApplicationDbContext()) {
                Actor.Delete(id, DbContext);
                return Ok();
            //}
        }
        #endregion
    }
}
