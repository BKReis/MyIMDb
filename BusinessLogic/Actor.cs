using Api.Actor;
using BusinessLogic.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic {
    [Table("Actors")]
    public class Actor {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        [MaxLength(100)]
        [Required]
        public string Birthplace { get; set; }
        public virtual ICollection<MovieActor> MovieActors { get; set; }

        #region RETRIEVE
        public static List<Actor> SelectAll(ApplicationDbContext db) {
            return db.Actors.ToList();
        }

        public static Actor SelectById(Guid id, ApplicationDbContext db) {
            var actor = db.Actors.FirstOrDefault(a => a.Id == id);
            if (actor == null) {
                throw new Exception("The actor was not found.");
            }
            return actor;
        }

        public static List<string> CharactersRelation(Guid id, ApplicationDbContext db) {
            var characters = db.MovieActors.Where(c => c.ActorId == id).Select(m => m.Character).ToList();

            return characters;
        }
        #endregion


        #region MODEL
        public ActorModel ToModel() {
            var model = new ActorModel() {
                Id = Id,
                Name = Name,
                Birthplace = Birthplace
            };
            return model;
        }
        #endregion

        #region STATIC
        public static Actor Create(string name, string birthplace, ApplicationDbContext db) {
            if (db.Actors.Count(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) > 0) {
                throw new Exception(string.Format("The actor with name {0} already exists in the database.", name));
            }
            var actor = new Actor() {
                Id = Guid.NewGuid(),
                Name = name,
                Birthplace = birthplace
            };
            db.Actors.Add(actor);
            db.SaveChanges();
            return actor;
        }

        public static Actor Update(Guid id, string name, string birthplace, ApplicationDbContext db) {
            var actor = SelectById(id, db);
            if (db.Actors.Count(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && a.Id != id) > 0) {
                throw new Exception(string.Format("The actor with name {0} already exists in the database.", name));
            }
            actor.Name = name;
            actor.Birthplace = birthplace;

            db.SaveChanges();
            return actor;
        }
        public static Actor Delete(Guid id, ApplicationDbContext db) {
            var actor = SelectById(id, db);
            var movieActors = actor.MovieActors.ToList();
            foreach (var movieActor in movieActors) {
                db.Entry(movieActor).State = System.Data.Entity.EntityState.Deleted;
            }
            db.Entry(actor).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return actor;
        }
        #endregion
    }
}
