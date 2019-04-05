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
    public class NavigationItemsController : Controller
    {
        private readonly NavigationBBT2DbContext _context;

        public NavigationItemsController(NavigationBBT2DbContext context)
        {
            _context = context;
        }

        // GET: NavigationItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.NavigationItem.ToListAsync());
        }

        // GET: NavigationItems/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navigationItem = await _context.NavigationItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (navigationItem == null)
            {
                return NotFound();
            }

            return View(navigationItem);
        }

        // GET: NavigationItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NavigationItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantId,Id,StoredType,Owner,DefaultName,DefaultDescription,Source,ImageUrl,LargeImageUrl,IsMobile,DocumentMode")] NavigationItem navigationItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(navigationItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(navigationItem);
        }

        // GET: NavigationItems/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navigationItem = await _context.NavigationItem.FindAsync(id);
            if (navigationItem == null)
            {
                return NotFound();
            }
            return View(navigationItem);
        }

        // POST: NavigationItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TenantId,Id,StoredType,Owner,DefaultName,DefaultDescription,Source,ImageUrl,LargeImageUrl,IsMobile,DocumentMode")] NavigationItem navigationItem)
        {
            if (id != navigationItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(navigationItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NavigationItemExists(navigationItem.Id))
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
            return View(navigationItem);
        }

        // GET: NavigationItems/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navigationItem = await _context.NavigationItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (navigationItem == null)
            {
                return NotFound();
            }

            return View(navigationItem);
        }

        // POST: NavigationItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var navigationItem = await _context.NavigationItem.FindAsync(id);
            _context.NavigationItem.Remove(navigationItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NavigationItemExists(string id)
        {
            return _context.NavigationItem.Any(e => e.Id == id);
        }
    }
}
