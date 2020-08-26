using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyImdb.ViewModels {
    public class GenreViewModel {
        [Required(ErrorMessage = "The genre Id must be provided")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The name of the genre is required")]
        [MaxLength(20, ErrorMessage = "The name can't be greater than {1} characters")]
        public string Name { get; set; }
    }
}