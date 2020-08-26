using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dto {
    public class MovieSimpleDto {
        public Guid Id { get; set; }
        public int Rank { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
    }
}
