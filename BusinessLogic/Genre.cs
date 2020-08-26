using BusinessLogic.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic {
    [Table("Genres")]
    public class Genre {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
      
        public virtual ICollection<Movie> Movies { get; set; }


        #region RETRIEVE
        public static List<Genre> SelectAll(ApplicationDbContext db) {
            return db.Genres.ToList();
        }

        public static Genre SelectById(Guid id, ApplicationDbContext db) {
            var genre = db.Genres.FirstOrDefault(g => g.Id == id);
            if (genre == null) {
                throw new Exception("The genre was not found.");
            }
            return genre;
        }

        public static List<String> MovieTitles(Guid id, ApplicationDbContext db) {
            var movies = db.Movies.Where(m => m.Genre.Id == id).ToList();

            return movies.Select(m => (string)m.Title).ToList();
        }
        #endregion


        #region STATIC
        public static Genre Create(string name, ApplicationDbContext db) {
            if (db.Genres.Count(m => m.Name == name) > 0) {
                throw new Exception(string.Format("The genre with name {0} already exists.", name));
            }
             var genre = new Genre() {
                Id = Guid.NewGuid(),
                Name = name
            };
            db.Genres.Add(genre);
            db.SaveChanges();
            return genre;
        }

        public static Genre Update(Guid id, string name, ApplicationDbContext db) {
            var genre = SelectById(id, db);
            if (db.Genres.Count(g => g.Name == name && g.Id != id) > 0) {
                throw new Exception(string.Format("The genre {0} already exists.", name));
            }
            genre.Name = name;
            db.SaveChanges();
            return genre;
        }

        public static Genre Delete(Guid id, ApplicationDbContext db) {
            var genre = SelectById(id, db);
            var movies = genre.Movies.ToList();
            foreach (var movie in movies) {
                db.Entry(movie).State = System.Data.Entity.EntityState.Deleted;
            }
            db.Entry(genre).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return genre;
        }
        #endregion


    }
}
