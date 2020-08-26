using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyImdb.ViewModels {
    public class ActorDeleteViewModel {
        public ActorViewModel Actor { get; set; } // Genre
        public List<String> Characters { get; set; }
    }
}