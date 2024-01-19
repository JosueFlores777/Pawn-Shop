using pawnShop.Models;
using System.Data.SqlClient;
using System.Data;

namespace pawnShop.Data
{
    public class UsersDto
    {
        public List<ClientModel> List(string search)
        {

            var oList = new List<ClientModel>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConexion()))
            {
                conexion.Open();

                string query;
                SqlCommand cmd;

                if (!string.IsNullOrEmpty(search))
                {
                    query = "SELECT u.*, e.name AS registered_by_name\r\nFROM users u\r\nLEFT JOIN employees e ON u.id = e.id\r\nWHERE u.name = @search OR u.lastName = @search;\r\nh";
                    cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@search", search);

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new ClientModel
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Name = dr["name"].ToString(),
                                LastName = dr["lastName"].ToString(),
                                NameEmplooye = dr["registered_by_name"].ToString(),
                                IDClient = dr["IDClient"].ToString(),
                                Email = dr["email"].ToString(),
                                Password = dr["password"].ToString(),
                                Phone = dr["phone"].ToString(),
                                Role = dr["role"].ToString(),
                                CreationDate = Convert.ToDateTime(dr["creation_date"]),
                                UpdateDate = dr["modification_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["modification_date"]),
                                CreateEmployedId = Convert.ToInt32(dr["created_by_employee_id"]),
                            

                            });
                        }
                    }
                }
                else
                {
                    cmd = new SqlCommand("\r\nSELECT u.*, e.name AS registered_by_name\r\nFROM users u\r\nLEFT JOIN employees e ON u.id = e.id", conexion);
                    cmd.CommandType = CommandType.Text;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new ClientModel
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Name = dr["name"].ToString(),
                                NameEmplooye = dr["registered_by_name"].ToString(),
                                IDClient = dr["IDClient"].ToString(),
                                LastName = dr["lastName"].ToString(),
                                Email = dr["email"].ToString(),
                                Password = dr["password"].ToString(),
                                Phone = dr["phone"].ToString(),
                                Role = dr["role"].ToString(),
                                CreationDate = Convert.ToDateTime(dr["creation_date"]),
                                UpdateDate = dr["modification_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["modification_date"]),
                                CreateEmployedId = Convert.ToInt32(dr["created_by_employee_id"]),
                        
                            });
                        }
                    }
                }

                conexion.Close();
            }

            return oList;
        }

        public ClientModel Get(int id)
        {
            var oUser = new ClientModel();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConexion()))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM users WHERE id = @UserId", conexion);
                cmd.Parameters.AddWithValue("@UserId", id);
                cmd.CommandType = CommandType.Text;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oUser.Id = Convert.ToInt32(dr["id"]);
                        oUser.Name = dr["name"].ToString();
                        oUser.IDClient = dr["IDClient"].ToString();
                        oUser.LastName = dr["lastName"].ToString();
                        oUser.Email = dr["email"].ToString();
                        oUser.Password = dr["password"].ToString();
                        oUser.Phone = dr["phone"].ToString();
                        oUser.Role = dr["role"].ToString();
                        oUser.CreationDate = Convert.ToDateTime(dr["creation_date"]);
                        oUser.UpdateDate = dr["modification_date"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["modification_date"]);
                        oUser.CreateEmployedId = Convert.ToInt32(dr["created_by_employee_id"]);
                        oUser.UpdatedByEmployeeId = (dr["update_by_employee_id"] != DBNull.Value) ? Convert.ToInt32(dr["update_by_employee_id"]) : 0;
                    }

                    conexion.Close();
                }

                return oUser;
            }


        }

        public bool Save(ClientModel oUser)
        {

            int idE = 1;
            bool resp;
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConexion()))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO users (IDClient,name,lastName , email, password, phone ,role, creation_date,created_by_employee_id) VALUES (@IDClient,@Name, @lastName, @Email, @Password,  @phone ,@Role, @CreationDate,@created_by_employee_id)", conexion);
                    cmd.Parameters.AddWithValue("@IDClient", oUser.IDClient);
                    cmd.Parameters.AddWithValue("@Name", oUser.Name);
                    cmd.Parameters.AddWithValue("@lastName", oUser.LastName);
                    cmd.Parameters.AddWithValue("@Email", oUser.Email);
                    cmd.Parameters.AddWithValue("@Password", oUser.Password);
                    cmd.Parameters.AddWithValue("@phone", oUser.Phone);
                    cmd.Parameters.AddWithValue("@Role", oUser.Role);
                    cmd.Parameters.AddWithValue("@CreationDate", oUser.CreationDate);
                    cmd.Parameters.AddWithValue("created_by_employee_id", oUser.CreateEmployedId);
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

        public bool Edit(ClientModel oUser)
        {
            bool resp;
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConexion()))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE users SET IDClient = @IDClient, name = @Name, lastName = @LastName, email = @Email, password = @Password, phone = @Phone, role = @Role, modification_date = @ModificationDate, update_by_employee_id=@update_by_employee_id WHERE id = @UserId", conexion);

                    cmd.Parameters.AddWithValue("@UserId", oUser.Id);
                    cmd.Parameters.Add("@IDClient", SqlDbType.Int).Value = oUser.IDClient;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = oUser.Name;  
                    cmd.Parameters.AddWithValue("@lastName", oUser.LastName);
                    cmd.Parameters.AddWithValue("@Email", oUser.Email);
                    cmd.Parameters.AddWithValue("@Password", oUser.Password);
                    cmd.Parameters.AddWithValue("@phone", oUser.Phone);
                    cmd.Parameters.AddWithValue("@Role", oUser.Role);
                    cmd.Parameters.AddWithValue("@ModificationDate", oUser.UpdateDate);
                    cmd.Parameters.AddWithValue("@update_by_employee_id", oUser.UpdatedByEmployeeId);

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

        public bool Delete(int idUser)
        {
            bool resp;
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConexion()))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM users WHERE id = @UserId", conexion);
                    cmd.Parameters.AddWithValue("@UserId", idUser);
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
