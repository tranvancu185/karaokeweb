using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Karaoke_project.Models
{
    public partial class User
    {
        [Required(ErrorMessage = "Vui lòng nhập Id Code người dùng!")]
        [StringLength(20, ErrorMessage = "Id Code dài tối đa 20 kí tự!")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên người dùng!")]
        public string Hoten { get; set; }
        public int? Role { get; set; }

        [DisplayName("Image Name")]
        public string Avatar { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập username người dùng!")]
        [StringLength(20, ErrorMessage = "Username dài tối đa 20 kí tự!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập password!")]
        public string Password { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
        public virtual Role RoleNavigation { get; set; }
    }
}
