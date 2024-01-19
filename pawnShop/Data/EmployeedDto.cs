using Microsoft.AspNetCore.Components.Routing;
using pawnShop.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace pawnShop.Data
{
    [Authorize(Policy = "AdminPolicy")]
    public class EmployeedDto
    {
        public List<EmployeeModel> List(string search)
        {
            var oList = new List<EmployeeModel>();
            var cn = new Conexion();


            using (var conexion = new SqlConnection(cn.getConexion()))
            {
                conexion.Open();

                string query;
                SqlCommand cmd;
                if (!string.IsNullOrEmpty(search))
                {

                    query = "SELECT * FROM employees WHERE name LIKE '%' + @search + '%' OR lastName LIKE '%' + @search + '%'";
                    cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@search", search);
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new EmployeeModel
                            {
                                Id = Convert.ToInt32(dr["ID"]),
                                Name = dr["name"].ToString(),
                                Phone = dr["phone"].ToString(),
                                LastName = dr["LastName"].ToString(),
                                IDUser = dr["userID"].ToString(),
                                Email = dr["email"].ToString(),
                                Password = dr["password"].ToString(),
                                Role = dr["role"].ToString(),
                                HirringDate = Convert.ToDateTime(dr["hiring_date"]),
                                CreationDate = Convert.ToDateTime(dr["creation_date"]),
                                LastUpdatedDate = dr["modification_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["modification_date"]),

                            });
                        }
                    }
                }
                else
                {
                    cmd = new SqlCommand("Select * from employees", conexion);
                    cmd.CommandType = CommandType.Text;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new EmployeeModel
                            {
                                Id = Convert.ToInt32(dr["ID"]),
                                Name = dr["name"].ToString(),
                                Phone = dr["phone"].ToString(),
                                LastName = dr["LastName"].ToString(),
                                IDUser = dr["userID"].ToString(),
                                Email = dr["email"].ToString(),
                                Password = dr["password"].ToString(),
                                Role = dr["role"].ToString(),
                                HirringDate = Convert.ToDateTime(dr["hiring_date"]),
                                CreationDate = Convert.ToDateTime(dr["creation_date"]),
                                LastUpdatedDate = dr["modification_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["modification_date"]),
                            });
                        }
                    }
                }

                conexion.Close();
            }
            return oList;
        }

        public EmployeeModel Get(int id)
        {

            var oEmployeed = new EmployeeModel();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConexion()))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM employees WHERE id = @employeeId", conexion);
                cmd.Parameters.AddWithValue("@employeeId", id);
                cmd.CommandType = CommandType.Text;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oEmployeed.Id = Convert.ToInt32(dr["id"]);
                        oEmployeed.Name = dr["name"].ToString();
                        oEmployeed.Phone = dr["phone"].ToString();
                        oEmployeed.LastName = dr["lastName"].ToString();
                        oEmployeed.IDUser = dr["userID"].ToString();
                        oEmployeed.Email = dr["email"].ToString();
                        oEmployeed.Password = dr["password"].ToString();
                        oEmployeed.Role = dr["role"].ToString();
                        oEmployeed.HirringDate = Convert.ToDateTime(dr["hiring_date"]);
                        oEmployeed.CreationDate = Convert.ToDateTime(dr["creation_date"]);
                        oEmployeed.LastUpdatedDate = dr["modification_date"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["modification_date"]);
             
                    }
                }

                conexion.Close();
            }

            return oEmployeed;
        }

        public bool Save(EmployeeModel employeeModel) {
            bool reps;
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConexion()))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO employees (name,phone,lastName , userID, email, password ,role, hiring_date,creation_date) VALUES (@name,@phone, @lastName, @userID, @email,  @password ,@role, @hiring_date,@creation_date)", conexion);
                    cmd.Parameters.AddWithValue("@name", employeeModel.Name);
                    cmd.Parameters.AddWithValue("@phone", employeeModel.Phone);
                    cmd.Parameters.AddWithValue("@lastName", employeeModel.LastName);
                    cmd.Parameters.AddWithValue("@userID", employeeModel.IDUser);
                    cmd.Parameters.AddWithValue("@email", employeeModel.Email);
                    cmd.Parameters.AddWithValue("@password", employeeModel.Password);
                    cmd.Parameters.AddWithValue("@role", employeeModel.Role);
                    cmd.Parameters.AddWithValue("@hiring_date", employeeModel.HirringDate);
                    cmd.Parameters.AddWithValue("@creation_date", employeeModel.CreationDate);
                    cmd.CommandType = CommandType.Text;

                    int rowsAffected = cmd.ExecuteNonQuery();
                    reps = rowsAffected > 0;

                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                reps = false;
            }

            return reps;
        }

        public bool Edit(EmployeeModel employeeModel)
        {
            bool resp;
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConexion()))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE employees SET name = @name, phone = @phone, lastName = @lastName, userID = @userID, email = @email, password = @password, role = @role,modification_date=@modification_date WHERE id = @id", conexion);

                    cmd.Parameters.AddWithValue("@id", employeeModel.Id); 
                    cmd.Parameters.AddWithValue("@name", employeeModel.Name);
                    cmd.Parameters.AddWithValue("@phone", employeeModel.Phone);
                    cmd.Parameters.AddWithValue("@lastName", employeeModel.LastName);
                    cmd.Parameters.AddWithValue("@userID", employeeModel.IDUser);
                    cmd.Parameters.AddWithValue("@email", employeeModel.Email);
                    cmd.Parameters.AddWithValue("@password", employeeModel.Password);
                    cmd.Parameters.AddWithValue("@role", employeeModel.Role);
                    cmd.Parameters.AddWithValue("@modification_date", employeeModel.LastUpdatedDate);

                    cmd.CommandType = CommandType.Text;

                    int rowsAffected = cmd.ExecuteNonQuery();
                    resp = rowsAffected > 0;

                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                resp = false;
            }

            return resp;
        }

        public bool Delete(int EmployeedId)
        {
            bool resp;
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConexion()))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM employees WHERE id = @EmployeedId", conexion);
                    cmd.Parameters.AddWithValue("@EmployeedId", EmployeedId);
                    cmd.CommandType = CommandType.Text;

                    int rowsAffected = cmd.ExecuteNonQuery();

                    resp = rowsAffected > 0;

                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                resp = false;
            }

            return resp;
        }

    }

}
