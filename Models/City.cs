using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity_Assignment.Models
{
    public class City : DbObject
    {
        [Required]
        public int CountryId { get; set; }
        
        public virtual Country Country { get; set; }
    }
}