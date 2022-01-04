using Karaoke_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karaoke_project.Services
{
    public class BillDetailSevices
    {
        private readonly web_karaokeContext _context;
        public BillDetailSevices(web_karaokeContext context)
        {
            _context = context;
        }

        public async Task addBillDetail(BillDetail billDetail)
        {
            _context.Add(billDetail);
            await _context.SaveChangesAsync();
        }
    }
}
