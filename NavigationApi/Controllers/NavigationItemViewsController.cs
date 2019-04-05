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
    public class NavigationItemViewsController : ControllerBase
    {
        private readonly NavigationBBT2DbContext _context;

        public NavigationItemViewsController(NavigationBBT2DbContext context)
        {
            _context = context;
        }

        // GET: api/NavigationItemViews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NavigationItemView>>> GetNavigationItemView()
        {
            return await _context.NavigationItemView.ToListAsync();
        }

        // GET: api/NavigationItemViews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NavigationItemView>> GetNavigationItemView(string id)
        {
            var navigationItemView = await _context.NavigationItemView.FindAsync(id);

            if (navigationItemView == null)
            {
                return NotFound();
            }

            return navigationItemView;
        }

        // PUT: api/NavigationItemViews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNavigationItemView(string id, NavigationItemView navigationItemView)
        {
            if (id != navigationItemView.NavigationItemTenantId)
            {
                return BadRequest();
            }

            _context.Entry(navigationItemView).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NavigationItemViewExists(id))
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

        // POST: api/NavigationItemViews
        [HttpPost]
        public async Task<ActionResult<NavigationItemView>> PostNavigationItemView(NavigationItemView navigationItemView)
        {
            _context.NavigationItemView.Add(navigationItemView);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNavigationItemView", new { id = navigationItemView.NavigationItemTenantId }, navigationItemView);
        }

        // DELETE: api/NavigationItemViews/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<NavigationItemView>> DeleteNavigationItemView(string id)
        {
            var navigationItemView = await _context.NavigationItemView.FindAsync(id);
            if (navigationItemView == null)
            {
                return NotFound();
            }

            _context.NavigationItemView.Remove(navigationItemView);
            await _context.SaveChangesAsync();

            return navigationItemView;
        }

        private bool NavigationItemViewExists(string id)
        {
            return _context.NavigationItemView.Any(e => e.NavigationItemTenantId == id);
        }
    }
}
