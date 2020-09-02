using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.MovieActor {
    public class MovieActorModel {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Character { get; set; }
        public Guid SelectedActorId { get; set; }
        public Guid SelectedMovieId { get; set; }
    }
}