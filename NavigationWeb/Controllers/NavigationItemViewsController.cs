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
    public class NavigationItemViewsController : Controller
    {
        private readonly NavigationBBT2DbContext _context;

        public NavigationItemViewsController(NavigationBBT2DbContext context)
        {
            _context = context;
        }

        // GET: NavigationItemViews
        public async Task<IActionResult> Index()
        {
            return View(await _context.NavigationItemView.ToListAsync());
        }

        // GET: NavigationItemViews/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navigationItemView = await _context.NavigationItemView
                .FirstOrDefaultAsync(m => m.NavigationItemTenantId == id);
            if (navigationItemView == null)
            {
                return NotFound();
            }

            return View(navigationItemView);
        }

        // GET: NavigationItemViews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NavigationItemViews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NavigationItemTenantId,UserId,NavigationItemId,DateViewed")] NavigationItemView navigationItemView)
        {
            if (ModelState.IsValid)
            {
                _context.Add(navigationItemView);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(navigationItemView);
        }

        // GET: NavigationItemViews/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navigationItemView = await _context.NavigationItemView.FindAsync(id);
            if (navigationItemView == null)
            {
                return NotFound();
            }
            return View(navigationItemView);
        }

        // POST: NavigationItemViews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NavigationItemTenantId,UserId,NavigationItemId,DateViewed")] NavigationItemView navigationItemView)
        {
            if (id != navigationItemView.NavigationItemTenantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(navigationItemView);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NavigationItemViewExists(navigationItemView.NavigationItemTenantId))
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
            return View(navigationItemView);
        }

        // GET: NavigationItemViews/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navigationItemView = await _context.NavigationItemView
                .FirstOrDefaultAsync(m => m.NavigationItemTenantId == id);
            if (navigationItemView == null)
            {
                return NotFound();
            }

            return View(navigationItemView);
        }

        // POST: NavigationItemViews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var navigationItemView = await _context.NavigationItemView.FindAsync(id);
            _context.NavigationItemView.Remove(navigationItemView);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NavigationItemViewExists(string id)
        {
            return _context.NavigationItemView.Any(e => e.NavigationItemTenantId == id);
        }
    }
}
