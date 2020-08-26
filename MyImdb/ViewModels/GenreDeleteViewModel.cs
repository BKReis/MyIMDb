using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyImdb.ViewModels {
    public class GenreDeleteViewModel {
        public GenreViewModel Genre { get; set; } // Genre
        public List<String> Movies { get; set; }
    }
}