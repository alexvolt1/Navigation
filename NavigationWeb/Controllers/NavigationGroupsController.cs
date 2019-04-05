using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NavigationWeb.Helper;
using NavigationWeb.Utility;
using Newtonsoft.Json;
using SharedNav.Data;
using SharedNav.Models;

namespace NavigationWeb.Controllers
{
    public class NavigationGroupsController : Controller
    {
        private readonly HttpClient _client;

        ApiHelper _api = new ApiHelper();

        public NavigationGroupsController(NavigationBBT2DbContext context)
        {
            _client = _api.Initial();
        }

        // GET: NavigationGroups
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage res = await _client.GetAsync(SD.ClientApiNavigationGroups);
            IEnumerable<NavigationGroup> navigationGroups = null;
            if (res.IsSuccessStatusCode)
            {
                navigationGroups = JsonConvert.DeserializeObject<IEnumerable<NavigationGroup>>(res.Content.ReadAsStringAsync().Result);
            }

            return View(navigationGroups);
        }

        // GET: NavigationGroups/Details/5
        public async Task<IActionResult> Details(string id)
        {

            HttpResponseMessage res = await _client.GetAsync(SD.ClientApiNavigationGroups + "/" + id);
            if (id == null)
            {
                return NotFound();
            }
            IEnumerable<NavigationGroup> navigationGroups = null;
            navigationGroups = JsonConvert.DeserializeObject<IEnumerable<NavigationGroup>>(res.Content.ReadAsStringAsync().Result);
            NavigationGroup navigationGroup = navigationGroups.FirstOrDefault();
            if (navigationGroup == null)
            {
                return NotFound();
            }
            return View(navigationGroup);

        }

        // GET: NavigationGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NavigationGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NavigationGroup navigationGroup)
        {
            if (ModelState.IsValid)
            {
                var myContent = JsonConvert.SerializeObject(navigationGroup);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpClient client = _api.Initial();
                HttpResponseMessage res = await client.PostAsync(SD.ClientApiNavigationGroups, byteContent);
                return RedirectToAction(nameof(Index));
            }
            return View(navigationGroup);
        }

        // GET: NavigationGroups/Edit/5
        public async Task<IActionResult> Edit(string id)
        {


            HttpResponseMessage res = await _client.GetAsync(SD.ClientApiNavigationGroups + "/" + id);
            IEnumerable<NavigationGroup> navigationGroups = null;

            if (id == null)
            {
                return NotFound();
            }

            navigationGroups = JsonConvert.DeserializeObject<IEnumerable<NavigationGroup>>(res.Content.ReadAsStringAsync().Result);

            NavigationGroup navigationGroup = navigationGroups.FirstOrDefault();

            if (navigationGroup == null)
            {
                return NotFound();
            }
            return View(navigationGroup);
        }

        // POST: NavigationGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("TenantId,Id,Name,Platform")] NavigationGroup navigationGroup)
        //{
        //    if (id != navigationGroup.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(navigationGroup);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!NavigationGroupExists(navigationGroup.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(navigationGroup);
        //}

        //// GET: NavigationGroups/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var navigationGroup = await _context.NavigationGroup
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (navigationGroup == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(navigationGroup);
        //}

        // POST: NavigationGroups/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var navigationGroup = await _context.NavigationGroup.FindAsync(id);
        //    _context.NavigationGroup.Remove(navigationGroup);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool NavigationGroupExists(string id)
        //{
        //    return _context.NavigationGroup.Any(e => e.Id == id);
        //}
    }
}
