using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Karaoke_project.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Bills = new HashSet<Bill>();
        }

        public int Id { get; set; }

        public string Hoten { get; set; }

        public string Phone { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}
