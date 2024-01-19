using pawnShop.Models;
using System.Data.SqlClient;
using System.Data;

namespace pawnShop.Data
{
    public class WarehousesDto
    {
        #region Ware
        public List<WarehousesModel> List(string search)
        {
            var olist = new List<WarehousesModel>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConexion()))
            {
                conexion.Open();

                string query;

                if (!string.IsNullOrEmpty(search))
                {
                    query = @"
                SELECT a.id AS WarehouseId, a.name AS WarehouseName, a.location AS WarehouseLocation, 
                       b.id AS ShelfId, b.name AS ShelfName, b.capacity AS ShelfCapacity,
                       a.creation_date AS WarehouseCreation, a.modification_date AS WarehouseModification,
                       b.creation_date AS ShelfCreation, b.modification_date AS ShelfModification
                FROM warehouses a
                INNER JOIN shelves b ON a.id = b.warehouse_id 
                WHERE a.name = @search OR b.name = @search;
            ";

                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@search", search);

                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                olist.Add(new WarehousesModel
                                {
                                    // Populate Warehouse properties
                                    Id = Convert.ToInt32(dr["WarehouseId"]),
                                    Name = dr["WarehouseName"].ToString(),
                                    Location = dr["WarehouseLocation"].ToString(),
                                    creation = Convert.ToDateTime(dr["WarehouseCreation"]),
                                    updatedDate = dr["WarehouseModification"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["WarehouseModification"]),


                                    Shelves = new ShelvesModel
                                    {
                                        Id = Convert.ToInt32(dr["ShelfId"]),
                                        Name = dr["ShelfName"].ToString(),
                                        Capacity = dr["ShelfCapacity"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["ShelfCapacity"]),
                                        Created = Convert.ToDateTime(dr["ShelfCreation"]),
                                        Updated = dr["ShelfModification"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ShelfModification"]),
                                    }
                                });
                            }
                        }
                    }
                }
                else
                {
                    query = @"
                SELECT a.id AS WarehouseId, a.name AS WarehouseName, a.location AS WarehouseLocation, 
                       b.id AS ShelfId, b.name AS ShelfName, b.capacity AS ShelfCapacity,
                       a.creation_date AS WarehouseCreation, a.modification_date AS WarehouseModification,
                       b.creation_date AS ShelfCreation, b.modification_date AS ShelfModification
                FROM warehouses a
                INNER JOIN shelves b ON a.id = b.warehouse_id;
            ";

                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                olist.Add(new WarehousesModel
                                {
                                    Id = Convert.ToInt32(dr["WarehouseId"]),
                                    Name = dr["WarehouseName"].ToString(),
                                    Location = dr["WarehouseLocation"].ToString(),
                                    creation = Convert.ToDateTime(dr["WarehouseCreation"]),
                                    updatedDate = dr["WarehouseModification"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["WarehouseModification"]).Date,
                                    Shelves = new ShelvesModel
                                    {
                                        Id = Convert.ToInt32(dr["ShelfId"]),
                                        Name = dr["ShelfName"].ToString(),
                                        Capacity = dr["ShelfCapacity"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["ShelfCapacity"]),
                                        Created = Convert.ToDateTime(dr["ShelfCreation"]).Date,
                                        Updated = dr["ShelfModification"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ShelfModification"]),
                                    }
                                });
                            }
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

            using (var conexion = new SqlConnection(cn.getConexion()))
            {
                conexion.Open();

                string query = @"SELECT a.id AS warehouse_id, a.name AS warehouse_name, 
                                a.location, b.id AS shelf_id, b.name AS shelf_name, 
                                b.capacity, b.warehouse_id
                        FROM warehouses a
                        INNER JOIN shelves b ON a.id = b.warehouse_id
                        WHERE a.id = @id";

                SqlCommand cmd;
                cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandType = CommandType.Text;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        wareHouse.Id = Convert.ToInt32(dr["warehouse_id"]);
                        wareHouse.Name = dr["warehouse_name"].ToString();
                        wareHouse.Location = dr["location"].ToString();


                        var shelvesModel = new ShelvesModel
                        {
                            Id = Convert.ToInt32(dr["shelf_id"]),
                            Name = dr["shelf_name"].ToString(),
                            Capacity = dr["capacity"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["capacity"]),

                        };


                        wareHouse.Shelves = shelvesModel;
                    }
                }

                conexion.Close();
            }

            return wareHouse;
        }

        public bool Save(WarehousesModel warehousesModel)
        {

            bool rep;
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConexion()))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("insert into warehouses (name , location, creation_date) values(@name,@location,@creation_date)", conexion);
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

        public bool Edit(WarehousesModel warehousesModel)
        {
            bool resp;
            var cn = new Conexion();

            try
            {
                using (var conexion = new SqlConnection(cn.getConexion()))
                {
                    conexion.Open();

                    using (var cmd = new SqlCommand("UpdateWarehouseDetails", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.AddWithValue("@id", warehousesModel.Id);
                        cmd.Parameters.AddWithValue("@newWarehouseName", warehousesModel.Name);
                        cmd.Parameters.AddWithValue("@newLocation", warehousesModel.Location);

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

        public bool Delete(WarehousesModel warehousesModel)
        {
            bool resp;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConexion()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("Delete from warehouses where id =@id", conexion);
                    cmd.Parameters.AddWithValue("@id", warehousesModel.Id);

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

        #endregion


        #region Shave

        public List<ShelvesModel> ListShabvle(string search)
        {
            var olist = new List<ShelvesModel>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConexion()))
            {
                conexion.Open();

                string query;

                if (!string.IsNullOrEmpty(search))
                {
                    query = @"
                SELECT a.id as ShelfId, b.name as Ware,a.name AS ShelfName, b.name AS WarehouseName, 
                       a.capacity AS ShelfCapacity, 
                       a.creation_date AS ShelfCreation, 
                       a.modification_date AS ShelfModification
                FROM shelves a 
                INNER JOIN warehouses b ON a.warehouse_id = b.id 
                WHERE a.name = @search OR b.name = @search;
            ";

                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@search", search);

                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                olist.Add(new ShelvesModel
                                {
                                    Id = Convert.ToInt32(dr["ShelfId"]),
                                    Name = dr["ShelfName"].ToString(),
                                    NameWa = dr["Ware"].ToString(),
                                    Capacity = dr["ShelfCapacity"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["ShelfCapacity"]),
                                    Created = Convert.ToDateTime(dr["ShelfCreation"]),
                                    Updated = dr["ShelfModification"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ShelfModification"]),
                                }); ; ;
                            }
                        }
                    }
                }
                else
                {
                    query = @"
                SELECT a.id as ShelfId, b.name as Ware,a.name AS ShelfName, b.name AS WarehouseName, 
                       a.capacity AS ShelfCapacity, 
                       a.creation_date AS ShelfCreation, 
                       a.modification_date AS ShelfModification
                FROM shelves a 
                INNER JOIN warehouses b ON a.warehouse_id = b.id;
            ";

                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                olist.Add(new ShelvesModel
                                {
                                    Id = Convert.ToInt32(dr["ShelfId"]), 
                                    Name = dr["ShelfName"].ToString(),
                                    NameWa = dr["Ware"].ToString(),
                                    Capacity = dr["ShelfCapacity"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["ShelfCapacity"]),
                                    Created = Convert.ToDateTime(dr["ShelfCreation"]),
                                    Updated = dr["ShelfModification"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ShelfModification"]),
                                });
                            }
                        }
                    }
                }
            }

            return olist;
        }

        public ShelvesModel GetShabvle(int id)
        {
            var wareHouse = new ShelvesModel();
            var cn = new Conexion();

            try
            {
                using (var conexion = new SqlConnection(cn.getConexion()))
                {
                    conexion.Open();

                    string query = @"SELECT a.id, a.warehouse_id, a.name, b.name as [Ware House], a.capacity FROM shelves a INNER JOIN warehouses b ON a.id = @id";

                    SqlCommand cmd;
                    cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    using (var dr = cmd.ExecuteReader())
                    {
               
                        while (dr.Read())
                        {
                            wareHouse.Id = Convert.ToInt32(dr["id"]);
                            wareHouse.Name = dr["name"].ToString();
                            wareHouse.NameWa = dr["Ware House"].ToString();
                            wareHouse.idW = Convert.ToInt32(dr["warehouse_id"]);
                            wareHouse.Capacity = dr["capacity"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["capacity"]);
                        }
                    }
                } // The using block will automatically close the connection

            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine($"Error: {ex.Message}");
            }

            return wareHouse;
        }


        public bool EditShabvle(ShelvesModel shelvesModel)
        {
            bool resp;
            var cn = new Conexion();

            try
            {
                using (var conexion = new SqlConnection(cn.getConexion()))
                {
                    conexion.Open();

                    using (var cmd = new SqlCommand("UpdateShelf", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.AddWithValue("@shelfId ", shelvesModel.Id);
                        cmd.Parameters.AddWithValue("@newName ", shelvesModel.Name);
                        cmd.Parameters.AddWithValue("@newWarehouseId ", shelvesModel.idW);
                        cmd.Parameters.AddWithValue("@newCapacity  ", shelvesModel.Capacity);

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

        public bool SaveShavle(ShelvesModel warehousesModel)
        {

            bool rep;
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConexion()))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("insert into shelves (name,warehouse_id, capacity, creation_date) values(@name,@warehouse_id,@capacity,@creation_date )", conexion);
                    cmd.Parameters.AddWithValue("@name", warehousesModel.Name);
                    cmd.Parameters.AddWithValue("@warehouse_id", warehousesModel.idW);
                    cmd.Parameters.AddWithValue("@capacity", warehousesModel.Capacity);
                    cmd.Parameters.AddWithValue("@creation_date", warehousesModel.Created);

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


        public bool DeleteShavl(int Id)
        {
            bool resp;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getConexion()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("Delete from shelves where id =@id", conexion);
                    cmd.Parameters.AddWithValue("@id", Id);

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


        #endregion



    }
}
