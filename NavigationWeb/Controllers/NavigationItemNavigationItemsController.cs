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
    public class NavigationItemNavigationItemsController : Controller
    {
        private readonly NavigationBBT2DbContext _context;

        public NavigationItemNavigationItemsController(NavigationBBT2DbContext context)
        {
            _context = context;
        }

        // GET: NavigationItemNavigationItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.NavigationItemNavigationItem.ToListAsync());
        }

        // GET: NavigationItemNavigationItems/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navigationItemNavigationItem = await _context.NavigationItemNavigationItem
                .FirstOrDefaultAsync(m => m.NavigationItemTenantId == id);
            if (navigationItemNavigationItem == null)
            {
                return NotFound();
            }

            return View(navigationItemNavigationItem);
        }

        // GET: NavigationItemNavigationItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NavigationItemNavigationItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NavigationItemTenantId,ParentId,NavigationItemId,Sequence,Inherited")] NavigationItemNavigationItem navigationItemNavigationItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(navigationItemNavigationItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(navigationItemNavigationItem);
        }

        // GET: NavigationItemNavigationItems/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navigationItemNavigationItem = await _context.NavigationItemNavigationItem.FindAsync(id);
            if (navigationItemNavigationItem == null)
            {
                return NotFound();
            }
            return View(navigationItemNavigationItem);
        }

        // POST: NavigationItemNavigationItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NavigationItemTenantId,ParentId,NavigationItemId,Sequence,Inherited")] NavigationItemNavigationItem navigationItemNavigationItem)
        {
            if (id != navigationItemNavigationItem.NavigationItemTenantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(navigationItemNavigationItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NavigationItemNavigationItemExists(navigationItemNavigationItem.NavigationItemTenantId))
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
            return View(navigationItemNavigationItem);
        }

        // GET: NavigationItemNavigationItems/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navigationItemNavigationItem = await _context.NavigationItemNavigationItem
                .FirstOrDefaultAsync(m => m.NavigationItemTenantId == id);
            if (navigationItemNavigationItem == null)
            {
                return NotFound();
            }

            return View(navigationItemNavigationItem);
        }

        // POST: NavigationItemNavigationItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var navigationItemNavigationItem = await _context.NavigationItemNavigationItem.FindAsync(id);
            _context.NavigationItemNavigationItem.Remove(navigationItemNavigationItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NavigationItemNavigationItemExists(string id)
        {
            return _context.NavigationItemNavigationItem.Any(e => e.NavigationItemTenantId == id);
        }
    }
}
