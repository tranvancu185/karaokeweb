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
            string dateCre = date?.ToString("yyyy-mm-dd HH:mm:ss.fff");
            List<Bill> ListBill = this.getListBill();
            Bill Exist = new Bill();
            foreach(Bill bi in ListBill)
            {
                string ex = (string)bi.CreateAt?.ToString("yyyy-mm-dd HH:mm:ss.fff");
                if (ex == dateCre)
                {
                    Exist = bi;
                }
                else
                {
                }
            }

            return Exist;
        }
    }
}
