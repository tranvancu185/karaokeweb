using Karaoke_project.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karaoke_project.Services
{
    public class AdminServices
    {
        private readonly web_karaokeContext _context;
        public string userId;
        public AdminServices(web_karaokeContext context)
        {
            _context = context;
        }
    }
}
