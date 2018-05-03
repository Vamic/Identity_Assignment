using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Identity_Assignment.Models
{
    public class Person : DbObject
    {
        [Required]
        public int CityId { get; set; }
        
        public virtual City City { get; set; }
    }
}