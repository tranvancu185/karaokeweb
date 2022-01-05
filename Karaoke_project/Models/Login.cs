using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Karaoke_project.Models
{
    public class Login
    {
        [Key]
        public string Username { get; set; }

        public string password { get; set; }
    }
}
