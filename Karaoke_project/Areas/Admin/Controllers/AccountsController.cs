using AspNetCoreHero.ToastNotification.Abstractions;
using Karaoke_project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Karaoke_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountsController : Controller
    {
        private readonly web_karaokeContext _context;
        public INotyfService _notyfService { get; }

        private readonly IWebHostEnvironment _hostEnvironment;

        public AccountsController(web_karaokeContext context, INotyfService notyfService, IWebHostEnvironment hostEnviroment)
        {
            _context = context;
            _notyfService = notyfService;
            this._hostEnvironment = hostEnviroment;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            
            return RedirectToAction("Login");
        }
    }
}
