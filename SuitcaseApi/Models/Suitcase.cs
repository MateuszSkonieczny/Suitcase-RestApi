using System.Collections.Generic;

namespace SuitcaseApi.Models
{
    public class Suitcase
    {
        public Suitcase()
        {
            SuitcaseItems = new HashSet<SuitcaseItem>();
        }
        
        public int IdSuitcase { get; set; }
        public string Name { get; set; }
        public int IdCity { get; set; }
        public int IdUser { get; set; }
        
        public virtual User IdUserNavigation { get; set; }
        public virtual City IdCityNavigation { get; set; }
        public virtual ICollection<SuitcaseItem> SuitcaseItems { get; set; }
    }
}