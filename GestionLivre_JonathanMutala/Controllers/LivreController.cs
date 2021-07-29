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
using GestionLivre_JonathanMutala.Controllers;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace GestionLivre_JonathanMutala.Controllers
{
    public class LivreController : Controller
    {
        private readonly IConfiguration Myconfiguration;
        public LivreController(IConfiguration configuration,IWebHostEnvironment hostEnvironment)
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
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("AllLivreView",sqlConnection); 
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                // Affichage de livre selon le user connecté
                //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT IDLivre,Titre,Auteur,DatePublication FROM Livre INNER JOIN Livre_User_Tab ON id_livre_FK = IDLivre INNER JOIN UserTab ON id_user_FK = UserID where UserID = '" + LoginController.idUser + "'", sqlConnection);
                sqlDataAdapter.Fill(dataTable);
            }
            return View(dataTable);
        }

        // GET: Livre/AddOrEdit 

        public IActionResult AddOrEdit(int? id)
        {
            LivreModel livreModel = new LivreModel();
            if (id > 0)
                livreModel = GetLivreByID(id); 
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
               
                    using (SqlConnection sqlConnection = new SqlConnection(Myconfiguration.GetConnectionString("MyConnectionString")))
                    {
                        sqlConnection.Open();
                    
                        SqlCommand sqlcmd = new SqlCommand("LivreAddOrEdit", sqlConnection);
                    
                     sqlcmd.CommandType = System.Data.CommandType.StoredProcedure;
                     sqlcmd.Parameters.AddWithValue("IDLivre", livreModel.IDLivre);
                    sqlcmd.Parameters.AddWithValue("Titre", livreModel.TitreLivre);
                     sqlcmd.Parameters.AddWithValue("Auteur", livreModel.AuteurLivre);
                     sqlcmd.Parameters.AddWithValue("DatePublication", livreModel.DatePublicationLivre.ToShortDateString());
                    
                    sqlcmd.ExecuteNonQuery();

                    }
                    
                
                return RedirectToAction(nameof(Index));
            }
            return View(livreModel);
        }
        /*
        public IActionResult getImageFile(LivreModel livreModel)
        {
            if(ModelState.IsValid)
            { 
            string wwwRoot = iwebHostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(livreModel.imageFile.FileName);
            string fileExtension = Path.GetExtension(livreModel.imageFile.FileName);
            livreModel.ImagePath = fileName  =  fileName = fileName + DateTime.Now.ToShortDateString() + fileExtension;
            string path = Path.Combine(wwwRoot + "/Image", fileName); 
           using (SqlConnection sqlConnection = new SqlConnection(Myconfiguration.GetConnectionString("MyConnectionString")))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("LivreAddOrEdit", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("ImagePath", livreModel.ImagePath); 
            }
            return RedirectToAction(nameof(Index));
            }
            return View(livreModel); 

        }
        */

        // GET: Livre/Delete/5
        public IActionResult Delete(int? id)
        {
            LivreModel livreModel = GetLivreByID(id); 

            return View(livreModel);
        }

        // POST: Livre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Myconfiguration.GetConnectionString("MyConnectionString")))
            {
                sqlConnection.Open();
                SqlCommand sqlcmd = new SqlCommand("DeleteLivreByID", sqlConnection);
                sqlcmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("IDLivre", id);
                sqlcmd.ExecuteNonQuery();

            }
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public LivreModel GetLivreByID(int ? id)
        {
            LivreModel livreModel = new LivreModel();
           
            using (SqlConnection sqlConnection = new SqlConnection(Myconfiguration.GetConnectionString("MyConnectionString")))
            {
                DataTable dataTable = new DataTable();
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("LivreViewByID", sqlConnection);
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("IDLivre", id); 
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.Fill(dataTable);
                if(dataTable.Rows.Count == 1)
                {
                    livreModel.IDLivre = Convert.ToInt32(dataTable.Rows[0]["IDLivre"].ToString());
                    livreModel.TitreLivre = dataTable.Rows[0]["Titre"].ToString();
                    livreModel.AuteurLivre = dataTable.Rows[0]["Auteur"].ToString();
                    livreModel.DatePublicationLivre =Convert.ToDateTime(dataTable.Rows[0]["DatePublication"]);
                }
                return livreModel; 
            }
        }

    }

}
