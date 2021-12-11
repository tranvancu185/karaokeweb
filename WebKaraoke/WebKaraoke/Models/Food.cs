using System;
using System.Collections.Generic;

#nullable disable

namespace WebKaraoke.Models
{
    public partial class Food
    {
        public Food()
        {
            Bills = new HashSet<Bill>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}
