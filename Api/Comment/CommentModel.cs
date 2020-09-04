using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Api.Comment {
    public class CommentModel {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public string User { get; set; }
        public string Movie { get; set; }
        public Guid SelectedMovieId { get; set; }
        public Guid SelectedUserId { get; set; }

    }
}