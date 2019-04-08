using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        readonly ILogger<NavigationGroupsController> _log;

        public string ErrorMessage { get; set; }

        public NavigationGroupsController(ILogger<NavigationGroupsController> log)
        {
            _client = (new ApiHelper()).Initial();
            _log = log;
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
                HttpResponseMessage res = await _client.PostAsync(SD.ClientApiNavigationGroups, byteContent);
                return RedirectToAction(nameof(Index));
            }
            return View(navigationGroup);
        }

        // GET: NavigationGroups/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            HttpResponseMessage res = await _client.GetAsync(SD.ClientApiNavigationGroups + "/" + id);
            NavigationGroup navigationGroup = null;
            if (id == null)
            {
                return NotFound();
            }
            navigationGroup = JsonConvert.DeserializeObject<NavigationGroup>(res.Content.ReadAsStringAsync().Result);
            if (navigationGroup == null)
            {
                return NotFound();
            }
            return View(navigationGroup);
        }

        // POST: NavigationGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, NavigationGroup navigationGroup)
        {
            if (id != navigationGroup.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var myContent = JsonConvert.SerializeObject(navigationGroup);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage res = await _client.PutAsync(SD.ClientApiNavigationGroups + "/" + id, byteContent);

                bool resSuccess = res.IsSuccessStatusCode;
                var resReason = res.ReasonPhrase;

                //return RedirectToAction(nameof(Index));

                //return Error();
                var notFoundResponse = new HttpResponseMessage(HttpStatusCode.NotFound);
                //throw new ArgumentException("Data is null");
                string errorMessage = "Message: 404 Not Found - from external API";
                _log.LogError(errorMessage);
                throw new ExternalException(errorMessage);

            }



            return View(navigationGroup);
        }

        // GET: NavigationGroups/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            HttpResponseMessage res = await _client.GetAsync(SD.ClientApiNavigationGroups + "/" + id);
            NavigationGroup navigationGroup = null;
            if (id == null)
            {
                return NotFound();
            }
            navigationGroup = JsonConvert.DeserializeObject<NavigationGroup>(res.Content.ReadAsStringAsync().Result);
            if (navigationGroup == null)
            {
                return NotFound();
            }
            return View(navigationGroup);
        }

        //POST: NavigationGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            HttpResponseMessage res = await _client.DeleteAsync(SD.ClientApiNavigationGroups + "/" + id);
            return RedirectToAction(nameof(Index));
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {

                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                ErrorMessage = ErrorMessage
            });
        }



        //private bool NavigationGroupExists(string id)
        //{
        //    return _context.NavigationGroup.Any(e => e.Id == id);
        //}
    }
}
