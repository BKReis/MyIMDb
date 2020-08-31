using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Api.Genre {
    public class GenrePostRequest {
        [Required(ErrorMessage = "The name of the genre is required")]
        [MaxLength(100, ErrorMessage = "The name can't be greater than {1} characters")]
        public string Name { get; set; }
    }
}