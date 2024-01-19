using pawnShop.Models;
using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using pawnShop.Data;

namespace pawnShop.DataDto
{
    public class AccountDto
    {
        public bool Login(UserModel oUser, HttpContext httpContext)
        {
            bool resp = true;
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getConexion()))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM employees WHERE email = @email AND Password = @Password", conexion);


                cmd.Parameters.AddWithValue("@email", oUser.Email);
                cmd.Parameters.AddWithValue("@Password", oUser.Password);

                try
                {
                    conexion.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            oUser.Id = Convert.ToInt32(reader["Id"]);
                            oUser.Name = Convert.ToString(reader["Name"]);
                            oUser.Role = Convert.ToString(reader["Role"]);

                            httpContext.Session.SetInt32("userId", oUser.Id);
                            httpContext.Session.SetString("userName", oUser.Name);
                            httpContext.Session.SetString("userRole", oUser.Role);
                        }
                        else
                        {
                            resp = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    resp = false;
                }
            }

            return resp;
        }
    }
}
