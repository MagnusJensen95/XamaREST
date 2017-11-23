using System;
using System.Collections.Generic;

namespace RESTXama.Models
{
    public partial class Userinfo
    {
        public int Id { get; set; }
        public string Picture { get; set; }
        public string Address { get; set; }

        public Users IdNavigation { get; set; }
    }
}
