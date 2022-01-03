using Karaoke_project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karaoke_project.Services
{
    public class CustomersService
    {
        private readonly web_karaokeContext _context;
        public CustomersService(web_karaokeContext context)
        {
            _context = context;
        }

        public Customer getCustomerByPhone(string Phone)
        {
            Customer Exist = _context.Customers.AsNoTracking().FirstOrDefault(x => x.Phone == Phone);
            return Exist;
        }
    }
}
