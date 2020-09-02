using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic;
using BusinessLogic.Data;
using System.Data.Entity;

namespace MyImdb.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        //public ActionResult Index(string msg = null)
        //{
        //    using (var db = new ApplicationDbContext()) {
        //        var movies = Movie.SelectSimple(db).ConvertAll(m => new MovieListViewModel() {
        //            Id = m.Id,
        //            Rank = m.Rank,
        //            Title = m.Title,
        //            Year = m.Year,
        //            Genre = m.Genre
        //        }); ;
        //        ViewBag.Message = msg;
        //        return View(movies);
        //    }
        //   }

        public ActionResult Index() { return View(); }

        public ActionResult Create() { return View(); }
        public ActionResult Edit() { return View(); }
        public ActionResult Delete() { return View(); }

        public ActionResult Details() { return View(); }

        public ActionResult MovieOfTheMonth() { return View();}

        //public ActionResult Details(Guid id) {
        //    using (var db = new ApplicationDbContext()) {
        //        var model = new MovieDetailedViewModel();
        //        try {
        //            var movie = Movie.DetailedMovie(id, db);
        //            model.Id = movie.Id;
        //            model.Rank = movie.Rank;
        //            model.Title = movie.Title;
        //            model.Year = movie.Year;
        //            model.StoryLine = movie.Storyline;
        //            model.Genre = movie.Genre; 
        //            model.Actors = movie.Actors.ConvertAll(a => new CharacterActorListViewModel() {
        //                Id = a.Id,
        //                Name = a.Name,
        //                Character = a.Character
        //            });
        //        }
        //        catch (Exception e) {
        //            ViewBag.Error = e.Message;
        //        }
        //        return View(model);
        //    }
        //}

        //[HttpPost]
        //public ActionResult Create(MovieCreateViewModel model) {
        //    using (var db = new ApplicationDbContext()) {
        //        model.Genres = Genre.SelectAll(db).ConvertAll(g => new GenreViewModel() {
        //            Id = g.Id,
        //            Name = g.Name
        //        });
        //        if (!ModelState.IsValid) {
        //            return View(model);
        //        }
        //        try {
        //            Movie.Create(model.Rank, model.Title, model.Year, model.StoryLine, model.SelectedGenreId, db);
        //            return RedirectToAction(nameof(Index), new { msg = "Movie created with success." });
        //        }
        //        catch (Exception e) {
        //            ViewBag.Error = e.Message;
        //            return View(model);
        //        }
        //    }
        //}

    }
}
