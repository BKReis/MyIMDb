using Api.MovieActor;
using BusinessLogic.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic {
    [Table("MovieActors")]
    public class MovieActor {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Character { get; set; }
        [Required]
        public Guid MovieId { get; set; }
        public virtual Movie Movie { get; set; }
        [Required]
        public Guid ActorId { get; set; }
        public virtual Actor Actor { get; set; }


        #region MODEL
        public MovieActorModel ToModel() {
            var model = new MovieActorModel() {
                Id = Id,
                Character = Character,
                Name = Actor.Name,
                SelectedActorId = ActorId,
                SelectedMovieId = MovieId  
            };
            return model;
        }
        #endregion

        #region RETRIEVE
        public static MovieActor SelectById(Guid id, ApplicationDbContext db) {
            var character = db.MovieActors.FirstOrDefault(m => m.Id == id);
            if (character == null) {
                throw new Exception("The character was not found.");
            }
            return character;
        }
        #endregion
        #region STATIC
        public static MovieActor Create(string character, Guid movieId, Guid actorId, ApplicationDbContext db) {
            var movieActor = new MovieActor() {
                Id = Guid.NewGuid(),
                ActorId = actorId,
                MovieId = movieId,
                Character = character
            };
            db.MovieActors.Add(movieActor);
            db.SaveChanges();
            return movieActor;
        }

        public static MovieActor Update(Guid id, string name, Guid movieId, Guid actorId, ApplicationDbContext db) {
            var character = SelectById(id, db);
            character.Character = name;
            character.MovieId = movieId;
            character.ActorId = actorId;
          
            db.SaveChanges();
            return character;
        }

        public static MovieActor Delete(Guid id, ApplicationDbContext db) {
            var character = SelectById(id, db);

            db.Entry(character).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return character;
        }
        #endregion
    }
}
