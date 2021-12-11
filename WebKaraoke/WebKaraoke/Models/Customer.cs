using System;
using System.Collections.Generic;

#nullable disable

namespace WebKaraoke.Models
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
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}
