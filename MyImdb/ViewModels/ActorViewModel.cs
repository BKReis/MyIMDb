using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyImdb.ViewModels {
    public class ActorViewModel {
        [Required(ErrorMessage = "The actor Id must be provided")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The name of the actor is required")]
        [MaxLength(100, ErrorMessage = "The name can't be greater than {1} characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The birthplace of the actor is required")]
        [MaxLength(100, ErrorMessage = "The name can't be greater than {1} characters")]
        public string Birthplace { get; set; }
    }
}