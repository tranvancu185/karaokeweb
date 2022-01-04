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
        [Required(ErrorMessage = "Vui lòng nhập họ tên khách hàng!")]
        public string Hoten { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại khách hàng!")]
        [StringLength(11, ErrorMessage = "Số điện thoại dài tối đa 11 kí tự!")]
        public string Phone { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}
