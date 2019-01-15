using System;
using System.Collections.Generic;

namespace Benefacts.Models
{
    public partial class Admin
    {
        public int AId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
