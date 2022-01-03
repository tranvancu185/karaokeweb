using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Karaoke_project.Models
{
    public partial class User
    {
        public string Id { get; set; }
        public string Hoten { get; set; }
        public int? Role { get; set; }

        [DisplayName("Image Name")]
        public string Avatar { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
        public virtual Role RoleNavigation { get; set; }
    }
}
