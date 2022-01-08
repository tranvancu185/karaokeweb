using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Karaoke_project.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using PagedList.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Karaoke_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminUsersController : Controller
    {
        private readonly web_karaokeContext _context;
        public INotyfService _notyfService { get; }

        public string userId;

        private readonly IWebHostEnvironment _hostEnvironment;

        public AdminUsersController(web_karaokeContext context, INotyfService notyfService, IWebHostEnvironment hostEnviroment)
        {
            _context = context;
            _notyfService = notyfService;
            this._hostEnvironment = hostEnviroment;
        }

        // GET: Admin/AdminUsers
        [Route("listUser", Name = "ListUser")]
        public IActionResult Index(int page=1, int RoleID =0)
        {
            // Check User Logged in 
            userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            // Get User Logged in
            User signed = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).FirstOrDefault(x => x.Id == userId);
            ViewData["UserAvatar"] = signed.Avatar;
            ViewData["UserRole"] = signed.Role;
            ViewData["UserName"] = signed.Hoten;
            ViewData["UserId"] = signed.Id;
            var pageNumber = page;
            var pageSize = 20;
            // Get list User
            List<User> web_karaokeContext = new List<User>();
            if(RoleID != 0)
            {
                web_karaokeContext = _context.Users.AsNoTracking().Where(x => x.Role == RoleID).Include(f => f.RoleNavigation).OrderByDescending(x => x.Id).ToList();
            }
            else
            {
                web_karaokeContext = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).OrderByDescending(x => x.Id).ToList();
            }
            // Pagination
            PagedList<User> pagedList = new PagedList<User>(web_karaokeContext.AsQueryable(), pageNumber, pageSize);
            ViewData["Role"] = new SelectList(_context.Roles, "Id", "Name");
            ViewBag.CurrentPage = pageNumber;
            ViewBag.CurrentRoleID = RoleID;
            return View(pagedList);
        }

        // Filter by Role 
        // @params Role ID
        // @output Json object User
        public IActionResult Fillter(int RoleID = 0 )
        {
            var url = $"/Admin/AdminUsers?RoleID={RoleID}";
            if(RoleID == 0)
            {
                url = $"/Admin/AdminUsers";
            }
            return Json(new { status = "Success", redirectUrl = url });
        }

        // GET: Admin/AdminUsers/Details/5
        [Route("DetailUser", Name = "DetailUser")]
        public async Task<IActionResult> Details(string id)
        {
            // Check User Logged in
            userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            // Get User Logged in
            User signed = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).FirstOrDefault(x => x.Id == userId);
            ViewData["UserAvatar"] = signed.Avatar;
            ViewData["UserRole"] = signed.Role;
            ViewData["UserName"] = signed.Hoten;
            ViewData["UserId"] = signed.Id;
            // Validate params
            if (id == null)
            {
                return NotFound();
            }
            // Get user by id
            var user = await _context.Users
                .Include(f => f.RoleNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: Admin/AdminUsers/Create
        public IActionResult Create()
        {
            userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }

            // Get User signed 
            User signed = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).FirstOrDefault(x => x.Id == userId);
            ViewData["UserAvatar"] = signed.Avatar;
            ViewData["UserRole"] = signed.Role;
            ViewData["UserName"] = signed.Hoten;
            ViewData["UserId"] = signed.Id;
            ViewData["Role"] = new SelectList(_context.Roles, "Id", "Name");
            return View();
        }

        // POST: Admin/AdminUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Hoten,Role,Username,Password,ImageFile")] User user)
        {
            if (ModelState.IsValid)
            {
                // Check User existed
                User userExist = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == user.Id || x.Username == user.Username);
                if(userExist != null)
                {
                    if ((userExist.Id).ToString().Replace(" ", string.Empty) == user.Id.ToString())
                    {
                        _notyfService.Error("Id nhân viên đã tồn tại!");
                        return RedirectToAction(nameof(Create));
                    }
                    if ((userExist.Username).ToString().Replace(" ", string.Empty) == user.Username.ToString())
                    {
                        _notyfService.Error("Username nhân viên đã tồn tại!");
                        return RedirectToAction(nameof(Create));
                    }
                }
                else
                {
                    // Set image avatar User
                    if (user.ImageFile != null)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(user.ImageFile.FileName);
                        string extension = Path.GetExtension(user.ImageFile.FileName);
                        user.Avatar = fileName + DateTime.Now.ToString("yyymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/image/avatar/" + user.Avatar);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await user.ImageFile.CopyToAsync(fileStream);
                        }
                    }
                    else
                    {
                        user.Avatar = "thumb-1.jpg";
                    }
                    // Add User
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    _context.ChangeTracker.Clear();
                    _notyfService.Success("Tạo mới thành công!");
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["Role"] = new SelectList(_context.Roles, "Id", "Name", user.Role);
            // Get User signed 
            userId = HttpContext.Session.GetString("UserId");
            User signed = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).FirstOrDefault(x => x.Id == userId);
            ViewData["UserAvatar"] = signed.Avatar;
            ViewData["UserRole"] = signed.Role;
            ViewData["UserName"] = signed.Hoten;
            ViewData["UserId"] = signed.Id;
            return View(user);
        }

        // GET: Admin/AdminUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            // Get User logged In
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
            // Validate id User
            if (id == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            // Create select list Role 
            ViewData["Role"] = new SelectList(_context.Roles, "Id", "Name", user.Role);
            return View(user);
        }

        // POST: Admin/AdminUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, User user)
        {
            // Check User logged in
            userId = HttpContext.Session.GetString("UserId");
            User signed = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).FirstOrDefault(x => x.Id == userId);
            ViewData["UserAvatar"] = signed.Avatar;
            ViewData["UserRole"] = signed.Role;
            ViewData["UserName"] = signed.Hoten;
            ViewData["UserId"] = signed.Id;
            //Set data default User
            User imageModel = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            user.Username = imageModel.Username;
            user.Password = imageModel.Password;
            //Check role user logged in and update data User
            if (signed.Role != 1)
            {
                user.Hoten = imageModel.Hoten;
                user.Password = imageModel.Password;
                user.Role = imageModel.Role;
                user.RoleNavigation = imageModel.RoleNavigation;
            }
            if (user.ImageFile == null )
            {
                user.Avatar = imageModel.Avatar;
            }
            else
            {
                //delete image
                var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image/avatar", imageModel.Avatar);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(user.ImageFile.FileName);
                string extension = Path.GetExtension(user.ImageFile.FileName);
                user.Avatar = fileName + DateTime.Now.ToString("yyymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/image/avatar/" + user.Avatar);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await user.ImageFile.CopyToAsync(fileStream);
                }
            }
            _context.Update(user);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            _notyfService.Success("Cập nhật thành công!");
            ViewData["Role"] = new SelectList(_context.Roles, "Id", "Name", user.Role);
            return View(user);
        }

        // GET: Admin/AdminUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
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
            // Validate param id
            if (id == null)
            {
                return NotFound();
            }
            var user = await _context.Users
                .Include(f => f.RoleNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Admin/AdminUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var imageModel = await _context.Users.FindAsync(id);
            //delete image
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image/avatar", imageModel.Avatar);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            _notyfService.Success("Xóa thành công!");
            return RedirectToAction(nameof(Index));
        }
    } 
}
