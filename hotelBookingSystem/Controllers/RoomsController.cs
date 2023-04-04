using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hotelBookingSystem.Data;
using hotelBookingSystem.Models;
using Microsoft.AspNetCore.Http;

namespace hotelBookingSystem.Controllers
{
    public class RoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.rooms.Include(r => r.branch).Include(r => r.roomType);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.rooms == null)
            {
                return NotFound();
            }

            var room = await _context.rooms
                .Include(r => r.branch)
                .Include(r => r.roomType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        public IActionResult Create()
        {
            ViewData["BranchId"] = new SelectList(_context.branches, "Id", "Name");
            ViewData["RoomTypeId"] = new SelectList(_context.roomTypes, "Id", "Name");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Room room , IFormFile file)
        {
            if (file != null)
            {
                string imagename = Guid.NewGuid().ToString() + ".jpg";
                string filePathImg = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\RoomImg", imagename);
                using (var stream = System.IO.File.Create(filePathImg))
                {
                    await file.CopyToAsync(stream);
                }
                room.Image = imagename;
            }

            if (!(room.Number==null && room.Status==null && room.Price==null && room.BranchId == null && room.RoomTypeId==null))
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["BranchId"] = new SelectList(_context.branches, "Id", "Name", room.BranchId);
            ViewData["RoomTypeId"] = new SelectList(_context.roomTypes, "Id", "Name", room.RoomTypeId);
            return View(room);
        }

      
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.rooms == null)
            {
                return NotFound();
            }

            var room = await _context.rooms
                .Include(r => r.branch)
                .Include(r => r.roomType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.rooms == null)
            {
                return Problem("Entity set 'ApplicationDbContext.rooms'  is null.");
            }
            var room = await _context.rooms.FindAsync(id);
            if (room != null)
            {
                _context.rooms.Remove(room);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
          return _context.rooms.Any(e => e.Id == id);
        }
    }
}
