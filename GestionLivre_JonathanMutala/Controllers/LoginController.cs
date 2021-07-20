﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionLivre_JonathanMutala.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace GestionLivre_JonathanMutala.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration Myconfiguration;

       
        public LoginController(IConfiguration iConfiguration)
        {
            Myconfiguration = iConfiguration;
        }
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

            using (SqlConnection sqlConnection = new SqlConnection(Myconfiguration.GetConnectionString("MyConnectionString")))
            {
                DataTable dataTable = new DataTable();
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Validate_User", sqlConnection);
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("UserName",userModel.UserName );
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("Password", userModel.Password);

                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                if(userModel.UserName == null && userModel.Password == null)
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