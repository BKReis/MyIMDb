using BusinessLogic.Data;
using BusinessLogic.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Api.Movie;

namespace BusinessLogic {
    [Table("Movies")]
    public class Movie {
        [Key]
        public Guid Id { get; set; }
        public int Rank { get; set; }
        [MaxLength(100)]
        [Required]
        public string Title { get; set; }
        public int Year { get; set; }
        [MaxLength(200)]
        public string Storyline { get; set; }
        public virtual ICollection<MovieActor> MovieActors { get; set; }

        [Required]
        public Guid GenreId { get; set; }
        public virtual Genre Genre { get; set; }
        public DateTime CreationDateUtc { get; set; }
        [NotMapped]
        public DateTimeOffset CreationDate {
            get {
                return new DateTimeOffset(CreationDateUtc, TimeSpan.Zero);
            }
            set {
                CreationDateUtc = value.UtcDateTime;
            }
        }

        public MovieSimpleModel ToModel() {
            var model = new MovieSimpleModel() {
                Id = Id,
                Title = Title,
                Rank = Rank,
                Year = Year,
                Genre = Genre.Name
            };
            return model;
        }
        #region RETRIEVE
        public static List<Movie> SelectAll(ApplicationDbContext db) {
            var movies = db.Movies.ToList();
            return movies;
        }

        public static List<MovieSimpleModel> SelectSimple(ApplicationDbContext db) {
            var result = from m in db.Movies
                         join g in db.Genres on m.GenreId equals g.Id
                         select new MovieSimpleModel {
                             Id = m.Id,
                             Title = m.Title,
                             Rank = m.Rank,
                             Year = m.Year,
                             Genre = g.Name
                         };
            return result.ToList();
        }

        public static MovieDetailedDto DetailedMovie(Guid id, ApplicationDbContext db) {
            var movie = db.Movies.FirstOrDefault(m => m.Id == id); // include and theninclude
            if (movie == null) {
                throw new Exception("The movie was not found.");
            }
            var result = new MovieDetailedDto {
                Id = movie.Id,
                Genre = movie.Genre.Name,
                Title = movie.Title,
                Rank = movie.Rank,
                Year = movie.Year,
                Storyline = movie.Storyline
            };
            result.Actors = new List<ActorNameCharacterDto>();
            foreach(var movieActor in movie.MovieActors) {
                result.Actors.Add(new ActorNameCharacterDto() { 
                    Id = movieActor.Id,
                    Name = movieActor.Actor.Name,
                    Character = movieActor.Character
                });
            };
            return result;
        }

        //public static List<Movie> IncludeSelectAll(ApplicationDbContext db) {
        //    var result = db.Movies
        //        .Include(m => m.Genres)
        //        .ToList();
        //    return result.ToList();
        //}


        public static Movie SelectById(Guid id, ApplicationDbContext db) {
            var movie = db.Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null) {
                throw new Exception("The movie was not found.");
            }
            return movie;
        }

        public static List<String> CharactersRelation(Guid id, ApplicationDbContext db) {
            var characters = db.MovieActors.Where(c => c.MovieId == id).ToList();
            return characters.Select(m => (string)m.Character).ToList();
        }
        #endregion

        #region STATIC
        public static Movie Create(int rank, string title, int year, string storyline, Guid genreId, ApplicationDbContext db) {
            if (db.Movies.Count(m => m.Title == title) > 0) {
                throw new Exception(string.Format("The movie with title {0} already exists.", title));
            }
            if (db.Genres.Count(g => g.Id == genreId) < 1) {
                throw new Exception("The selected genre was not found.");
            }
            var movie = new Movie() {
                CreationDate = DateTimeOffset.Now,
                Id = Guid.NewGuid(),
                Rank = rank,
                Title = title,
                Year = year,
                Storyline = storyline,
                GenreId = genreId
            };
            db.Movies.Add(movie);
            db.SaveChanges();
            return movie;
        }

        public static Movie Update(Guid id, int rank, string title, int year, string storyline, Guid genreId, ApplicationDbContext db) {
            var movie = SelectById(id, db);
            if (db.Movies.Count(m => m.Title.Equals(title, StringComparison.OrdinalIgnoreCase) && m.Id != id) > 0) {
                throw new Exception(string.Format("The movie {0} already exists.", title));
            }
            if (db.Movies.Count(m => m.Rank == rank && m.Id != id) > 0) {
                throw new Exception(string.Format("A movie with rank {0} already exists.", rank));
            }
  
            movie.Title = title;
            movie.Rank = rank;
            movie.Year = year;
            movie.Storyline = storyline;
            movie.GenreId = genreId;

            db.SaveChanges();
            return movie;
        }

        public static Movie Delete(Guid id, ApplicationDbContext db) {
            var movie = SelectById(id, db);
            var movieActors = movie.MovieActors.ToList();
            foreach (var movieActor in movieActors) {
                db.Entry(movieActor).State = System.Data.Entity.EntityState.Deleted;
            }
            db.Entry(movie).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return movie;
        }
        #endregion
    }
}
