﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyImdb.ViewModels {
    public class MovieDetailedViewModel {
        [Required(ErrorMessage = "The movie Id must be provided")]
        public Guid Id { get; set; }
        public int Rank { get; set; } // Movie
        public string Title { get; set; } // Movie
        public int Year { get; set; } // Movie
        public string StoryLine { get; set; }
        public string Genre { get; set; } // Genre
        public List<CharacterActorListViewModel> Actors { get; set; }
    }
}