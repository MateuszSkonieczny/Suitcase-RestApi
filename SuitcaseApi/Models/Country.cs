using System.Collections.Generic;

namespace SuitcaseApi.Models
{
    public class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }

        public int IdCountry { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<City> Cities { get; set; }
    }
}