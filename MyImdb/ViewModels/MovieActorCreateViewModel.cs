﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyImdb.ViewModels {
    public class MovieActorCreateViewModel {
        [Required(ErrorMessage = "Character name is required")]
        [MaxLength(100, ErrorMessage = "Character name can't be greater than {1} characters")]
        [DisplayName("Character name")]
        public string Character { get; set; }
        public List<ActorViewModel> Actors { get; set; }
        [DisplayName("Actor")]
        [Required(ErrorMessage = "The actor must be selected.")]
        public Guid SelectedActorId { get; set; }
        [Required(ErrorMessage = "The movie must be selected.")]
        public Guid SelectedMovieId { get; set; }

    }
}