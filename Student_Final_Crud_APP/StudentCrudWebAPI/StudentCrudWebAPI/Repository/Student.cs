using StudentCrudWebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace StudentCrudWebAPI.Repository
{
    public class Student : IStudent
    {

        private readonly IConfiguration _configuration;
        string connectionStr;
        public Student(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionStr = configuration.GetConnectionString("DBConn");
        }


        public void Add(StudentModel student)
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();
                string sp = "SHUBHAM_STUDENT_SPCREATE";
                using (SqlCommand command = new SqlCommand(sp, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", student.Name);
                    command.Parameters.AddWithValue("@FatherName", student.FatherName);
                    command.Parameters.AddWithValue("@MotherName", student.MotherName);
                    command.Parameters.AddWithValue("@Gender", student.Gender);
                    command.Parameters.AddWithValue("@DOB", student.DOB);
                    command.Parameters.AddWithValue("@AadharNo", student.AadharNo);
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();
                string SP = "SHUBHAM_STUDENT_SPDELETE";
                using (SqlCommand command = new SqlCommand(SP, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    int rowcount = command.ExecuteNonQuery();
                    return rowcount > 0;
                }
            }
        }

        public List<StudentModel> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();
                string sp = "SHUBHAM_STUDENT_SPGET";
                using (SqlCommand command = new SqlCommand(sp, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    List<StudentModel> StdData = new List<StudentModel>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StudentModel std = new StudentModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                FatherName = reader["FatherName"].ToString(),
                                MotherName = reader["MotherName"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                DOB = Convert.ToDateTime(reader["DOB"]),
                                AadharNo = Convert.ToInt64(reader["AadharNo"])
                            };
                            StdData.Add(std);
                        }
                    }
                    return StdData;
                }
            }


        }

        public StudentModel GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
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
                            StudentModel stdDATA = new StudentModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                FatherName = reader["FatherName"].ToString(),
                                MotherName = reader["MotherName"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                DOB = Convert.ToDateTime(reader["DOB"]),
                                AadharNo = Convert.ToInt64(reader["AadharNo"])
                            };
                            return stdDATA;
                        }
                        return null;
                    }
                }
            }
        }

        public bool Update(int id, StudentModel student)
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();
                string sp = "SHUBHAM_STUDENT_SPUPDATE";
                using (SqlCommand command = new SqlCommand(sp, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Name", student.Name);
                    command.Parameters.AddWithValue("@FatherName", student.FatherName);
                    command.Parameters.AddWithValue("@MotherName", student.MotherName);
                    command.Parameters.AddWithValue("@Gender", student.Gender);
                    command.Parameters.AddWithValue("@DOB", student.DOB);
                    command.Parameters.AddWithValue("@AadharNo", student.AadharNo);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
