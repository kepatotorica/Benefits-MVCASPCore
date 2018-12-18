using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCASPCore.Models
{
    public partial class Relative
    {
        [Key]
        public int RelId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Relation { get; set; }
        public int? UId { get; set; }

        public Users U { get; set; }
    }
}
