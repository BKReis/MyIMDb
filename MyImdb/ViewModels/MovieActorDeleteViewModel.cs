using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyImdb.ViewModels {
    public class MovieActorDeleteViewModel {
        public MovieActorViewModel Relation { get; set; } // Genre
        public List<CharacterActorListViewModel> Actors { get; set; }


    } 
}