using Api.Comment;
using BusinessLogic.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic {
    [Table("Comments")]
    public class Comment {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Value { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        [Required]
        public Guid MovieId { get; set; }
        public virtual Movie Movie { get; set; }

        #region MODEL
        public CommentModel ToModel() {
            var model = new CommentModel() {
                Id = Id,
                Value = Value,
                User = User.Email,
                Movie = Movie.Title,
                SelectedMovieId = Movie.Id,
                SelectedUserId = User.Id
            };
            return model;
        }
        #endregion

        #region RETRIEVE
        public static Comment SelectById(Guid id, ApplicationDbContext db) {
            var comment = db.Comments.FirstOrDefault(c => c.Id == id);
            if (comment == null) {
                throw new Exception("The comment was not found.");
            }
            return comment;
        }
        #endregion
        #region STATIC
        public static Comment Create(string value, Guid movieId, Guid userId, ApplicationDbContext db) {
            var comment = new Comment() {
                Id = Guid.NewGuid(),
                UserId = userId,
                MovieId = movieId,
                Value = value
            };
            db.Comments.Add(comment);
            db.SaveChanges();
            return comment;
        }

        public static Comment Update(Guid id, string value, Guid movieId, Guid userId, ApplicationDbContext db) {
            var comment = SelectById(id, db);
            comment.Value = value;
            comment.MovieId = movieId;
            comment.UserId = userId;

            db.SaveChanges();
            return comment;
        }

        public static Comment Delete(Guid id, ApplicationDbContext db) {
            var comment = SelectById(id, db);

            db.Entry(comment).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return comment;
        }
        #endregion
    }
}
