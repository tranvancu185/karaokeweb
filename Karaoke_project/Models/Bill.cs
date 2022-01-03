using System;
using System.Collections.Generic;

#nullable disable

namespace Karaoke_project.Models
{
    public partial class Bill
    {
        public Bill()
        {
            BillDetails = new HashSet<BillDetail>();
        }

        public int Id { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? DateBook { get; set; }
        public TimeSpan? CheckIn { get; set; }
        public TimeSpan? CheckOut { get; set; }
        public double? Total { get; set; }
        public int? Status { get; set; }
        public int? IdRoom { get; set; }
        public int? IdCus { get; set; }

        public virtual Customer IdCusNavigation { get; set; }
        public virtual Room IdRoomNavigation { get; set; }
        public virtual ICollection<BillDetail> BillDetails { get; set; }
    }
}
