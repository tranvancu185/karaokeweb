using System;
using System.Collections.Generic;

#nullable disable

namespace WebKaraoke.Models
{
    public partial class User
    {
        public string Id { get; set; }
        public string Hoten { get; set; }
        public int? Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
