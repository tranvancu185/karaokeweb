using AspNetCoreHero.ToastNotification.Abstractions;
using Karaoke_project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Karaoke_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly web_karaokeContext _context;
        public INotyfService _notyfService { get; }

        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(ILogger<HomeController> logger, web_karaokeContext context, INotyfService notyfService, IWebHostEnvironment hostEnviroment)
        {
            _logger = logger;
            _context = context;
            _notyfService = notyfService;
            this._hostEnvironment = hostEnviroment;
        }

        public IActionResult Admin()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }
        
        public IActionResult Index()
        {
            List<Room> rooms = _context.Rooms.OrderBy(a => a.Id).ToList();
            List<Food> foods = _context.Foods.OrderBy(a => a.Id).ToList();
            ViewData["foods"] = foods;
            ViewData["rooms"] = rooms;
            return View();
        }
        
        [Route("abou-us.html", Name = "AboutUs")]
        public IActionResult AboutUs()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "DangNhap")]
        public IActionResult Login()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return View();
        }

        //Logout
        [HttpGet]
        [AllowAnonymous]
        [Route("dang-xuat.html", Name = "DangXuat")]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "DangNhap")]
        public async Task<IActionResult> Login(Login info)
        {
            if (ModelState.IsValid)
            {
                User data = _context.Users.Where(s => s.Username.Equals(info.Username) && s.Password.Equals(info.password)).FirstOrDefault();
                if (data != null)
                {
                    //add session
                    HttpContext.Session.SetString("UserId", data.Id);
                    var roleUser = HttpContext.Session.GetString("UserId");
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, data.Hoten),
                        new Claim("UserId", data.Id)
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    _notyfService.Success("Đăng nhập thành công!");
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else
                {
                    _notyfService.Error("Username hoặc password không đúng!");
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("dat-phong-client.html", Name = "BookPhongClient")]
        public IActionResult ClientBooks()
        {
            
            ViewData["Cat"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["Room"] = new SelectList(_context.Rooms, "Id", "Name");
            return View();
        }

    }
}
