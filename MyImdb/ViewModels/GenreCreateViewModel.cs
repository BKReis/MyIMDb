using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyImdb.ViewModels {
    public class GenreCreateViewModel {

            [Required(ErrorMessage = "Name of the genre is required")]
            [MaxLength(20, ErrorMessage = "Name of the genre can't be greater than {1} characters")]
            public string Name { get; set; }
            
        
    }
}
