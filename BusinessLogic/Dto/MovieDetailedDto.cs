using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dto {
    public class MovieDetailedDto {
        public Guid Id { get; set; }
        public int Rank { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Storyline { get; set; }
        public List<ActorNameCharacterDto> Actors { get; set; }
    }
}
