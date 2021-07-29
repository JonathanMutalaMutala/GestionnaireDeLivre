using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionLivre_JonathanMutala.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using GestionLivre_JonathanMutala.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace GestionLivre_JonathanMutala.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration Myconfiguration;

        [ActivatorUtilitiesConstructor]
        public LoginController(IConfiguration iConfiguration)
        {
           
            Myconfiguration = iConfiguration;
        }
      public static  int idUser; 
        public LoginController()
        {
        }

        public object Session { get; private set; }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LoginValidation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoginValidation([Bind("UserId", "Nom", "Prenom", "UserName", "Password")] UserModel userModel)
        {

           // SqlConnection sqlConnection = new s
            using (SqlConnection sqlConnection = new SqlConnection(Myconfiguration.GetConnectionString("MyConnectionString")))
            {

                DataTable dataTable = new DataTable();
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Validate_User", sqlConnection);

                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("UserName",userModel.UserName );
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("Password", userModel.Password);
                SqlCommand sqlCommand = new SqlCommand("SELECT UserID from UserTab WHERE UserName = '" + userModel.UserName + "'");
                sqlCommand.Connection = sqlConnection;
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                       
                        idUser =  reader.GetInt32(0);
                        // reader.GetValue(reader.GetOrdinal("UserID"));
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                

                if (userModel.UserName == null && userModel.Password == null)
                {
                    return RedirectToAction("Create", "User");
                }
                else
                {
                    sqlDataAdapter.Fill(dataTable);



                    if (dataTable.Rows.Count < 1)
                    {
                        return RedirectToAction("Create", "User");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Livre");   
                    }
                }
               
                sqlConnection.Close();
            }

            return View();
        }

       
    }
}
