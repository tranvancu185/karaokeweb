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
    public class RoomsController : Controller
    {
        private readonly web_karaokeContext _context;
        public INotyfService _notyfService { get; }

        public string userId;

        public RoomsController(web_karaokeContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/Rooms
        [Route("ListRoom", Name = "ListRoom")]
        public async Task<IActionResult> Index()
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
            return View(await _context.Rooms.ToListAsync());
        }

        // GET: Admin/Rooms/Details/5
        [Route("DetailRoom", Name = "DetailRoom")]
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
            var room = await _context.Rooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }
            User signed = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).FirstOrDefault(x => x.Id == userId);
            ViewData["UserAvatar"] = signed.Avatar;
            ViewData["UserRole"] = signed.Role;
            ViewData["UserName"] = signed.Hoten;
            ViewData["UserId"] = signed.Id;
            return View(room);
        }

        // GET: Admin/Rooms/Create
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

        // POST: Admin/Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Status,Price,TypeRoom")] Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Add(room);
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
            return View(room);
        }

        // GET: Admin/Rooms/Edit/5
        [Route("EditRoom", Name = "EditRoom")]
        public async Task<IActionResult> Edit(int? id)
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
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            User signed = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).FirstOrDefault(x => x.Id == userId);
            ViewData["UserAvatar"] = signed.Avatar;
            ViewData["UserRole"] = signed.Role;
            ViewData["UserName"] = signed.Hoten;
            ViewData["UserId"] = signed.Id;
            return View(room);
        }

        // POST: Admin/Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("EditRoom", Name = "EditRoom")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Status,Price,TypeRoom")] Room room)
        {
            if (id != room.Id)
            {
                _notyfService.Error("Cập nhật thất bại!");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    _context.Update(room);
                    await _context.SaveChangesAsync();
                    _notyfService.Success("Cập nhật mới thành công!");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.Id))
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
            userId = HttpContext.Session.GetString("UserId");
            User signed = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).FirstOrDefault(x => x.Id == userId);
            ViewData["UserAvatar"] = signed.Avatar;
            ViewData["UserRole"] = signed.Role;
            ViewData["UserName"] = signed.Hoten;
            ViewData["UserId"] = signed.Id;
            return View(room);
        }

        // GET: Admin/Rooms/Delete/5
        [Route("DelRoom", Name = "DelRoom")]
        public async Task<IActionResult> Delete(int? id)
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
            var room = await _context.Rooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }
            User signed = _context.Users.AsNoTracking().Include(f => f.RoleNavigation).FirstOrDefault(x => x.Id == userId);
            ViewData["UserAvatar"] = signed.Avatar;
            ViewData["UserRole"] = signed.Role;
            ViewData["UserName"] = signed.Hoten;
            ViewData["UserId"] = signed.Id;
            return View(room);
        }

        // POST: Admin/Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("DelRoom", Name = "DelRoom")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            _notyfService.Success("Xóa thành công!");
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }
    }
}
