using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyImdb.Controllers
{
    public class DialogController : Controller
    {
        // GET: Dialog
        public ActionResult FavoriteGenre() { return View(); }
    }
}