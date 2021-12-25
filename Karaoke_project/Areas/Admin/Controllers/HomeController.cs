using AspNetCoreHero.ToastNotification.Abstractions;
using Karaoke_project.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            List<Food> web_karaokeContext = new List<Food>();
            if(RoleID == 0)
            {
                web_karaokeContext = _context.Foods.AsNoTracking().Include(f => f.IdCategoryNavigation).OrderByDescending(x => x.IdCategory).ToList();
            }
            else
            {
                web_karaokeContext = _context.Foods.AsNoTracking().Where(x => x.IdCategory == RoleID).Include(f => f.IdCategoryNavigation).OrderByDescending(x => x.Id).ToList();
            }

            return Json(new { status = "Success", data = web_karaokeContext });
        }

        public IActionResult GetFoodById(int id = 0)
        {
            if(id == null)
            {
                return Json(new { status = "Failed"});
            }
            var web_karaokeContext = _context.Foods.Include(f => f.IdCategoryNavigation).FirstOrDefaultAsync(m => m.Id == id);
            return Json(new { status = "Success", data = web_karaokeContext });
        }

        [HttpPost]
        public async Task<IActionResult> AddCus()
        {
            string body = "";
            using (StreamReader stream = new StreamReader(Request.Body))
            {
                body = await stream.ReadToEndAsync();
            }
            Debug.WriteLine(body);
            return Json(new { status = "Success", data = body});
        }

        public IActionResult BookFood()
        {
            ViewData["Cat"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

    }
}
