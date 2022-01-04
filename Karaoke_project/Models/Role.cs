using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Karaoke_project.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên quyền truy cập!")]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
