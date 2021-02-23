using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private ApplicationDbContext _db;

        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }
        //when you get page
        public async Task<IActionResult> OnGet(int? id)
        {
            Book = new Book();
            //create
            if (id == null)
                return Page();

            //update
            Book = await _db.Book.FirstOrDefaultAsync(u => u.Id == id);

            if (Book == null)
                return NotFound();

            return Page();
        }

        //When you post information
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Book.Id == 0)
                    _db.Book.Add(Book);
                else
                    _db.Book.Update(Book);
                
                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }

            return RedirectToPage();
        }
        
    }
}
