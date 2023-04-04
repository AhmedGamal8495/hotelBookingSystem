using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hotelBookingSystem.Data;
using hotelBookingSystem.Models;
using Microsoft.AspNetCore.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace hotelBookingSystem.Controllers
{
    public class CartsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public CartsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> Cart()
        {
            var user = await _userManager.GetUserAsync(User);
            var result = _context.carts.Include(r => r.room).Include(r => r.ApplicationUser).Where(u => u.UserId == user.Id).ToList();

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(Cart model, DateTime FromDate , DateTime ToDate )
        {
            var room = _context.rooms.FirstOrDefault(r => r.Id == model.Id);
            var user = await _userManager.GetUserAsync(User);

            var cart = new Cart();

            cart.UserId = user.Id;
            cart.RoomId = room.Id;
            cart.FromDate = FromDate;
            cart.ToDate = ToDate;
            cart.TotalPrice = room.Price;

            var shopcart = _context.carts.FirstOrDefault(u => u.UserId == user.Id);

            if (DateTime.Now>FromDate && DateTime.Now<=ToDate)
            {
                room.Status = false;
            }

            if (shopcart != null)
            {
                cart.TotalPrice = room.Price * 95/100 ;
            }
            _context.carts.Add(cart);
            _context.SaveChanges();

            return RedirectToAction("Cart", "Carts");
        }



        [HttpPost , ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            
            if (_context.carts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.carts'  is null.");
            }
            var cart = await _context.carts.FindAsync(id);
            if (DateTime.Now > cart.FromDate && DateTime.Now <= cart.ToDate)
            {
                _context.carts.Remove(cart);
            }
            if( id !=null)
            {
                var room = _context.rooms.FirstOrDefault(r => r.Id == cart.RoomId);
                room.Status = true;
                _context.carts.Remove(cart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Cart));
        }


       
    }
}
