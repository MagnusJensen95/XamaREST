using System;
using System.Collections.Generic;

namespace RESTXama.Models
{
    public partial class Gallery
    {
        public int Id { get; set; }
        public int PictureId { get; set; }
        public byte[] Picture { get; set; }
        public string Description { get; set; }

        public Users IdNavigation { get; set; }
    }
}
