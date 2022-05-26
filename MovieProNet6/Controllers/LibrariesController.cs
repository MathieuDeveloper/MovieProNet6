using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieProNet6.Data;
using MovieProNet6.Models.CustomCollection;

namespace MovieProNet6.Controllers
{
    public class LibrariesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LibrariesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Libraries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Library.Include(l => l.Collection).Include(l => l.Movie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Libraries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Library == null)
            {
                return NotFound();
            }

            var library = await _context.Library
                .Include(l => l.Collection)
                .Include(l => l.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (library == null)
            {
                return NotFound();
            }

            return View(library);
        }

        // GET: Libraries/Create
        public IActionResult Create()
        {
            ViewData["CollectionId"] = new SelectList(_context.Collection, "Id", "Id");
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id");
            return View();
        }

        // POST: Libraries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CollectionId,MovieId")] Library library)
        {
            if (ModelState.IsValid)
            {
                _context.Add(library);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CollectionId"] = new SelectList(_context.Collection, "Id", "Id", library.CollectionId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id", library.MovieId);
            return View(library);
        }

        // GET: Libraries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Library == null)
            {
                return NotFound();
            }

            var library = await _context.Library.FindAsync(id);
            if (library == null)
            {
                return NotFound();
            }
            ViewData["CollectionId"] = new SelectList(_context.Collection, "Id", "Id", library.CollectionId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id", library.MovieId);
            return View(library);
        }

        // POST: Libraries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CollectionId,MovieId")] Library library)
        {
            if (id != library.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(library);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibraryExists(library.Id))
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
            ViewData["CollectionId"] = new SelectList(_context.Collection, "Id", "Id", library.CollectionId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id", library.MovieId);
            return View(library);
        }

        // GET: Libraries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Library == null)
            {
                return NotFound();
            }

            var library = await _context.Library
                .Include(l => l.Collection)
                .Include(l => l.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (library == null)
            {
                return NotFound();
            }

            return View(library);
        }

        // POST: Libraries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Library == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Library'  is null.");
            }
            var library = await _context.Library.FindAsync(id);
            if (library != null)
            {
                _context.Library.Remove(library);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibraryExists(int id)
        {
          return (_context.Library?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
