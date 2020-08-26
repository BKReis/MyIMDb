using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyImdb.ViewModels {
    public class CharacterActorListViewModel {
        [Required(ErrorMessage = "The actor Id must be provided")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Character { get; set; }

    }
}