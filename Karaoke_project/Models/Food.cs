using System;
using System.Collections.Generic;

#nullable disable

namespace Karaoke_project.Models
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
        public int? IdCategory { get; set; }

        public virtual Category IdCategoryNavigation { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
    }
}
