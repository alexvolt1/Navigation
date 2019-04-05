using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SharedNav.Data;
using SharedNav.Models;

namespace NavigationWeb.Controllers
{
    public class NavigationMembershipsController : Controller
    {
        private readonly NavigationBBT2DbContext _context;

        public NavigationMembershipsController(NavigationBBT2DbContext context)
        {
            _context = context;
        }

        // GET: NavigationMemberships
        public async Task<IActionResult> Index()
        {
            return View(await _context.NavigationMembership.ToListAsync());
        }

        // GET: NavigationMemberships/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navigationMembership = await _context.NavigationMembership
                .FirstOrDefaultAsync(m => m.NavigationGroupTenantId == id);
            if (navigationMembership == null)
            {
                return NotFound();
            }

            return View(navigationMembership);
        }

        // GET: NavigationMemberships/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NavigationMemberships/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NavigationGroupTenantId,Member,Type,NavigationGroupId")] NavigationMembership navigationMembership)
        {
            if (ModelState.IsValid)
            {
                _context.Add(navigationMembership);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(navigationMembership);
        }

        // GET: NavigationMemberships/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navigationMembership = await _context.NavigationMembership.FindAsync(id);
            if (navigationMembership == null)
            {
                return NotFound();
            }
            return View(navigationMembership);
        }

        // POST: NavigationMemberships/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NavigationGroupTenantId,Member,Type,NavigationGroupId")] NavigationMembership navigationMembership)
        {
            if (id != navigationMembership.NavigationGroupTenantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(navigationMembership);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NavigationMembershipExists(navigationMembership.NavigationGroupTenantId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(navigationMembership);
        }

        // GET: NavigationMemberships/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navigationMembership = await _context.NavigationMembership
                .FirstOrDefaultAsync(m => m.NavigationGroupTenantId == id);
            if (navigationMembership == null)
            {
                return NotFound();
            }

            return View(navigationMembership);
        }

        // POST: NavigationMemberships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var navigationMembership = await _context.NavigationMembership.FindAsync(id);
            _context.NavigationMembership.Remove(navigationMembership);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NavigationMembershipExists(string id)
        {
            return _context.NavigationMembership.Any(e => e.NavigationGroupTenantId == id);
        }
    }
}
