using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Api.Actor {
    public class ActorModel {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Birthplace { get; set; }
    }
}