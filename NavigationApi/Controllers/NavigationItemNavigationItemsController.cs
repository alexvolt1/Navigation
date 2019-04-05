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
    public class NavigationItemNavigationItemsController : ControllerBase
    {
        private readonly NavigationBBT2DbContext _context;

        public NavigationItemNavigationItemsController(NavigationBBT2DbContext context)
        {
            _context = context;
        }

        // GET: api/NavigationItemNavigationItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NavigationItemNavigationItem>>> GetNavigationItemNavigationItem()
        {
            return await _context.NavigationItemNavigationItem.ToListAsync();
        }

        // GET: api/NavigationItemNavigationItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NavigationItemNavigationItem>> GetNavigationItemNavigationItem(string id)
        {
            var navigationItemNavigationItem = await _context.NavigationItemNavigationItem.FindAsync(id);

            if (navigationItemNavigationItem == null)
            {
                return NotFound();
            }

            return navigationItemNavigationItem;
        }

        // PUT: api/NavigationItemNavigationItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNavigationItemNavigationItem(string id, NavigationItemNavigationItem navigationItemNavigationItem)
        {
            if (id != navigationItemNavigationItem.NavigationItemTenantId)
            {
                return BadRequest();
            }

            _context.Entry(navigationItemNavigationItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NavigationItemNavigationItemExists(id))
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

        // POST: api/NavigationItemNavigationItems
        [HttpPost]
        public async Task<ActionResult<NavigationItemNavigationItem>> PostNavigationItemNavigationItem(NavigationItemNavigationItem navigationItemNavigationItem)
        {
            _context.NavigationItemNavigationItem.Add(navigationItemNavigationItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNavigationItemNavigationItem", new { id = navigationItemNavigationItem.NavigationItemTenantId }, navigationItemNavigationItem);
        }

        // DELETE: api/NavigationItemNavigationItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<NavigationItemNavigationItem>> DeleteNavigationItemNavigationItem(string id)
        {
            var navigationItemNavigationItem = await _context.NavigationItemNavigationItem.FindAsync(id);
            if (navigationItemNavigationItem == null)
            {
                return NotFound();
            }

            _context.NavigationItemNavigationItem.Remove(navigationItemNavigationItem);
            await _context.SaveChangesAsync();

            return navigationItemNavigationItem;
        }

        private bool NavigationItemNavigationItemExists(string id)
        {
            return _context.NavigationItemNavigationItem.Any(e => e.NavigationItemTenantId == id);
        }
    }
}
