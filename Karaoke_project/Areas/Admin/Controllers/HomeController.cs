using AspNetCoreHero.ToastNotification.Abstractions;
using Karaoke_project.Models;
using Karaoke_project.Services;
using Microsoft.AspNetCore.Hosting;
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
        public INotyfService _notyfService { get; }
        private readonly IWebHostEnvironment _hostEnvironment;
        public HomeController(web_karaokeContext context, INotyfService notyfService, IWebHostEnvironment hostEnviroment)
        {
            _context = context;
            _notyfService = notyfService;
            this._hostEnvironment = hostEnviroment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Book()
        {
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

        [HttpPost]
        public async Task<IActionResult> AddCus()
        {
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
            bill.CreateAt = DateTime.Now;
            bill.DateBook = data.dateBook;
            bill.IdCus = CusExist.Id;
            bill.IdRoom = data.idRoom;
            bill.Total = data.totalBook;
            _context.Add(bill);
            await _context.SaveChangesAsync();

            // Insert Bill Detail
            BillServices billServices = new BillServices(_context);
            Bill BillExist = billServices.getBillByCreateAt(bill.CreateAt);
            Console.WriteLine("Bill Ex" + BillExist);
            
            foreach (dynamic foo in data.foodBook)
            {
                BillDetail newBillDetail = new BillDetail();
                newBillDetail.IdBill = BillExist.Id;
                newBillDetail.IdFood = (int)foo.id;
                newBillDetail.Quantity = (int)foo.quanBook;
                Console.WriteLine("newBillDetail List : " + newBillDetail.IdFood);
                _context.Add(newBillDetail);
                await _context.SaveChangesAsync();
            }
            return Json(new { status = "Success"});
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

    }
}
