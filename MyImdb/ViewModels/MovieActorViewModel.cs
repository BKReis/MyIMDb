using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyImdb.ViewModels {
    public class MovieActorViewModel {
        [Required(ErrorMessage = "The relattion Id must be provided")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The name of the character is required")]
        [MaxLength(20, ErrorMessage = "The name can't be greater than {1} characters")]
        public string Name { get; set; }
        public List<ActorViewModel> Actors { get; set; }
        [DisplayName("Actor")]
        [Required(ErrorMessage = "The actor must be selected.")]
        public Guid SelectedActorId { get; set; }
        [Required(ErrorMessage = "The movie must be selected.")]
        public Guid SelectedMovieId { get; set; }
    }
}