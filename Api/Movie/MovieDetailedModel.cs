using Api.Actor;
using Api.Comment;
using Api.MovieActor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Movie {
    public class MovieDetailedModel {
        public Guid Id { get; set; }
        public int Rank { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public Guid SelectedGenreId { get; set; }
        public string Genre { get; set; }
        public string Storyline { get; set; }
        public List<MovieActorModel> Actors { get; set; }
        public List<CommentModel> Comments { get; set; }
    }
}