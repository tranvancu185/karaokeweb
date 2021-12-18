using System;
using System.Collections.Generic;

#nullable disable

namespace Karaoke_project.Models
{
    public partial class Room
    {
        public Room()
        {
            Bills = new HashSet<Bill>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Status { get; set; }
        public double? Price { get; set; }
        public int? TypeRoom { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}
