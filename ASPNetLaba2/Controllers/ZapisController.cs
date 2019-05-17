using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNetLaba2.Models;
using Microsoft.AspNetCore.Authorization;

namespace ASPNetLaba2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZapisController : ControllerBase
    {
        private readonly RegistraturaContext _context;

        public ZapisController(RegistraturaContext context)
        {
            _context = context;
            if (_context.Zapis.Count() == 0)
            {
                _context.Zapis.Add(new Zapis
                {
                    Url = "https:\\zapis.su"
                });
                _context.SaveChanges();
            }
        }

        // GET: api/Zapis
        [HttpGet]
        public IEnumerable<Zapis> GetAll()
        {
            return _context.Zapis.Include(p => p.Pacient);
        }

        // GET: api/Zapis
        [HttpGet("{id}")]
        public async Task<IActionResult> GetZapis([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var zapis = await _context.Zapis.SingleOrDefaultAsync(m => m.ZapisId == id);
            if (zapis == null)
            {
                return NotFound();
            }
            return Ok(zapis);
        }

        // PUT: api/Zapis
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Zapis zapis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Pacient p = zapis.Pacient.FirstOrDefault();
            var item = _context.Zapis.Find(id);
            if (item == null)
            {
                return NotFound();
            }
       
            item.Url = zapis.Url;
            item.Zapis_date = zapis.Zapis_date;
            item.Zapis_time = zapis.Zapis_time;
            item.Kabinet = zapis.Kabinet;
                       
            _context.Zapis.Update(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Zapis 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Zapis zapis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Pacient p = zapis.Pacient.FirstOrDefault();

            _context.Pacient.Add(p);
            //await _context.SaveChangesAsync();
            _context.Zapis.Add(zapis);
            /*_context.Zapis.Add(new Zapis {
                PacientId = p.PacientId,
            });*/
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetZapis", new { id = zapis.ZapisId }, zapis);
        }


        // DELETE: api/Zapis
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _context.Zapis.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            _context.Zapis.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool ZapisExists(int id)
        {
            return _context.Zapis.Any(e => e.ZapisId == id);
        }
    }
}