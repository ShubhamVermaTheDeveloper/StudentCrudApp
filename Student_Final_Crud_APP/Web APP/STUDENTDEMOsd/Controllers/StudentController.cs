using Microsoft.AspNetCore.Mvc;   
using Microsoft.Extensions.Configuration;
using StudentCrudApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using NuGet.Protocol.Plugins;

namespace StudentCrudApp.Controllers
{
    // inherite the controller class that is used to provide the funcationality for handling HTTP requests and returning in an MVC Application
    public class StudentController : Controller
    {
        // Interface used to accessing configuration settings and values within application
        private readonly IConfiguration _STDConfig;

        // declare a variable for store connection string
        string ConnectionStr;

        // Define the constructor that accepts Iconfiguration parameter named stdConfig
        public StudentController(IConfiguration stdConfig)
        { 
            _STDConfig = stdConfig;

            // Retrive the connection string from appsetting.json file and stored inside the variable
            ConnectionStr = stdConfig.GetConnectionString("DBConn");

        }

        // Home Action Method
       public ActionResult Home()
        {
            List<StudentsMOD> StudentData = GetMethod();  //Initializing a list of studentsMOD object
            return View(StudentData);   // return the Home view by passing StudentData list 
        }


        // Action method for creating a new record 
       public ActionResult Create()
        {
            return View();
        }


        
        [HttpPost]
        public ActionResult Create(StudentsMOD StudentData)    // For handling HTTP Post request for create or save data
        {
            if (ModelState.IsValid)
            {
                insertStudentData(StudentData);
                return RedirectToAction("Home");
            }
            return View(StudentData);

        }


        public ActionResult Delete(int id)    // for handling Delete http request
        {
            StudentsMOD StudentData = GetMethod(id);
            if (StudentData == null)
            {
                return NotFound();
            }
            return View(StudentData);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)    // for confirmation of deletion
        {
            DeleteStudentData(id);
            return RedirectToAction("Home");
        }



        public ActionResult Details(int id)   // Show the student details
        {
            StudentsMOD StudentData = GetMethod(id);
            if (StudentData == null)
            {
                return NotFound();
            }
            return View(StudentData);
        }


        public ActionResult Edit(int id)     // action method for edit the student record
        {
            StudentsMOD StudentData = GetMethod(id);
            if (StudentData == null)
            {
                return NotFound();
            }
            return View(StudentData);
        }

        [HttpPost]
        public ActionResult Edit(StudentsMOD StudentData)    // edit and update the record into the database
        {
            if (ModelState.IsValid)
            {
                UpdateStudentData(StudentData);
                return RedirectToAction("Home");
            }
            return View(StudentData);
        }

            public void UpdateStudentData(StudentsMOD StudentData)   // This method perform the update functionality into the database
        {
            using (SqlConnection connection = new SqlConnection(ConnectionStr))  // provided is used to create and manage a sqlconnection object
            {
                connection.Open();
                string SP = "SHUBHAM_STUDENT_SPUPDATE";   // Update stored procedure 
                using (SqlCommand command = new SqlCommand(SP, connection))  // provided is used to create and manage a sql command
                {
                    command.CommandType = CommandType.StoredProcedure;  // indicate which type of command that the sqlcommand object will execute (query, stored procedure, text, tabledirect)
                    command.Parameters.AddWithValue("@Id", StudentData.Id);
                    command.Parameters.AddWithValue("@Name", StudentData.Name);
                    command.Parameters.AddWithValue("@FatherName", StudentData.FatherName);
                    command.Parameters.AddWithValue("@MotherName", StudentData.MotherName);
                    command.Parameters.AddWithValue("@Gender", StudentData.Gender);
                    command.Parameters.AddWithValue("@DOB", StudentData.DOB);
                    command.Parameters.AddWithValue("@AadharNo", StudentData.AadharNo);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Method for inserting student data
        public void insertStudentData(StudentsMOD StudentData)   
           {
            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SHUBHAM_STUDENT_SPCREATE", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Name", StudentData.Name);
                    command.Parameters.AddWithValue("@FatherName", StudentData.FatherName);
                    command.Parameters.AddWithValue("@MotherName", StudentData.MotherName);
                    command.Parameters.AddWithValue("@Gender", StudentData.Gender);
                    command.Parameters.AddWithValue("@DOB", StudentData.DOB);
                    command.Parameters.AddWithValue("@AadharNo", StudentData.AadharNo);
                    command.ExecuteNonQuery();
                }
            }
        }


        // For perform delete functionality
        public void DeleteStudentData(int Id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                connection.Open();
                string SP = "SHUBHAM_STUDENT_SPDELETE";
                using (SqlCommand command = new SqlCommand(SP, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", Id);
                    command.ExecuteNonQuery();
                }
            }
        }


        public List<StudentsMOD> GetMethod()
        {
            List<StudentsMOD> StudentData = new List<StudentsMOD>();

            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                connection.Open();
                string sp = "SHUBHAM_STUDENT_SPGET";
                using (SqlCommand command = new SqlCommand(sp, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StudentsMOD std = new StudentsMOD
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                FatherName = reader["FatherName"].ToString(),
                                MotherName = reader["MotherName"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                DOB = Convert.ToDateTime(reader["DOB"]),
                                AadharNo = Convert.ToInt64(reader["AadharNo"])
                            };
                            StudentData.Add(std);
                        }
                    }
                }
            }

            return StudentData;
        }

        public StudentsMOD GetMethod(int id)   
        {
            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                connection.Open();
                string sp = "SHUBHAM_STUDENT_SPGETBYID"; 
                using (SqlCommand command = new SqlCommand(sp, connection)) 
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            StudentsMOD StudentData = new StudentsMOD
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                FatherName = reader["FatherName"].ToString(),
                                MotherName = reader["MotherName"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                DOB = Convert.ToDateTime(reader["DOB"]),
                                AadharNo = Convert.ToInt64(reader["AadharNo"])
                            };
                            return StudentData;
                        }
                        return null;
                    }
                }
            }
        }


       

    }
}
