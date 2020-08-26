using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyImdb.ViewModels {
    public class ActorCreateViewModel {
        [Required(ErrorMessage = "Name of the actor is required")]
        [MaxLength(100, ErrorMessage = "Name of the actor can't be greater than {1} characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Birthplace of the actor is required")]
        [MaxLength(100, ErrorMessage = "Birthplace of the actor can't be greater than {1} characters")]
        public string Birthplace { get; set; }

    }
}