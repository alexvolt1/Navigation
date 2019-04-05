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
    public class NavigationMembershipsController : ControllerBase
    {
        private readonly NavigationBBT2DbContext _context;

        public NavigationMembershipsController(NavigationBBT2DbContext context)
        {
            _context = context;
        }

        // GET: api/NavigationMemberships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NavigationMembership>>> GetNavigationMembership()
        {
            return await _context.NavigationMembership.ToListAsync();
        }

        // GET: api/NavigationMemberships/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NavigationMembership>> GetNavigationMembership(string id)
        {
            var navigationMembership = await _context.NavigationMembership.FindAsync(id);

            if (navigationMembership == null)
            {
                return NotFound();
            }

            return navigationMembership;
        }

        // PUT: api/NavigationMemberships/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNavigationMembership(string id, NavigationMembership navigationMembership)
        {
            if (id != navigationMembership.NavigationGroupTenantId)
            {
                return BadRequest();
            }

            _context.Entry(navigationMembership).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NavigationMembershipExists(id))
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

        // POST: api/NavigationMemberships
        [HttpPost]
        public async Task<ActionResult<NavigationMembership>> PostNavigationMembership(NavigationMembership navigationMembership)
        {
            _context.NavigationMembership.Add(navigationMembership);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNavigationMembership", new { id = navigationMembership.NavigationGroupTenantId }, navigationMembership);
        }

        // DELETE: api/NavigationMemberships/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<NavigationMembership>> DeleteNavigationMembership(string id)
        {
            var navigationMembership = await _context.NavigationMembership.FindAsync(id);
            if (navigationMembership == null)
            {
                return NotFound();
            }

            _context.NavigationMembership.Remove(navigationMembership);
            await _context.SaveChangesAsync();

            return navigationMembership;
        }

        private bool NavigationMembershipExists(string id)
        {
            return _context.NavigationMembership.Any(e => e.NavigationGroupTenantId == id);
        }
    }
}
