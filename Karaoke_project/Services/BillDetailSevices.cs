using Karaoke_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        public bool deleteBillDetailRange(int id)
        {
            bool check = false;
            List<BillDetail> billCheck = _context.BillDetails.OrderByDescending(x => x.Id).Where(x=>x.IdBill==id).ToList();
            if (billCheck.Count > 0)
            {
                _context.BillDetails.RemoveRange(_context.BillDetails.Where(x => x.IdBill == id));
                _context.SaveChanges();
                check = true;
            }
            Thread.Sleep(2000);
            return check;
        }

        public List<BillDetail> getListFoodBill(int id)
        {
            List<BillDetail> billCheck = _context.BillDetails.OrderByDescending(x => x.Id).Where(x => x.IdBill == id).ToList();
            
            return billCheck;
        }
    }
}
