using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Karaoke_project.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Karaoke_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly web_karaokeContext _context;

        public string userId;
        public INotyfService _notyfService { get; }

        public CategoriesController(web_karaokeContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/Categories
        [Route("listCategories", Name = "listCategories")]
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
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Admin/Categories/Details/5
        [Route("DetailCategories", Name = "DetailCategories")]
        public async Task<IActionResult> Details(int? id)
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
            // get detail categories
            if (id == null)
            {
                return NotFound();
            }
            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // GET: Admin/Categories/Create
        public IActionResult Create()
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
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                _notyfService.Success("Tạo mới thành công!");
                return RedirectToAction(nameof(Index));
            }
            userId = HttpContext.Session.GetString("UserId");
            User signed = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).FirstOrDefault(x => x.Id == userId);
            ViewData["UserAvatar"] = signed.Avatar;
            ViewData["UserRole"] = signed.Role;
            ViewData["UserName"] = signed.Hoten;
            ViewData["UserId"] = signed.Id;
            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Check Userr logged in
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
            userId = HttpContext.Session.GetString("UserId");
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
        {
            // Check User logged in
            if (userId == null)
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            userId = HttpContext.Session.GetString("UserId");
            User signed = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).FirstOrDefault(x => x.Id == userId);
            ViewData["UserAvatar"] = signed.Avatar;
            ViewData["UserRole"] = signed.Role;
            ViewData["UserName"] = signed.Hoten;
            ViewData["UserId"] = signed.Id;
            // Edit categories
            if (id != category.Id)
            {
                _notyfService.Error("Cập nhật thất bại!");
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                    _notyfService.Success("Cập nhật thành công!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // Check user logged id
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
            // return view delete categories
            if (id == null)
            {
                return NotFound();
            }
            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            List<Food> exists = _context.Foods.Where(x => x.IdCategory == id).ToList();
            if (exists.Count > 0)
            {
                _notyfService.Error("Không thể xóa do tồn tại thực đơn!");
                return RedirectToAction(nameof(Index));
            }
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            _notyfService.Success("Xóa thành công!");
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
