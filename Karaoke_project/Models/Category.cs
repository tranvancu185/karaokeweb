using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Karaoke_project.Models
{
    public partial class Category
    {
        public Category()
        {
            Foods = new HashSet<Food>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên loại thức ăn!")]
        public string Name { get; set; }

        public virtual ICollection<Food> Foods { get; set; }
    }
}
