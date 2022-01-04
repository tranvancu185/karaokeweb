using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Karaoke_project.Models
{
    public partial class Food
    {
        public Food()
        {
            BillDetails = new HashSet<BillDetail>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên món ăn!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá món ăn!")]
        public double? Price { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số lượng món ăn!")]
        public int? Quantity { get; set; }
        public string Image { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
        public int? IdCategory { get; set; }

        public virtual Category IdCategoryNavigation { get; set; }
        public virtual ICollection<BillDetail> BillDetails { get; set; }
    }
}
