using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Karaoke_project.Models;
using Microsoft.AspNetCore.Http;

namespace Karaoke_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BillsController : Controller
    {
        private readonly web_karaokeContext _context;
        public string userId;
        public BillsController(web_karaokeContext context)
        {
            _context = context;
        }

        // GET: Admin/Bills
        public async Task<IActionResult> Index()
        {
            userId = HttpContext.Session.GetString("UserId");
            User signed = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).FirstOrDefault(x => x.Id == userId);
            ViewData["UserAvatar"] = signed.Avatar;
            ViewData["UserRole"] = signed.Role;
            ViewData["UserName"] = signed.Hoten;
            ViewData["UserId"] = signed.Id;
            var web_karaokeContext = _context.Bills.Include(b => b.IdCusNavigation).Include(b => b.IdRoomNavigation);
            return View(await web_karaokeContext.ToListAsync());
        }

        // GET: Admin/Bills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            userId = HttpContext.Session.GetString("UserId");
            User signed = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).FirstOrDefault(x => x.Id == userId);
            ViewData["UserAvatar"] = signed.Avatar;
            ViewData["UserRole"] = signed.Role;
            ViewData["UserName"] = signed.Hoten;
            ViewData["UserId"] = signed.Id;
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.IdCusNavigation)
                .Include(b => b.IdRoomNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bill == null)
            {
                return NotFound();
            }
            List<BillDetail> billDetail = _context.BillDetails.AsNoTracking().Where(x => x.IdBill == bill.Id).Include(f => f.IdFoodNavigation).OrderByDescending(x => x.Id).ToList();
            List<Food> listFoodBill = new List<Food>();
            foreach (BillDetail bil in billDetail)
            {
                Console.WriteLine(bil.IdFoodNavigation.Price);
                listFoodBill.Add(bil.IdFoodNavigation);
            }
            ViewData["foodBill"] = listFoodBill;
            ViewData["foodDetail"] = billDetail;
            return View(bill);
        }

        // GET: Admin/Bills/Create
        public IActionResult Create()
        {
            userId = HttpContext.Session.GetString("UserId");
            User signed = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).FirstOrDefault(x => x.Id == userId);
            ViewData["UserAvatar"] = signed.Avatar;
            ViewData["UserRole"] = signed.Role;
            ViewData["UserName"] = signed.Hoten;
            ViewData["UserId"] = signed.Id;

            ViewData["IdCus"] = new SelectList(_context.Customers, "Id", "Hoten");
            ViewData["IdRoom"] = new SelectList(_context.Rooms, "Id", "Name");
            return View();
        }

        // POST: Admin/Bills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreateAt,DateBook,CheckIn,CheckOut,Total,Status,IdRoom,IdCus")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCus"] = new SelectList(_context.Customers, "Id", "Hoten", bill.IdCus);
            ViewData["IdRoom"] = new SelectList(_context.Rooms, "Id", "Name", bill.IdRoom);

            userId = HttpContext.Session.GetString("UserId");
            User signed = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).FirstOrDefault(x => x.Id == userId);
            ViewData["UserAvatar"] = signed.Avatar;
            ViewData["UserRole"] = signed.Role;
            ViewData["UserName"] = signed.Hoten;
            ViewData["UserId"] = signed.Id;
            return View(bill);
        }

        // GET: Admin/Bills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }

            List<BillDetail> billDetail = _context.BillDetails.AsNoTracking().Where(x => x.IdBill == bill.Id).Include(f => f.IdFoodNavigation).OrderByDescending(x => x.Id).ToList();
            List<Food> listFoodBill = new List<Food>();
            foreach(BillDetail bil in billDetail)
            {
                Console.WriteLine(bil.IdFoodNavigation.Price);
                listFoodBill.Add(bil.IdFoodNavigation);
            }

            ViewData["foodBill"] = listFoodBill;
            ViewData["foodDetail"] = billDetail;
            ViewData["IdCus"] = new SelectList(_context.Customers, "Id", "Hoten", bill.IdCus);
            ViewData["IdRoom"] = new SelectList(_context.Rooms, "Id", "Name", bill.IdRoom);

            userId = HttpContext.Session.GetString("UserId");
            User signed = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).FirstOrDefault(x => x.Id == userId);
            ViewData["UserAvatar"] = signed.Avatar;
            ViewData["UserRole"] = signed.Role;
            ViewData["UserName"] = signed.Hoten;
            ViewData["UserId"] = signed.Id;
            return View(bill);
        }

        // POST: Admin/Bills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Status,IdRoom,IdCus")] Bill bill)
        {
            if (id != bill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(bill.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            userId = HttpContext.Session.GetString("UserId");
            User signed = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).FirstOrDefault(x => x.Id == userId);
            ViewData["UserAvatar"] = signed.Avatar;
            ViewData["UserRole"] = signed.Role;
            ViewData["UserName"] = signed.Hoten;
            ViewData["UserId"] = signed.Id;

            ViewData["IdCus"] = new SelectList(_context.Customers, "Id", "Hoten", bill.IdCus);
            ViewData["IdRoom"] = new SelectList(_context.Rooms, "Id", "Name", bill.IdRoom);
            return View(bill);
        }

        // GET: Admin/Bills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.IdCusNavigation)
                .Include(b => b.IdRoomNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bill == null)
            {
                return NotFound();
            }
            userId = HttpContext.Session.GetString("UserId");
            User signed = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).FirstOrDefault(x => x.Id == userId);
            ViewData["UserAvatar"] = signed.Avatar;
            ViewData["UserRole"] = signed.Role;
            ViewData["UserName"] = signed.Hoten;
            ViewData["UserId"] = signed.Id;
            return View(bill);
        }

        // POST: Admin/Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bill = await _context.Bills.FindAsync(id);
            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillExists(int id)
        {
            return _context.Bills.Any(e => e.Id == id);
        }
    }
}
