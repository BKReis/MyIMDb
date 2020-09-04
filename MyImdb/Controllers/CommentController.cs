using BusinessLogic;
using BusinessLogic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyImdb.Controllers {
    public class CommentController : Controller {
        [Authorize]
        public ActionResult Delete() { return View(); }
        [Authorize]
        public ActionResult Create() { return View(); }
        [Authorize]
        public ActionResult Edit() { return View(); }

    }
}