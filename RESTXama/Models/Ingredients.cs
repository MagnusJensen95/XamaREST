using System;
using System.Collections.Generic;

namespace RESTXama.Models
{
    public partial class Ingredients
    {
        public int ProductId { get; set; }
        public string IngredientName { get; set; }
        public string Amount { get; set; }

        public Prices Product { get; set; }
    }
}
