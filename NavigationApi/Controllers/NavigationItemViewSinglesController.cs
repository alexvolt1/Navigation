using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedNav.Data;
using SharedNav.Models;

namespace NavigationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavigationItemViewSinglesController : ControllerBase
    {
        private readonly NavigationBBT2DbContext _context;

        public NavigationItemViewSinglesController(NavigationBBT2DbContext context)
        {
            _context = context;
        }

        // GET: api/NavigationItemViewSingles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NavigationItemViewSingle>>> GetNavigationItemViewSingle()
        {
            return await _context.NavigationItemViewSingle.ToListAsync();
        }

        // GET: api/NavigationItemViewSingles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NavigationItemViewSingle>> GetNavigationItemViewSingle(int id)
        {
            var navigationItemViewSingle = await _context.NavigationItemViewSingle.FindAsync(id);

            if (navigationItemViewSingle == null)
            {
                return NotFound();
            }

            return navigationItemViewSingle;
        }

        // PUT: api/NavigationItemViewSingles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNavigationItemViewSingle(int id, NavigationItemViewSingle navigationItemViewSingle)
        {
            if (id != navigationItemViewSingle.Id)
            {
                return BadRequest();
            }

            _context.Entry(navigationItemViewSingle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NavigationItemViewSingleExists(id))
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

        // POST: api/NavigationItemViewSingles
        [HttpPost]
        public async Task<ActionResult<NavigationItemViewSingle>> PostNavigationItemViewSingle(NavigationItemViewSingle navigationItemViewSingle)
        {
            _context.NavigationItemViewSingle.Add(navigationItemViewSingle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNavigationItemViewSingle", new { id = navigationItemViewSingle.Id }, navigationItemViewSingle);
        }

        // DELETE: api/NavigationItemViewSingles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<NavigationItemViewSingle>> DeleteNavigationItemViewSingle(int id)
        {
            var navigationItemViewSingle = await _context.NavigationItemViewSingle.FindAsync(id);
            if (navigationItemViewSingle == null)
            {
                return NotFound();
            }

            _context.NavigationItemViewSingle.Remove(navigationItemViewSingle);
            await _context.SaveChangesAsync();

            return navigationItemViewSingle;
        }

        private bool NavigationItemViewSingleExists(int id)
        {
            return _context.NavigationItemViewSingle.Any(e => e.Id == id);
        }
    }
}
