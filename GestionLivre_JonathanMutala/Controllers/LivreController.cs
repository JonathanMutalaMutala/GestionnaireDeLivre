using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionLivre_JonathanMutala.Data;
using GestionLivre_JonathanMutala.Models;

namespace GestionLivre_JonathanMutala.Controllers
{
    public class LivreController : Controller
    {
        private readonly GestionLivre_JonathanMutalaContext _context;

        public LivreController()
        {
        }

        // GET: Livre
        public IActionResult Index()
        { 
            return View();
        }

        // GET: Livre/Edit/5
        public IActionResult AddOrEdit(int? id)
        {
            LivreModel livreModel = new LivreModel(); 
            return View(livreModel);
        }

        // POST: Livre/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(int id, [Bind("IDLivre,TitreLivre,AuteurLivre,DatePublicationLivre")] LivreModel livreModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                   
                }
                catch (Exception e)
                {
                   
                }
                return RedirectToAction(nameof(Index));
            }
            return View(livreModel);
        }

        // GET: Livre/Delete/5
        public IActionResult Delete(int? id)
        {
            return View();
        }

        // POST: Livre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            return RedirectToAction(nameof(Index));
        }

    }
}
