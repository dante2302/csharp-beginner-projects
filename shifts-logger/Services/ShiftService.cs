using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shifts_logger.Data.DbContexts;
using shifts_logger.Models;

namespace shifts_logger.Services
{
    public class ShiftService
    {
        private readonly ShiftContext _context = new();
        
        public void Add(Shift shift)
        {
            _context.Add(shift);
            _context.SaveChanges();
        }

        public async Task<List<Shift>> GetAll()
        {
            return await _context.Shifts.OrderBy(x => x.Id).ToListAsync();
        }
    }
}
