using System.Collections.Generic;

namespace Identity_Assignment.Models
{
    public class Country : DbObject
    { 
        public virtual List<City> Cities { get; set; }

        public Country()
        {
            Cities = new List<City>();
        }
    }
}