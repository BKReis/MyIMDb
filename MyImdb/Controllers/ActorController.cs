using BusinessLogic;
using BusinessLogic.Data;
using MyImdb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyImdb.Controllers {
    public class ActorController : Controller {
        // GET: Actor
        public ActionResult Index(string msg = null) {
            using (var db = new ApplicationDbContext()) {
                var actors = Actor.SelectAll(db).ConvertAll(a => new ActorViewModel() {
                    Id = a.Id,
                    Name = a.Name,
                    Birthplace = a.Birthplace
                });
                ViewBag.Message = msg;
                return View(actors);
            }
        }

        public ActionResult Edit(Guid id) {
            using (var db = new ApplicationDbContext()) {
                var model = new ActorViewModel();
                try {
                    var actor = Actor.SelectById(id, db);
                    model.Id = actor.Id;
                    model.Name = actor.Name;
                    model.Birthplace = actor.Birthplace;
                }
                catch (Exception e) {
                    ViewBag.Error = e.Message;
                }
                return View(model);
            }
        }

        public ActionResult Delete(Guid id) {
            using (var db = new ApplicationDbContext()) {
                var model = new ActorDeleteViewModel();
                try {
                    var actor = Actor.SelectById(id, db);
                    model.Actor = new ActorViewModel() {
                        Id = actor.Id,
                        Name = actor.Name,
                        Birthplace = actor.Birthplace
                    };
                    model.Characters = Actor.CharactersRelation(actor.Id, db);
                }
                catch (Exception e) {
                    ViewBag.Error = e.Message;
                }
                return View(model);
            }
        }

        public ActionResult Create() {
            var model = new ActorCreateViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ActorCreateViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            using (var db = new ApplicationDbContext()) {
                try {
                    Actor.Create(model.Name, model.Birthplace, db);
                    return RedirectToAction(nameof(Index), new { msg = "Actor created with success." });
                }
                catch (Exception e) {
                    ViewBag.Error = e.Message;
                    return View(model);
                }
            }
        }

        [HttpPost]
        public ActionResult Edit(ActorViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            using (var db = new ApplicationDbContext()) {
                try {
                    Actor.Update(model.Id, model.Name, model.Birthplace, db);
                    return RedirectToAction(nameof(Index), new { msg = "Actor updated with success." });
                }
                catch (Exception e) {
                    ViewBag.Error = e.Message;
                    return View(model);
                }
            }
        }
        [HttpPost]
        public ActionResult Delete(ActorDeleteViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            using (var db = new ApplicationDbContext()) {
                try {
                    model.Characters = Actor.CharactersRelation(model.Actor.Id, db);
                    Actor.Delete(model.Actor.Id, db);
                    return RedirectToAction(nameof(Index), new { msg = "Actor deleted with success." });
                }
                catch (Exception e) {
                    ViewBag.Error = e.Message;
                    return View(model);
                }
            }
        }

    }
}