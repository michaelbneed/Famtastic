using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.Crud;
using DataAccess.Dto;
using DataAccess.Entity;
using DataAccess.Identity;
using Microsoft.AspNetCore.Identity;

namespace Famtastic.Controllers
{
    public class NotesController : Controller
    {
	    private readonly FamDbContext _context;
	    private readonly SignInManager<AspNetUser> _signInManager;
        private readonly IDbReadService _dbRead;
        private readonly IDbWriteService _dbWrite;

		public NotesController(FamDbContext context, SignInManager<AspNetUser> signInManager, IDbReadService dbRead, IDbWriteService dbWrite)
		{
			_context = context;
	        _signInManager = signInManager;
            _dbRead = dbRead;
            _dbWrite = dbWrite;
		}

        public async Task<IActionResult> Index(string search, bool showFamilyNotes)
        {
	        if (!_signInManager.IsSignedIn(User))
	        {
		        return RedirectToPage("/Account/Login", new { Area = "Identity" });
	        }

	        if (search != null)
	        {
		        Regex rgx = new Regex("[^a-zA-Z0-9 -]");
		        search = rgx.Replace(search, "").ToUpper();
	        }

	        ViewData["FilterParam"] = search;

			List<Note> notes = new List<Note>();
			if (showFamilyNotes)
	        {
		        notes = await _dbRead.GetAllRecordsAsync<Note>(n => !n.ShareToFam.Equals(null) && n.ShareToFam.Equals(true) && n.UserProfile.FamilyId == UserData.FamilyId);
	        }
	        else
	        {
				notes = await _dbRead.GetAllRecordsAsync<Note>(n => n.UserProfileId == UserData.UserProfileId);
			}

	        var notesEnumerable = notes.AsEnumerable();
            if (!String.IsNullOrEmpty(search))
            {
	            notesEnumerable = notes.Where(s => s.Text != null && s.Text.ToUpper().Contains(search)

	                                            || s.Title != null && s.Title.ToUpper().Contains(search));
            }

            return View(notesEnumerable.ToList().OrderByDescending(n => n.Id));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			var note = await _dbRead.GetSingleRecordAsync<Note>(n => n.Id.Equals(id));

            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Text,Advanced,DueDate,UserProfileId,ShareToFam,CreatedOn,CreatedBy")] Note note)
        {
	        if (ModelState.IsValid)
            {
	            note.UserProfileId = UserData.UserProfileId;
				note.CreatedOn = DateTime.Now;
				note.CreatedBy = UserData.UserId;
                
				_dbWrite.Add(note);
                await _dbWrite.SaveChangesAsync();
                if (note.Advanced == true)
                {
                    return RedirectToAction("Create", "NoteListItems", new { noteid = note.Id });
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "Id", "UserId", note.UserProfileId);

            return View(note);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _dbRead.GetSingleRecordAsync<Note>(n => n.Id.Equals(id));
            if (note == null)
            {
                return NotFound();
            }
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "Id", "UserId", note.UserProfileId);
            return View(note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Text,Advanced,DueDate,UserProfileId,ShareToFam,CreatedOn,CreatedBy")] Note note)
        {
            if (id != note.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
	                note.UserProfileId = UserData.UserProfileId;
					
					_dbWrite.Update(note);
                    await _dbWrite.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(note.Id))
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
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "Id", "UserId", note.UserProfileId);
            return View(note);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			var note = await _dbRead.GetSingleRecordAsync<Note>(n => n.Id.Equals(id));
                
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var note = await _context.Note.FindAsync(id);
            _dbWrite.Delete(note);
            await _dbWrite.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoteExists(int id)
        {
            return _context.Note.Any(e => e.Id == id);
        }
    }
}
