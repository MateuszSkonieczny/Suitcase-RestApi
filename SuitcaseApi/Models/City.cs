using System.Collections.Generic;

namespace SuitcaseApi.Models
{
    public class City
    {
        public City()
        {
            Suitcases = new HashSet<Suitcase>();
        }
        
        public int IdCity { get; set; }
        public string Name { get; set; }
        public int IdCountry { get; set; }
        
        public virtual Country IdCountryNavigation { get; set; }
        public virtual ICollection<Suitcase> Suitcases { get; set; }
    }
}