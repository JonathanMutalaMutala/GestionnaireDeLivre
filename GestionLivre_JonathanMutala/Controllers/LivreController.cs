using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionLivre_JonathanMutala.Data;
using GestionLivre_JonathanMutala.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data; 

namespace GestionLivre_JonathanMutala.Controllers
{
    public class LivreController : Controller
    {
        private readonly IConfiguration Myconfiguration;

        public LivreController(IConfiguration configuration)
        {
            this.Myconfiguration = configuration;
        }

        // GET: Livre
        public IActionResult Index()
        {
            DataTable dataTable = new DataTable(); 
            using (SqlConnection sqlConnection = new SqlConnection(Myconfiguration.GetConnectionString("MyConnectionString")))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("AllLivreView", sqlConnection);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.Fill(dataTable); 
            }
            return View(dataTable);
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
                    using (SqlConnection sqlConnection = new SqlConnection(Myconfiguration.GetConnectionString("MyConnectionString")))
                    {
                        sqlConnection.Open();
                        SqlCommand sqlcmd = new SqlCommand("LivreAddOrEdit", sqlConnection);
                        sqlcmd.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlcmd.Parameters.AddWithValue("IDLivre", livreModel.IDLivre);
                        sqlcmd.Parameters.AddWithValue("Titre", livreModel.TitreLivre);
                        sqlcmd.Parameters.AddWithValue("Auteur", livreModel.AuteurLivre);
                        sqlcmd.Parameters.AddWithValue("DatePublication", livreModel.DatePublicationLivre);
                        sqlcmd.ExecuteNonQuery();

                    }

                }
                catch(Exception e)
                {
                    Console.WriteLine(e); 
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
