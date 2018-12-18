using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCASPCore.Models
{
    //SqlConnection conn = new SqlConnection("");
    //var db = data.Open("SmallBakery");
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    ////super cool that you can do this in c# no need to build out the constructor or getter setters
    //public class Relative
    //{
    //    [Key]
    //    public int rel_id { get; set; }
    //    public string F_Name { get; set; }
    //    public string L_Name { get; set; }
    //    public string Relation { get; set; }
    //    [ForeignKey("Users")]
    //    public int u_id { get; set; }
    //}

    //public class Users
    //{
    //    [Key]
    //    public int rel_id { get; set; }
    //    public string F_Name { get; set; }
    //    public string L_Name { get; set; }
    //    public string email { get; set; }
    //    public string gender { get; set; }
    //}
}