using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Vui lòng nhập tên phòng!")]
        public string Name { get; set; }
        public int? Status { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá tiền phòng!")]
        public double? Price { get; set; }
        public int? TypeRoom { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}
