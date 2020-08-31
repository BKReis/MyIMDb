using BusinessLogic;
using BusinessLogic.Data;
using MyImdb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyImdb.Controllers {
    public class GenreController : Controller {
        // GET: Genre
        //public ActionResult Index(string msg = null) {
        //    using (var db = new ApplicationDbContext()) {
        //        var genres = Genre.SelectAll(db).ConvertAll(g => new GenreViewModel() {
        //            Id = g.Id,
        //            Name = g.Name
        //        });
        //        ViewBag.Message = msg;
        //        return View(genres);
        //    }
        //}

        public ActionResult Index() { return View(); }

        public ActionResult Create() { return View(); }

        public ActionResult Edit(Guid id) {
            using (var db = new ApplicationDbContext()) {
                var model = new GenreViewModel();
                try {
                    var genre = Genre.SelectById(id, db);
                    model.Id = genre.Id;
                    model.Name = genre.Name;
                }
                catch (Exception e) {
                    ViewBag.Error = e.Message;
                }
                return View(model);
            }
        }

        public ActionResult Delete(Guid id) {
            using (var db = new ApplicationDbContext()) {
                var model = new GenreDeleteViewModel();
                try {
                    var genre = Genre.SelectById(id, db);
                    model.Genre = new GenreViewModel() {
                        Id = genre.Id,
                        Name = genre.Name
                    };
                    model.Movies = Genre.MovieTitles(genre.Id, db);
                }
                catch (Exception e) {
                    ViewBag.Error = e.Message;
                }
                return View(model);
            }
        }

        //public ActionResult Create() {
        //    var model = new GenreCreateViewModel();
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult Create(GenreCreateViewModel model) {
        //    if (!ModelState.IsValid) {
        //        return View(model);
        //    }
        //    using (var db = new ApplicationDbContext()) {
        //        try {
        //            Genre.Create(model.Name, db);
        //            return RedirectToAction(nameof(Index), new { msg = "Genre created with success." });
        //        }
        //        catch (Exception e) {
        //            ViewBag.Error = e.Message;
        //            return View(model);
        //        }
        //    }
        //}

        [HttpPost]
        public ActionResult Edit(GenreViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            using (var db = new ApplicationDbContext()) {
                try {
                    Genre.Update(model.Id, model.Name, db);
                    return RedirectToAction(nameof(Index), new { msg = "Genre updated with success." });
                }
                catch (Exception e) {
                    ViewBag.Error = e.Message;
                    return View(model);
                }
            }
        }

        [HttpPost]
        public ActionResult Delete(GenreDeleteViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            using (var db = new ApplicationDbContext()) {
                try {
                    model.Movies = Genre.MovieTitles(model.Genre.Id, db);
                    Genre.Delete(model.Genre.Id, db);
                    return RedirectToAction(nameof(Index), new { msg = "Genre deleted with success." });
                }
                catch (Exception e) {
                    ViewBag.Error = e.Message;
                    return View(model);
                }
            }
        }

    }
}