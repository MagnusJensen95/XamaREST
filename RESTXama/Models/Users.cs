using System;
using System.Collections.Generic;

namespace RESTXama.Models
{
    public partial class Users
    {
        public Users()
        {
           
            Gallery = new HashSet<Gallery>();
            Prices = new HashSet<Prices>();
           
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
     
        public Userinfo Userinfo { get; set; }
        public ICollection<Gallery> Gallery { get; set; }
        public ICollection<Prices> Prices { get; set; }
      
    }
}
