using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Api.Account {
    public class AccountInfoResponse {
        public Guid Id { get; set; }
        public string Username { get; set; }
    }
}