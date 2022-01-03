using Karaoke_project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karaoke_project.Services
{
    public class RoomsService
    {
        private readonly web_karaokeContext _context;
        public RoomsService(web_karaokeContext context)
        {
            _context = context;
        }

        public Room getRoomById(int id)
        {
            Room Exist = _context.Rooms.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return Exist;
        }
    }
}
