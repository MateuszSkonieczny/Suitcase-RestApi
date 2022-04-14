using System.Collections.Generic;

namespace SuitcaseApi.Models
{
    public class User
    {
        public User()
        {
            Suitcases = new HashSet<Suitcase>();
        }
        
        public int IdUser { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string RefreshToken { get; set; }
        
        public virtual ICollection<Suitcase> Suitcases { get; set; }
    }
}