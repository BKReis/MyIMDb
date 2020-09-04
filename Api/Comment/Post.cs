using Api.Actor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Api.Comment {
    public class CommentPostRequest {
        [Required(ErrorMessage = "Comment is required")]
        [MaxLength(200, ErrorMessage = "The comment can't be greater than {1} characters")]
        [DisplayName("Comment")]
        public string Value { get; set; }
        [DisplayName("User")]
        [Required(ErrorMessage = "The user must be selected.")]
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "The movie must be selected.")]
        public Guid SelectedMovieId { get; set; }
    }
}