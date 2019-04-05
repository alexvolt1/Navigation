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
    public class NavigationItemsController : ControllerBase
    {
        private readonly NavigationBBT2DbContext _context;

        public NavigationItemsController(NavigationBBT2DbContext context)
        {
            _context = context;
        }

        // GET: api/NavigationItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NavigationItem>>> GetNavigationItem()
        {
            return await _context.NavigationItem.ToListAsync();
        }

        // GET: api/NavigationItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NavigationItem>> GetNavigationItem(string id)
        {
            var navigationItem = await _context.NavigationItem.FindAsync(id);

            if (navigationItem == null)
            {
                return NotFound();
            }

            return navigationItem;
        }

        // PUT: api/NavigationItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNavigationItem(string id, NavigationItem navigationItem)
        {
            if (id != navigationItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(navigationItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NavigationItemExists(id))
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

        // POST: api/NavigationItems
        [HttpPost]
        public async Task<ActionResult<NavigationItem>> PostNavigationItem(NavigationItem navigationItem)
        {
            _context.NavigationItem.Add(navigationItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NavigationItemExists(navigationItem.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNavigationItem", new { id = navigationItem.Id }, navigationItem);
        }

        // DELETE: api/NavigationItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<NavigationItem>> DeleteNavigationItem(string id)
        {
            var navigationItem = await _context.NavigationItem.FindAsync(id);
            if (navigationItem == null)
            {
                return NotFound();
            }

            _context.NavigationItem.Remove(navigationItem);
            await _context.SaveChangesAsync();

            return navigationItem;
        }

        private bool NavigationItemExists(string id)
        {
            return _context.NavigationItem.Any(e => e.Id == id);
        }
    }
}
