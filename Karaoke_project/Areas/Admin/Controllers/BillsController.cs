using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Karaoke_project.Models;
using Microsoft.AspNetCore.Http;
using Karaoke_project.Services;
using System.IO;
using Newtonsoft.Json;
using System.Threading;
using Rotativa;

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
        [Route("listbill", Name = "DanhSachHoaDon")]
        public async Task<IActionResult> Index()
        {
            // Check User logged in
            userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            User signed = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).FirstOrDefault(x => x.Id == userId);
            ViewData["UserAvatar"] = signed.Avatar;
            ViewData["UserRole"] = signed.Role;
            ViewData["UserName"] = signed.Hoten;
            ViewData["UserId"] = signed.Id;
            // Get day of month and last date of month
            DateTime dt = DateTime.Now;
            DateTime firstDayOfMonth = new DateTime(dt.Year, dt.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            // Get list Bill
            var web_karaokeContext = _context.Bills.Include(b => b.IdCusNavigation).Include(b => b.IdRoomNavigation).Where(x=>x.DateBook>= firstDayOfMonth && x.DateBook <= lastDayOfMonth);
            return View(await web_karaokeContext.ToListAsync());
        }

        // GET: Admin/Bills/Details/5
        [Route("billdetail", Name = "ChiTietHoaDon")]
        public async Task<IActionResult> Details(int? id)
        {
            // Check User logged in
            userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            User signed = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).FirstOrDefault(x => x.Id == userId);
            ViewData["UserAvatar"] = signed.Avatar;
            ViewData["UserRole"] = signed.Role;
            ViewData["UserName"] = signed.Hoten;
            ViewData["UserId"] = signed.Id;
            // Get detail Bill and return view
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
                listFoodBill.Add(bil.IdFoodNavigation);
            }
            ViewData["foodBill"] = listFoodBill;
            ViewData["foodDetail"] = billDetail;
            return View(bill);
        }

        // GET: Admin/Bills/Create
        public IActionResult Create()
        {
            // Check User logged in
            userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
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
            // Check User logged in
            userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            User signed = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).FirstOrDefault(x => x.Id == userId);
            ViewData["UserAvatar"] = signed.Avatar;
            ViewData["UserRole"] = signed.Role;
            ViewData["UserName"] = signed.Hoten;
            ViewData["UserId"] = signed.Id;
            // Create Bill
            if (ModelState.IsValid)
            {
                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCus"] = new SelectList(_context.Customers, "Id", "Hoten", bill.IdCus);
            ViewData["IdRoom"] = new SelectList(_context.Rooms, "Id", "Name", bill.IdRoom);
            return View(bill);
        }

        // GET: Admin/Bills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Check User logged in
            userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            User signed = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).FirstOrDefault(x => x.Id == userId);
            ViewData["UserAvatar"] = signed.Avatar;
            ViewData["UserRole"] = signed.Role;
            ViewData["UserName"] = signed.Hoten;
            ViewData["UserId"] = signed.Id;
            // Edit Bill and return view
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
            return View(bill);
        }

        // POST: Admin/Bills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit()
        {
            // Get data form request body
            string body = "";
            using (StreamReader stream = new StreamReader(Request.Body))
            {
                body = await stream.ReadToEndAsync();
            }
            dynamic json = JsonConvert.DeserializeObject(body);
            dynamic data = json.cus;
            // Update data Bill
            BillServices dao = new BillServices(_context);
            Bill bill = dao.getBillById((int)data.idBill);
            //Update data Bill Detail
            BillDetailSevices billDetailDAO = new BillDetailSevices(_context);
            bool check =  billDetailDAO.deleteBillDetailRange(bill.Id);
            bill.Status = data.status;
            bill.Total = data.totalBook;
            await dao.updateBill(bill);
            foreach (dynamic foo in data.foodBook)
            {
                BillDetail newBillDetail = new BillDetail();
                newBillDetail.IdBill = bill.Id;
                newBillDetail.IdFood = (int)foo.id;
                newBillDetail.Quantity = (int)foo.quanBook;
                _context.BillDetails.Add(newBillDetail);
                Thread.Sleep(500);
            }
            _context.SaveChanges();
            return Json(new { status = "Success" });
        }

        // GET: Admin/Bills/Delete/5

        public async Task<IActionResult> Delete(int? id)
        {
            // Check user logged in
            userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            User signed = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).FirstOrDefault(x => x.Id == userId);
            ViewData["UserAvatar"] = signed.Avatar;
            ViewData["UserRole"] = signed.Role;
            ViewData["UserName"] = signed.Hoten;
            ViewData["UserId"] = signed.Id;
            // Validate params id and delete bill
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
            return View(bill);
        }

        // POST: Admin/Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Delete Bill detail by id Bill
            BillDetailSevices dao = new BillDetailSevices(_context);
            bool check = dao.deleteBillDetailRange(id);
            // Delete Bill
            var bill = await _context.Bills.FindAsync(id);
            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Bills/getRoomByTime
        // @params DateTime startDate, DateTime endDate
        // @output Json list bill by startDate and endDate
        public IActionResult getRoomByTime(DateTime? startDate, DateTime? endDate)
        {
            BillServices BillDAO = new BillServices(_context);
            List<Bill> BillList = BillDAO.getBillStartEnd(startDate, endDate);
            return Json(new { status = "Success", data = BillList });
        }

        // GET: Admin/Bills/getBillStartEnd
        // @params DateTime startDate, DateTime endDate
        // @output Json list bill by startDate and endDate
        public IActionResult getBillStartEnd(DateTime? startDate, DateTime? endDate)
        {
            List<Bill> listBill = new List<Bill>();
            // Validate params endDate must be > starDate
            if (startDate > endDate)
            {
                return Json(new { status = "Failed", message = "Ngày kết thúc phải lớn hơn ngày bắt đầu!" });
            }
            else
            {
                BillServices BillDAO = new BillServices(_context);
                listBill = BillDAO.getBillStartEnd(startDate, endDate);
            }
            return Json(new { status = "Success", data = listBill });
        }

        // Get View report
        [Route("reportchart", Name = "Report")]
        public IActionResult ReportView()
        {
            // Check User Logged in
            userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            User signed = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).FirstOrDefault(x => x.Id == userId);
            ViewData["UserAvatar"] = signed.Avatar;
            ViewData["UserRole"] = signed.Role;
            ViewData["UserName"] = signed.Hoten;
            ViewData["UserId"] = signed.Id;
            return View();
        }

        // GET: Get Data for chart
        // @params
        // @output List data Bill
        public IActionResult getTotalSum()
        {
            var sells = _context.Bills.GroupBy(a => a.DateBook).Select(a => new { total = a.Sum(b => b.Total), Name = a.Key }).OrderByDescending(a => a.Name).ToList();
            foreach (var bi in sells)
            {
                Console.WriteLine(bi.Name + " " + bi.total);
            }
            return Json(new { status = "Success", data = sells });
        }
    }
}
