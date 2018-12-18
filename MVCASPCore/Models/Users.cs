using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCASPCore.Models
{
    public partial class Users
    {
        public Users()
        {
            Relative = new HashSet<Relative>();
        }

        [Key]
        public int UId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }

        public ICollection<Relative> Relative { get; set; }
    }
}
