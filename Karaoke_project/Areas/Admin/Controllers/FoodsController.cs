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
using Karaoke_project.Services;
using Microsoft.AspNetCore.Http;

namespace Karaoke_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FoodsController : Controller
    {
        private readonly web_karaokeContext _context;

        public string userId;
        public INotyfService _notyfService { get; }
        private readonly IWebHostEnvironment _hostEnvironment;


        public FoodsController(web_karaokeContext context, INotyfService notyfService, IWebHostEnvironment hostEnviroment)
        {
            _context = context;
            _notyfService = notyfService;
            this._hostEnvironment = hostEnviroment;
        }

        // GET: Admin/Foods
        [Route("ListFood", Name = "ListFood")]
        public IActionResult Index(int page=1 , int RoleID = 0)
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
            // return view list food page navigation
            var pageNumber = page;
            var pageSize = 20;
            List<Food> web_karaokeContext = new List<Food>();
            FoodsService FoodDAO = new FoodsService(_context);
            if (RoleID != 0)
            {
                web_karaokeContext = FoodDAO.getListFoodByCat(RoleID);
            }
            else
            {
                web_karaokeContext = FoodDAO.getListFood();
            }
            PagedList<Food> pagedList = new PagedList<Food>(web_karaokeContext.AsQueryable(), pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.CurrentRoleID = RoleID;
            ViewData["IdCategory"] = new SelectList(_context.Categories, "Id", "Name");
            return View(pagedList);
        }

        // GET: function fillter Food by categories ID
        // @params id categories
        // @output Json url page number
        public IActionResult Fillter(int RoleID = 0)
        {
            var url = $"/list-mon-an.html?RoleID={RoleID}";
            if (RoleID == 0)
            {
                url = $"/list-mon-an.html";
            }
            return Json(new { status = "Success", redirectUrl = url });
        }

        // GET: Admin/Foods/Details/5
        [Route("DetailFood", Name = "DetailFood")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            var food = await _context.Foods
                .Include(f => f.IdCategoryNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }
            User signed = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).FirstOrDefault(x => x.Id == userId);
            ViewData["UserAvatar"] = signed.Avatar;
            ViewData["UserRole"] = signed.Role;
            ViewData["UserName"] = signed.Hoten;
            ViewData["UserId"] = signed.Id;
            return View(food);
        }

        // GET: Admin/Foods/Create
        public IActionResult Create()
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
            ViewData["IdCategory"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Admin/Foods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Quantity,IdCategory,ImageFile")] Food food)
        {
            // Check User logged in
            userId = HttpContext.Session.GetString("UserId");
            User signed = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).FirstOrDefault(x => x.Id == userId);
            ViewData["UserAvatar"] = signed.Avatar;
            ViewData["UserRole"] = signed.Role;
            ViewData["UserName"] = signed.Hoten;
            ViewData["UserId"] = signed.Id;
            // Validate Form and create Food
            if (ModelState.IsValid)
            {
                // Get file name avatar
                if(food.ImageFile != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(food.ImageFile.FileName);
                    string extension = Path.GetExtension(food.ImageFile.FileName);
                    food.Image = fileName + DateTime.Now.ToString("yyymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/image/imageFood/" + food.Image);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await food.ImageFile.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    food.Image = "thumb-1.jpg";
                }
                _context.Add(food);
                await _context.SaveChangesAsync();
                _notyfService.Success("Tạo mới thành công!");
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategory"] = new SelectList(_context.Categories, "Id", "Name", food.IdCategory);
            return View(food);
        }

        // GET: Admin/Foods/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            if (id == null)
            {
                return NotFound();
            }
            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            ViewData["IdCategory"] = new SelectList(_context.Categories, "Id", "Name", food.IdCategory);
            return View(food);
        }

        // POST: Admin/Foods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Quantity,IdCategory,ImageFile")] Food food)
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
            if (id != food.Id)
            {
                _notyfService.Error("Cập nhật thất bại!");
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Food imageModel = await _context.Foods.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                    if (food.ImageFile == null)
                    {
                        food.Image = imageModel.Image;
                    }
                    else
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(food.ImageFile.FileName);
                        string extension = Path.GetExtension(food.ImageFile.FileName);
                        food.Image = fileName + DateTime.Now.ToString("yyymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/image/imageFood/" + food.Image);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await food.ImageFile.CopyToAsync(fileStream);
                        }
                    }
                    _context.Update(food);
                    await _context.SaveChangesAsync();
                    _context.ChangeTracker.Clear();
                    _notyfService.Success("Cập nhật thành công!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodExists(food.Id))
                    {
                        _notyfService.Error("Cập nhật thất bại!");
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategory"] = new SelectList(_context.Categories, "Id", "Id", food.IdCategory);
            return View(food);
        }

        // GET: Admin/Foods/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
            if (id == null)
            {
                return NotFound();
            }
            var food = await _context.Foods
                .Include(f => f.IdCategoryNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }
            return View(food);
        }

        // POST: Admin/Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imageModel = await _context.Foods.FindAsync(id);
            //delete image
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image/imageFood", imageModel.Image);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            var food = await _context.Foods.FindAsync(id);
            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();
            _notyfService.Success("Xóa mới thành công!");
            return RedirectToAction(nameof(Index));
        }

        private bool FoodExists(int id)
        {
            return _context.Foods.Any(e => e.Id == id);
        }
    }
}
