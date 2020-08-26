using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyImdb.ViewModels {
    public class MovieDeleteViewModel {
        public MovieListViewModel Movie { get; set; } 
        public List<String> Characters { get; set; }
    }
}