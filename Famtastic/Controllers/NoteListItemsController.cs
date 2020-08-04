using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.Crud;
using DataAccess.Entity;
using Famtastic.Util;

namespace Famtastic.Controllers
{
    public class NoteListItemsController : Controller
    {
        private readonly FamDbContext _context;
        private readonly IDbReadService _dbRead;
        private readonly IDbWriteService _dbWrite;

        private static int noteId = 0;
                
		public NoteListItemsController(FamDbContext context, IDbReadService dbRead, IDbWriteService dbWrite)
        {
            _context = context;
            _dbRead = dbRead;
            _dbWrite = dbWrite;
        }

        public async Task<IActionResult> Index(int? Id)
        {
            if (Id != null)
            {
                noteId = (int)Id;
            }
            
			_dbRead.IncludeEntityNavigation<Note>();
			var noteList = await _dbRead.GetAllRecordsAsync<NoteListItem>(n => n.NoteId.Equals(Id));
            var originalNote = await _dbRead.GetSingleRecordAsync<Note>(n => n.Id.Equals(Id));
            ViewData["NoteTitle"] = !string.IsNullOrEmpty(originalNote.Title);
            ViewData["NoteText"] = !string.IsNullOrEmpty(originalNote.Text);
            ViewData["NoteId"] = originalNote.Id;
            return View(noteList);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _dbRead.IncludeEntityNavigation<Note>();
            var noteListItem = await _dbRead.GetSingleRecordAsync<NoteListItem>(n => n.Id.Equals(id));
            if (noteListItem == null)
            {
                return NotFound();
            }

            return View(noteListItem);
        }

        public IActionResult Create()
        {
            ViewData["NoteId"] = new SelectList(_context.Note, "Id", "Id", noteId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NoteId,NoteListItem1,CreatedOn,CreatedBy")] NoteListItem noteListItem)
        {
            if (ModelState.IsValid)
            {
                noteListItem.CreatedBy = User.Identity.Name;
                noteListItem.CreatedOn = DateTime.Now;
                _dbWrite.Add(noteListItem);
                await _dbWrite.SaveChangesAsync();
                noteId = 0;
                return RedirectToAction("Index", "NoteListItems", new { Id = noteListItem.NoteId });
            }
            ViewData["NoteId"] = new SelectList(_context.Note, "Id", "Id", noteListItem.NoteId);
            
            return View(noteListItem);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noteListItem = await _dbRead.GetSingleRecordAsync<NoteListItem>(n => n.Id.Equals(id));
            if (noteListItem == null)
            {
                return NotFound();
            }
            ViewData["NoteId"] = new SelectList(_context.Note, "Id", "Id", noteListItem.NoteId);
            return View(noteListItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NoteId,NoteListItem1,CreatedOn,CreatedBy")] NoteListItem noteListItem)
        {
            if (id != noteListItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    noteListItem.CreatedBy = User.Identity.Name;
                    noteListItem.CreatedOn = DateTime.Now;

                    _dbWrite.Update(noteListItem);
                    await _dbWrite.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteListItemExists(noteListItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                noteId = 0;
                return RedirectToAction("Index", "NoteListItems", new { Id = noteListItem.NoteId });
            }
            ViewData["NoteId"] = new SelectList(_context.Note, "Id", "Id", noteListItem.NoteId);
            return View(noteListItem);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _dbRead.IncludeEntityNavigation<Note>();
			var noteListItem = await _dbRead.GetSingleRecordAsync<NoteListItem>(n => n.Id.Equals(id));
            if (noteListItem == null)
            {
                return NotFound();
            }

            return View(noteListItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
			var noteListItem = await _dbRead.GetSingleRecordAsync<NoteListItem>(n => n.Id.Equals(id));
			_dbWrite.Delete(noteListItem);
            await _dbWrite.SaveChangesAsync();
            
            return RedirectToAction("Index", "NoteListItems", new { Id = noteListItem.NoteId });
        }

        private bool NoteListItemExists(int id)
        {
            return _context.NoteListItem.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
		public IActionResult _NoteListItemCreate()
        {
	        throw new NotImplementedException();
        }
    }
}
