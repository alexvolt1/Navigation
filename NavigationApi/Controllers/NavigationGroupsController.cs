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
    public class NavigationGroupsController : ControllerBase
    {
        private readonly NavigationBBT2DbContext _context;

        public NavigationGroupsController(NavigationBBT2DbContext context)
        {
            _context = context;
        }

        // GET: api/NavigationGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NavigationGroup>>> GetNavigationGroup()
        {
            return await _context.NavigationGroup.ToListAsync();
        }

        // GET: api/NavigationGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NavigationGroup>> GetNavigationGroup(string id)
        {
            var navigationGroup = await _context.NavigationGroup.FindAsync(id);

            if (navigationGroup == null)
            {
                return NotFound();
            }

            return navigationGroup;
        }

        // PUT: api/NavigationGroups/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNavigationGroup(string id, NavigationGroup navigationGroup)
        {
            if (id != navigationGroup.Id)
            {
                return BadRequest();
            }

            _context.Entry(navigationGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NavigationGroupExists(id))
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

        // POST: api/NavigationGroups
        [HttpPost]
        public async Task<ActionResult<NavigationGroup>> PostNavigationGroup(NavigationGroup navigationGroup)
        {
            _context.NavigationGroup.Add(navigationGroup);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NavigationGroupExists(navigationGroup.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNavigationGroup", new { id = navigationGroup.Id }, navigationGroup);
        }

        // DELETE: api/NavigationGroups/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<NavigationGroup>> DeleteNavigationGroup(string id)
        {
            var navigationGroup = await _context.NavigationGroup.FindAsync(id);
            if (navigationGroup == null)
            {
                return NotFound();
            }

            _context.NavigationGroup.Remove(navigationGroup);
            await _context.SaveChangesAsync();

            return navigationGroup;
        }

        private bool NavigationGroupExists(string id)
        {
            return _context.NavigationGroup.Any(e => e.Id == id);
        }
    }
}
