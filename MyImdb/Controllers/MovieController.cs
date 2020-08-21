using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyImdb.Models;
using MyImdb.ViewModels;

namespace MyImdb.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Index(string msg = null)
        {
            var movies = Movie.SelectAll().ConvertAll(m => new MovieListViewModel() {
                Rank = m.Rank,
                Title = m.Title,
                Year = m.Year
            }); ;
            ViewBag.Message = msg;
            return View(movies); 
        }

        public ActionResult MovieOfTheMonth() {
            return View();
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MovieCreateViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            return RedirectToAction(nameof(Index), new { msg = "Movie created with success." });
        }
    }
}