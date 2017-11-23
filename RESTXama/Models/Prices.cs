using System;
using System.Collections.Generic;

namespace RESTXama.Models
{
    public partial class Prices
    {
        public Prices()
        {
            Ingredients = new HashSet<Ingredients>();
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Picture { get; set; }

        public Users IdNavigation { get; set; }
        public ICollection<Ingredients> Ingredients { get; set; }
    }
}
