using pawnShop.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace pawnShop.Models
{
    public class WarehouseInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class WarehousesDtoa
    {
        public List<WarehouseInfo> GetWarehouseInfo()
        {
            var warehouseInfoList = new List<WarehouseInfo>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConexion()))
            {
                conexion.Open();

                string query = "SELECT id, name FROM warehouses;";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var warehouseInfo = new WarehouseInfo
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Name = dr["name"].ToString()
                            };

                            warehouseInfoList.Add(warehouseInfo);
                        }
                    }
                }
            }

            return warehouseInfoList;
        }
    }
}
