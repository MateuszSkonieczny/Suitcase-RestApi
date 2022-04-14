using System.Collections.Generic;

namespace SuitcaseApi.Models
{
    public class Item
    {
        public Item()
        {
            SuitcaseItems = new HashSet<SuitcaseItem>();
        }
        
        public int IdItem { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public bool IsPacked { get; set; }
        
        public virtual ICollection<SuitcaseItem> SuitcaseItems { get; set; }
    }
}