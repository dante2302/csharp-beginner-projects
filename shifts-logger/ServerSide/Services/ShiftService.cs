using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shifts_logger.Data.DbContexts;
using shifts_logger.Models;

namespace shifts_logger.Services
{
    public class ShiftService
    {
        private readonly ShiftContext _context = new();
        
        public async Task Add(Shift shift)
        {
            _context.Add(shift);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Shift>> GetAll()
        {
            return await _context.Shifts.OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<Shift> GetOne(int id)
        {
            Shift shift = await _context.Shifts.FindAsync(id);
            return shift;
        }
        public async Task<bool> Edit(int id, Shift newShift)
        {
            Shift shift = await _context.Shifts.FindAsync(id);
            if (shift is null)
                return false;

            shift.Date = newShift.Date;
            shift.Start = newShift.Start;
            shift.End = newShift.End;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            Shift shift = await _context.Shifts.FindAsync(id);
            if (shift is null)
                return false;
            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
