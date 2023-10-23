using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Timetable_DateSheet_Generator.Data.DbContext;
using Timetable_DateSheet_Generator.Models;

namespace Timetable_DateSheet_Generator.Controllers
{
    public class InstitutesController : Controller
    {
        private readonly Timetable_DateSheet_Context _context;

        public InstitutesController(Timetable_DateSheet_Context context)
        {
            _context = context;
        }
        public IActionResult Iframe()
        {
            return this.View();
        }
        // GET: Institutes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Institutes.ToListAsync());
        }

        // GET: Institutes/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var institutes = await _context.Institutes
        //        .FirstOrDefaultAsync(m => m.InstituteID == id);
        //    if (institutes == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(institutes);
        //}

        // GET: Institutes/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Institutes/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("InstituteID,InstituteName")] Institutes institutes)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(institutes);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(institutes);
        //}

        // GET: Institutes/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var institutes = await _context.Institutes.FindAsync(id);
        //    if (institutes == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(institutes);
        //}

        // POST: Institutes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("InstituteID,InstituteName")] Institutes institutes)
        //{
        //    if (id != institutes.InstituteID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(institutes);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!InstitutesExists(institutes.InstituteID))
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
        //    return View(institutes);
        //}

        // GET: Institutes/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var institutes = await _context.Institutes
        //        .FirstOrDefaultAsync(m => m.InstituteID == id);
        //    if (institutes == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(institutes);
        //}

        //// POST: Institutes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var institutes = await _context.Institutes.FindAsync(id);
        //    _context.Institutes.Remove(institutes);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool InstitutesExists(int id)
        //{
        //    return _context.Institutes.Any(e => e.InstituteID == id);
        //}
    }
}
