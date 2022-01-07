using Karaoke_project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karaoke_project.Services
{
    public class BillServices
    {
        private readonly web_karaokeContext _context;
        public BillServices(web_karaokeContext context)
        {
            _context = context;
        }

        public List<Bill> getListBill()
        {
            List<Bill> ListBill = _context.Bills.AsNoTracking().OrderByDescending(x => x.Id).ToList();
            return ListBill;
        }

        public Bill getBillByCreateAt(DateTime? date)
        {
            string dateCre = date?.ToString("yyyy-mm-dd HH:mm:ss");
            List<Bill> ListBill =  this.getListBill();
            Bill Exist = new Bill();
            foreach(Bill bi in ListBill)
            {
                string ex = (string)bi.CreateAt?.ToString("yyyy-mm-dd HH:mm:ss");
                if (string.Equals(ex,dateCre))
                {
                    Exist = bi;
                }
            }
            return Exist;
        }

        public bool checkBookExist(Bill bill)
        {
            bool check = false;
            List<Bill> billCheck = _context.Bills.OrderByDescending(x => x.Id).Where(x => x.CheckIn <= bill.CheckIn && x.CheckOut > bill.CheckIn && x.DateBook == bill.DateBook && x.IdRoom == bill.IdRoom).ToList();
            if(billCheck.Count > 0)
            {
                check = true;
            }
            return check;
        }

        public List<Bill> getBillStartEnd(DateTime? startDate, DateTime? endDate)
        {
            List<Bill> billList = _context.Bills.OrderByDescending(x => x.Id).Include(b => b.IdCusNavigation).Include(b => b.IdRoomNavigation).Where(x => x.DateBook <= endDate && x.DateBook >= startDate).ToList();

            return billList;
        }

        public Bill getBillById(int id)
        {
            Bill bill = _context.Bills.AsNoTracking().OrderByDescending(x => x.Id).FirstOrDefault(x=>x.Id == id);

            return bill;
        }


        public async Task updateBill(Bill bill)
        {
            _context.Update(bill);
            await _context.SaveChangesAsync();
        }
    }
}
