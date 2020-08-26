using BusinessLogic;
using BusinessLogic.Data;
using MyImdb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyImdb.Controllers {
    public class MovieActorController : Controller {
        // GET: Actor

        public ActionResult Edit(Guid id) {
            using (var db = new ApplicationDbContext()) {
                var model = new MovieActorViewModel();
                try {
                    var character = MovieActor.SelectById(id, db);
                    model.Id = character.Id;
                    model.Name = character.Character;
                    model.SelectedMovieId = character.MovieId;
                    model.SelectedActorId = character.ActorId;
                    model.Actors = Actor.SelectAll(db).ConvertAll(a => new ActorViewModel() {
                        Id = a.Id,
                        Name = a.Name
                    });
                }
                catch (Exception e) {
                    ViewBag.Error = e.Message;
                }
                return View(model);
            }
        }

        public ActionResult Delete(Guid id) {
            using (var db = new ApplicationDbContext()) {
                var model = new MovieActorDeleteViewModel();
                try {
                    var character = MovieActor.SelectById(id, db);
                    model.Relation = new MovieActorViewModel() {
                        Id = character.Id,
                        Name = character.Character,
                        SelectedMovieId = character.MovieId,
                        SelectedActorId = character.ActorId
                    };
                }
                catch (Exception e) {
                    ViewBag.Error = e.Message;
                }
                return View(model);
            }
        }

        public ActionResult Create(Guid movieId) {
            using (var db = new ApplicationDbContext()) {
                var actors = Actor.SelectAll(db);
                var model = new MovieActorCreateViewModel() {
                    Actors = actors.ConvertAll(a => new ActorViewModel() {
                        Id = a.Id,
                        Name = a.Name
                    })
                };
                model.SelectedMovieId = movieId;

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Create(MovieActorCreateViewModel model) {
            using (var db = new ApplicationDbContext()) {
                model.Actors = Actor.SelectAll(db).ConvertAll(a => new ActorViewModel() {
                    Id = a.Id,
                    Name = a.Name
                });
                if (!ModelState.IsValid) {
                    return View(model);
                }
                try {
                    MovieActor.Create(model.Character, model.SelectedMovieId, model.SelectedActorId, db);
                    return RedirectToAction("Details", "Movie", new { id = model.SelectedMovieId, msg = "Character created with success." });
                }
                catch (Exception e) {
                    ViewBag.Error = e.Message;
                    return View(model);
                }
            }
        }


        [HttpPost]
        public ActionResult Edit(MovieActorViewModel model) {
            using (var db = new ApplicationDbContext()) {
                model.Actors = Actor.SelectAll(db).ConvertAll(a => new ActorViewModel() {
                    Id = a.Id,
                    Name = a.Name
                });
                if (!ModelState.IsValid) {
                    return View(model);
                }
                try {
                    MovieActor.Update(model.Id, model.Name, model.SelectedMovieId, model.SelectedActorId, db);
                    return RedirectToAction("Details", "Movie", new { id = model.SelectedMovieId, msg = "Character updated with success." });
                }
                catch (Exception e) {
                    ViewBag.Error = e.Message;
                    return View(model);
                }
            }
        }

        [HttpPost]
        public ActionResult Delete(MovieActorDeleteViewModel model) {
            using (var db = new ApplicationDbContext()) {
                //model.Actors = new List<CharacterActorListViewModel>(); 
                //    model.Relation.Actors.ConvertAll(a => new CharacterActorListViewModel() {
                //    Id = a.Id,
                //    Name = a.Name,
                //    Character = model.Relation.Name
                //});
                if (!ModelState.IsValid) {
                    return View(model);
                }
                try {
                    MovieActor.Delete(model.Relation.Id, db);
                    return RedirectToAction("Details", "Movie", new {id = model.Relation.SelectedMovieId, msg = "Character deleted with success." });
                }
                catch (Exception e) {
                    ViewBag.Error = e.Message;
                    return View(model);
                }
            }
        }

    }
}