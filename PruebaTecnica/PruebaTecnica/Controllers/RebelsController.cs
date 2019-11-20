using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Controllers
{
    [Route("Rebels")]
    [ApiController]
    public class RebelsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public RebelsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Rebels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rebel>>> GetRebels()
        {
            return await _context.Rebels.ToListAsync();
        }

        // GET: api/Rebels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rebel>> GetRebel(long id)
        {
            var rebel = await _context.Rebels.FindAsync(id);

            if (rebel == null)
            {
                return NotFound();
            }

            return rebel;
        }

        // PUT: api/Rebels/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRebel(long id, Rebel rebel)
        {
            if (id != rebel.Id)
            {
                return BadRequest();
            }

            _context.Entry(rebel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RebelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Rebels
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Rebel>> PostRebel(Rebel rebel)
        {
            rebel.Date = DateTime.UtcNow;

            try
            {
                try
                {
                    _context.Rebels.Add(rebel);
                    ValidateName(rebel.Name);
                    rebel.Register = true;
                    await _context.SaveChangesAsync();
                }
                catch (NullValueException ex)
                {
                    ErrorLogging(ex);
                    return BadRequest(ex.Message);
                }


                string file = "RebelsList.txt";
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB");

                if (System.IO.File.Exists(file))
                {
                    string fileContent = System.IO.File.ReadAllText("RebelsList.txt");
                    string newLine = String.Format("rebel {0} on planet {1} at {2}", rebel.Name, rebel.Planet, rebel.Date.ToString("F", culture));
                    string[] lines = { fileContent, newLine };
                    System.IO.File.WriteAllLines("RebelsList.txt", lines);
                }
                else
                {
                    string newLine = String.Format("rebel {0} on planet {1} at {2}", rebel.Name, rebel.Planet, rebel.Date.ToString("F", culture));
                    System.IO.File.WriteAllText("RebelsList.txt", newLine);
                }
            }
            catch (Exception ex)
            {
                ErrorLogging(ex);
                return BadRequest(ex);
            }
            return CreatedAtAction(nameof(GetRebel), new { id = rebel.Id }, rebel.Register);
        }

        private static void ValidateName(string name)
        {
            bool isNull = string.IsNullOrEmpty(name);
            if (isNull == true)
            {
                throw new NullValueException(name);
            }
        }

        private static void ErrorLogging(Exception ex)
        {
            string path = "Log.txt";
            if (!System.IO.File.Exists(path))
            {
                System.IO.File.Create(path).Dispose();
            }
            using(StreamWriter streamWriter = System.IO.File.AppendText(path))
            {
                streamWriter.Write("Error Message: " + ex.Message);
                streamWriter.Write("\nStack Trace: " + ex.StackTrace);
                streamWriter.Write(DateTime.Now + "\n");
            }

        }

        // DELETE: api/Rebels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Rebel>> DeleteRebel(long id)
        {
            var rebel = await _context.Rebels.FindAsync(id);
            if (rebel == null)
            {
                return NotFound();
            }

            _context.Rebels.Remove(rebel);
            await _context.SaveChangesAsync();

            return rebel;
        }

        private bool RebelExists(long id)
        {
            return _context.Rebels.Any(e => e.Id == id);
        }
    }
}
