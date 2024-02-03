using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using shifts_logger.Data.DbContexts;
using shifts_logger.Models;
using shifts_logger.Services;

namespace shifts_logger.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShiftsController : Controller
    {
        private readonly ShiftService service = new(); 

        [HttpGet]
        public async Task<ActionResult<List<Shift>>> Index()
        {
            return Json(await service.GetAll());        
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Details(int? id)
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<Shift>> Create([Bind("Id,Start,End")] Shift shift)
        {
            service.Add(shift);
            return CreatedAtAction(nameof(Create), new { id = shift.Id}, shift);
        }

        // GET: Shifts/Edit/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, [Bind("Id,Start,End")] Shift shift)
        {
            if(!(await service.Edit(id, shift)))
                return NotFound();
            return Json(shift);        
        }

        // POST: Shifts/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!(await service.Delete(id)))
                return NotFound();
            return NoContent();
        }
    }
}
