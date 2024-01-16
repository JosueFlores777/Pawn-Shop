using pawnShop.Models;
using System.Data.SqlClient;
using System.Data;

namespace pawnShop.Data
{
    public class UsersDto
    {
        public List<UsersModel> List(string search) { 

            var oList = new List<UsersModel>();
            var cn =new Conexion();
          
            using (var conexion = new SqlConnection(cn.getConexion()))
            {
                conexion.Open();

                string query;
                SqlCommand cmd;

                if (!string.IsNullOrEmpty(search))
                {
                    query = "SELECT * FROM users WHERE name = @search OR lastName = @search";
                    cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@search", search);
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new UsersModel
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Name = dr["name"].ToString(),
                                LastName = dr["lastName"].ToString(),
                                Email = dr["email"].ToString(),
                                Password = dr["password"].ToString(),
                                Phone = dr["phone"].ToString(),
                                Role = dr["role"].ToString(),
                                CreationDate = Convert.ToDateTime(dr["creation_date"]),
                                UpdateDate = dr["modification_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["modification_date"])

                            });
                        }
                    }
                }
                else {
                    cmd = new SqlCommand("Select * from users", conexion);
                    cmd.CommandType = CommandType.Text;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new UsersModel
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Name = dr["name"].ToString(),
                                LastName = dr["lastName"].ToString(),
                                Email = dr["email"].ToString(),
                                Password = dr["password"].ToString(),
                                Phone = dr["phone"].ToString(),
                                Role = dr["role"].ToString(),
                                CreationDate = Convert.ToDateTime(dr["creation_date"]),
                                UpdateDate = dr["modification_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["modification_date"])

                            });
                        }
                    }
                }
                 
                conexion.Close();
            }

            return oList; 
        }

        public UsersModel Get(int id)
        {
            var oUser = new UsersModel();
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
                        oUser.LastName = dr["lastName"].ToString();
                        oUser.Email = dr["email"].ToString();
                        oUser.Password = dr["password"].ToString();
                        oUser.Phone = dr["phone"].ToString();
                        oUser.Role = dr["role"].ToString();
                        oUser.CreationDate = Convert.ToDateTime(dr["creation_date"]);
                        oUser.UpdateDate = dr["modification_date"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["modification_date"]);
                    }
                }

                conexion.Close();
            }

            return oUser;
        }

        public bool Save(UsersModel oUser)
        {
            bool resp;
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConexion()))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO users (name,lastName , email, password, phone ,role, creation_date) VALUES (@Name, @lastName, @Email, @Password,  @phone ,@Role, @CreationDate)", conexion);
                    cmd.Parameters.AddWithValue("@Name", oUser.Name);
                    cmd.Parameters.AddWithValue("@lastName", oUser.LastName);
                    cmd.Parameters.AddWithValue("@Email", oUser.Email);
                    cmd.Parameters.AddWithValue("@Password", oUser.Password);
                    cmd.Parameters.AddWithValue("@phone", oUser.Phone);
                    cmd.Parameters.AddWithValue("@Role", oUser.Role);
                    cmd.Parameters.AddWithValue("@CreationDate", oUser.CreationDate);

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

        public bool Edit(UsersModel oUser)
        {
            bool resp;
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConexion()))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE users SET name = @Name, lastName = @LastName, email = @Email, password = @Password, phone = @Phone, role = @Role, modification_date = @ModificationDate WHERE id = @UserId", conexion);

                    cmd.Parameters.AddWithValue("@UserId", oUser.Id); 
                    cmd.Parameters.AddWithValue("@Name", oUser.Name);
                    cmd.Parameters.AddWithValue("@lastName", oUser.LastName);
                    cmd.Parameters.AddWithValue("@Email", oUser.Email);
                    cmd.Parameters.AddWithValue("@Password", oUser.Password);
                    cmd.Parameters.AddWithValue("@phone", oUser.Phone);
                    cmd.Parameters.AddWithValue("@Role", oUser.Role);
                    cmd.Parameters.AddWithValue("@ModificationDate", oUser.UpdateDate);

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
