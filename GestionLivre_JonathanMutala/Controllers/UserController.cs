using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionLivre_JonathanMutala.Data;
using GestionLivre_JonathanMutala.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace GestionLivre_JonathanMutala.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration Myconfiguration;

        public UserController( IConfiguration iConfiguration)
        {
          Myconfiguration   = iConfiguration; 
        }
        
        //Get : Users
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("UserId","Nom","Prenom","UserName","Password")] UserModel userModel)
        {
            if(ModelState.IsValid)
            {
                using (SqlConnection sqlConnection = new SqlConnection(Myconfiguration.GetConnectionString("MyConnectionString")))
                {
                    sqlConnection.Open();
                    SqlCommand sqlcmd = new SqlCommand("CreateUser", sqlConnection);
                    sqlcmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("UserID", userModel.UserId);
                    sqlcmd.Parameters.AddWithValue("Nom", userModel.Nom);
                    sqlcmd.Parameters.AddWithValue("Prenom", userModel.Prenom);
                    sqlcmd.Parameters.AddWithValue("UserName", userModel.UserName);
                    sqlcmd.Parameters.AddWithValue("Password", userModel.Password);
                    Console.WriteLine(sqlcmd.ToString());
                    sqlcmd.ExecuteNonQuery();
                }
                return RedirectToAction("Index", "Livre");
            }
           

            return View();
        }
    }
}
