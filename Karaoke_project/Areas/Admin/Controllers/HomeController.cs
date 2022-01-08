using AspNetCoreHero.ToastNotification.Abstractions;
using Karaoke_project.Models;
using Karaoke_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Karaoke_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly web_karaokeContext _context;
        public string userId;
        public INotyfService _notyfService { get; }
        private readonly IWebHostEnvironment _hostEnvironment;
        public HomeController(web_karaokeContext context, INotyfService notyfService, IWebHostEnvironment hostEnviroment)
        {
            _context = context;
            _notyfService = notyfService;
            this._hostEnvironment = hostEnviroment;
        }
        [AllowAnonymous]
        [Route("Dasboard", Name = "Dasboard")]
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString("UserId");
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
        [Route("Book", Name = "BookPhong")]
        public IActionResult Book()
        {
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
            ViewData["Cat"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["Room"] = new SelectList(_context.Rooms, "Id", "Name");
            return View();
        }


        public IActionResult GetFood(int RoleID = 0)
        {
            List<Food> foodList = new List<Food>();
            FoodsService dao = new FoodsService(_context);
            if(RoleID == 0)
            {
                foodList = dao.getListFood();
            }
            else
            {
                foodList = dao.getListFoodByCat(RoleID);
            }
            return Json(new { status = "Success", data = foodList });
        }

        public IActionResult GetFoodById(int id = 0)
        {
            if(id == null)
            {
                return Json(new { status = "Failed"});
            }
            FoodsService dao = new FoodsService(_context);
            Food food = dao.getListFoodById(id);
            return Json(new { status = "Success", data = food });
        }

        public IActionResult GetRoomById(int id = 0)
        {
            if (id == null)
            {
                return Json(new { status = "Failed" });
            }
            RoomsService dao = new RoomsService(_context);
            var room = dao.getRoomById(id);
            return Json(new { status = "Success", data = room });
        }

        public IActionResult getRoomByTime(DateTime? dateBook, TimeSpan? checkIn, TimeSpan? checkOut)
        {
            RoomsService RoomDAO = new RoomsService(_context);
            List<Room> roms = RoomDAO.checkRoomTime(dateBook, checkIn, checkOut);
            return Json(new { status = "Success", data = roms });
        }

        [HttpPost]
        public async Task<IActionResult> AddCus()
        {
            DateTime now = DateTime.Now;
            string body = "";
            using (StreamReader stream = new StreamReader(Request.Body))
            {
                body = await stream.ReadToEndAsync();
            }
            dynamic json = JsonConvert.DeserializeObject(body);
            dynamic data = json.cus;
            // Insert Customer
            Customer cus = new Customer();
            cus.Hoten = data.nameCus;
            cus.Phone = data.phoneCus;
            await this.checkCus(cus);
            CustomersService dao = new CustomersService(_context);
            // Insert Bill
            Customer CusExist = dao.getCustomerByPhone((string)cus.Phone);
            Bill bill = new Bill();
            bill.CheckIn = data.checkIn;
            bill.CheckOut = data.checkOut;
            bill.CreateAt = now;
            bill.DateBook = data.dateBook;
            bill.IdCus = CusExist.Id;
            bill.IdRoom = data.idRoom;
            bill.Total = data.totalBook;
            BillServices billServices = new BillServices(_context);
            bool checkBill = billServices.checkBookExist(bill);
            if(checkBill == false)
            {
                _context.Add(bill);
                _context.SaveChanges();
                // Insert Bill Detail
                Bill BillExist = billServices.getBillByCreateAt(bill.CreateAt);
                if (BillExist.Id == 0)
                {
                    return Json(new { status = "Failed" });
                }
                else
                {
                    BillDetailSevices billDetailDAO = new BillDetailSevices(_context);
                    foreach (dynamic foo in data.foodBook)
                    {
                        BillDetail newBillDetail = new BillDetail();
                        newBillDetail.IdBill = BillExist.Id;
                        newBillDetail.IdFood = (int)foo.id;
                        newBillDetail.Quantity = (int)foo.quanBook;
                        _context.Add(newBillDetail);
                        _context.SaveChanges();
                    }
                }
                return Json(new { status = "Success" });
            }
            else
            {
                return Json(new { status = "Failed", message = "Phòng đã được book vào khung giờ này, vui lòng chọn khung giờ khác!" });
            }
        }
        public async Task checkCus(Customer cus)
        {
            CustomersService dao = new CustomersService(_context);
            Customer CusExist = dao.getCustomerByPhone(cus.Phone);
            if (CusExist != null)
            {
            }
            else
            {
                _context.Add(cus);
                await _context.SaveChangesAsync();
            }
        }

        public IActionResult BookFood()
        {
            ViewData["Cat"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
