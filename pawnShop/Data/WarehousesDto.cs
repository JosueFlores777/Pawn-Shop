using pawnShop.Models;
using System.Data.SqlClient;
using System.Data;

namespace pawnShop.Data
{
    public class WarehousesDto
    {
        public List<WarehousesModel> List(string search) {

            var olist = new List<WarehousesModel>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConexion()))
            {
                conexion.Open();

                string query;
                SqlCommand cmd;

                if (!string.IsNullOrEmpty(search)) {
                    query = "SELECT * FROM warehouses  WHERE name = @search OR location = @search";
                    cmd = new SqlCommand(query,conexion);
                    cmd.Parameters.AddWithValue("@search", search);

                    using (var dr = cmd.ExecuteReader()) { 
                        while (dr.Read())
                        {
                            olist.Add(new WarehousesModel{ 
                                Id = Convert.ToInt32(dr["id"]),
                                Name = dr["name"].ToString(),
                                Location = dr["location"].ToString(),
                                creation = Convert.ToDateTime(dr["creation_date"]),
                                updatedDate = dr["modification_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["modification_date"])
                           
                            });
                        }
                    }

                }
                else {
                    query = "Select * from warehouses ";
                    cmd = new SqlCommand(query, conexion);

                    using(var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read()) {

                            olist.Add(new WarehousesModel { 
                            Id = Convert.ToInt32(dr["id"]),
                            Name = dr["name"].ToString(),
                            Location = dr["location"].ToString(),
                            creation = Convert.ToDateTime(dr["cretion_date"]),
                            updatedDate = dr["modification_date"] == DBNull.Value? (DateTime?)null : Convert.ToDateTime(dr["modification_date"])
                            
                            });
                        
                        }

                    }

                }


            }

            return olist;
        }

        public WarehousesModel Get(int id)
        {
            var wareHouse = new WarehousesModel();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConexion())) {
            
                conexion.Open();
                
                string query = "select * from warehouses  where id = @id";
                SqlCommand cmd;
                cmd =new SqlCommand(query,conexion);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandType = CommandType.Text;

                using (var dr = cmd.ExecuteReader()) { 
                
                    while (dr.Read()) {
                        wareHouse.Id = Convert.ToInt32(dr["id"]);
                        wareHouse.Name = dr["name"].ToString();
                        wareHouse.Location = dr["location"].ToString();
                        wareHouse.creation = Convert.ToDateTime(dr["creation_date"]);
                        wareHouse.updatedDate = dr["modification_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["modification_date"]);
                    }
                
                }
                conexion.Close();
            }

            return wareHouse;
        }

        public bool Save(WarehousesModel warehousesModel) {

            bool rep;
            try {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConexion()))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("inset into warehouses (name , location, creation_date) value(@name,@location,@creation_date)", conexion);
                    cmd.Parameters.AddWithValue("@name", warehousesModel.Name);
                    cmd.Parameters.AddWithValue("@location", warehousesModel.Location);
                    cmd.Parameters.AddWithValue("@creation_date", warehousesModel.creation);
                    cmd.CommandType = CommandType.Text;

                    int rowsAffected = cmd.ExecuteNonQuery();
                    rep = rowsAffected > 0;

                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                rep = false;
            }
            return rep;
        }

        public bool Edit(WarehousesModel warehousesModel) {
            bool resp;
            var cn = new Conexion();

            try {
                using (var conexion = new SqlConnection(cn.getConexion()))
                {

                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(" update warehouse set name =@name, location = @location, modification_date = @modification_date where id = @id ", conexion);

                    using (var dr = cmd.ExecuteReader())
                    {
                        cmd.Parameters.AddWithValue("@id", warehousesModel.Id);
                        cmd.Parameters.AddWithValue("@name", warehousesModel.Name);
                        cmd.Parameters.AddWithValue("@location", warehousesModel.Location);
                        cmd.Parameters.AddWithValue("@modification_date", warehousesModel.updatedDate);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        resp = rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                resp = false;
            }

            return resp;
        }

        public bool Delete(WarehousesModel warehousesModel) {
            bool resp;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConexion()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("Delete from warehouses where id =@id",conexion);
                    cmd.Parameters.AddWithValue("@id",warehousesModel.Id);

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
