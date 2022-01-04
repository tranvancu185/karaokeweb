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

        public List<Room> getListRoom()
        {
            List<Room> listRoom = _context.Rooms.OrderByDescending(x => x.Id).ToList();
            return listRoom;
        }

        public List<Room> checkRoomTime(DateTime? dateBook, TimeSpan? checkIn, TimeSpan? checkOut)
        {
            List<Bill> listBill = _context.Bills.OrderByDescending(x => x.Id).Where(x => x.CheckIn <= checkIn && x.CheckOut > checkIn && x.DateBook == dateBook).Include(f => f.IdRoomNavigation).ToList();
            List<Room> roms = new List<Room>();
            foreach (Bill bi in listBill)
            {
                roms.Add(bi.IdRoomNavigation);
            }
            List<Room> listRoom = this.getListRoom();
            listRoom.RemoveAll(
                x =>
                roms.Any(
                k => k.Id == x.Id));
            return listRoom;
        }
    }
}
