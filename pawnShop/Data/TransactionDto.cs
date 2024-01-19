using pawnShop.Models;
using System.Data.SqlClient;

namespace pawnShop.Data
{
    public class TransactionDto
    {

        public List<TransactionsModel> List(string search) { 
        
            var olist = new List<TransactionsModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConexion())) {
                conexion.Open();

                if (!string.IsNullOrEmpty(search))
                {
                    SqlCommand cmd = new SqlCommand("SELECT u.id, u.name AS [Name Customer], i.name AS [Name Product], i.Quantity AS [Stonk], s.name AS [Name Shelf], t.repurchase_date as [Repurchase], tp.transaction_type as [Type]\r\n" +
                               "FROM users u\r\n" +
                               "INNER JOIN items i ON u.id = i.id\r\n" +
                               "INNER JOIN pawns p ON i.id = p.item_id\r\n" +
                               "INNER JOIN shelves s ON p.shelf_id = s.id\r\n" +
                               "INNER JOIN transactions t ON u.id = t.id\r\n" +
                               "INNER JOIN transaction_types tp ON t.transaction_type_id = tp.id\r\n" +
                               "WHERE i.name LIKE '%' + @search + '%' OR u.name LIKE '%' + @search + '%'", conexion);

                    cmd.Parameters.AddWithValue("@search", search);


                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            olist.Add(new TransactionsModel
                            {
                                Id = Convert.ToInt32(dr["ID"]),
                                Repurchase = Convert.ToDateTime(dr["Repurchase"]),
                                Shelves = new ShelvesModel
                                {
                                   
                                    Name = dr["Name Shelf"].ToString(),
                                },

                                Users = new ClientModel
                                {

                                    Name = dr["Name Customer"].ToString(),
                                },
                                items = new ItemsModel
                                {
                                    Name = dr["Name Product"].ToString(),
                                    Quatity = Convert.ToInt32(dr["Stonk"])
                                },
                                TransactionType = new TransactionTypeModel
                                {
                                    Type = dr["Type"].ToString()
                                }

                            });

                        }
                    }
                }
                else {
                    SqlCommand cmd = new SqlCommand("SELECT u.id,  u.name AS [Name Customer],  i.name AS [Name Product],  i.Quantity AS [Stonk], s.name AS [Name Shelf],  t.repurchase_date as [Repurchase], tp.transaction_type as [Type]\r\nFROM\r\n  users u\r\nINNER JOIN\r\n  items i ON u.id = i.id \r\nINNER JOIN\r\n  pawns p ON i.id = p.item_id\r\nINNER JOIN\r\n  shelves s ON p.shelf_id = s.id \r\nINNER JOIN\r\n  transactions t ON u.id = t.id \r\nINNER JOIN\r\n  transaction_types tp ON t.transaction_type_id = tp.id;", conexion);
                 

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            olist.Add(new TransactionsModel
                            {
                                Id = Convert.ToInt32(dr["ID"]),
                                Repurchase = Convert.ToDateTime(dr["Repurchase"]),
                                Shelves = new ShelvesModel
                                {
                                 
                                    Name = dr["Name Shelf"].ToString(),
                                },

                                Users = new ClientModel
                                {

                                    Name = dr["Name Customer"].ToString(),
                                },
                                items = new ItemsModel
                                {
                                    Name = dr["Name Product"].ToString(),
                                    Quatity = Convert.ToInt32(dr["Stonk"])
                                },
                                TransactionType = new TransactionTypeModel
                                {
                                    Type = dr["Type"].ToString()
                                }

                            });

                        }
                    }

                }
            
            }


            return olist;
        }


    }
}
