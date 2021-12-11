using System;
using System.Collections.Generic;

#nullable disable

namespace WebKaraoke.Models
{
    public partial class BillDetail
    {
        public int? IdFood { get; set; }
        public int? IdBill { get; set; }
        public int? Quantuty { get; set; }

        public virtual Bill IdBillNavigation { get; set; }
        public virtual Food IdFoodNavigation { get; set; }
    }
}
