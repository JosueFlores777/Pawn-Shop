using pawnShop.Models;
using System.Data;
using System.Data.SqlClient;

namespace pawnShop.Data
{
    public class TransactionDto
    {

        public List<TransactionsModel> List(string search)
        {

            var olist = new List<TransactionsModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getConexion()))
            {
                conexion.Open();

                if (!string.IsNullOrEmpty(search))
                {
                    SqlCommand cmd = new SqlCommand("SELECT\r\n  u.id AS user_id,\r\n  u.name AS user_name,\r\n  i.name AS item_name,\r\n  s.name as name,\r\n  t.id AS transaction_id,\r\n  tt.transaction_type AS transaction_type_name,\r\n  t.repurchase_date as recupe,\r\n  p.pawn_date as pawn_date,\r\n  ti.Quantity AS item_quantity\r\nFROM\r\n  transactions t\r\nJOIN\r\n  users u ON t.user_id = u.id\r\nJOIN\r\n  transaction_types tt ON t.transaction_type_id = tt.id\r\nJOIN\r\n  shelves s ON t.shelf_id = s.id\r\nJOIN\r\n  items ti ON s.id = ti.shelf_id\r\nJOIN\r\n  pawns p ON ti.id = p.item_id AND p.shelf_id = s.id\r\nJOIN\r\n  items i ON p.item_id = i.id\r\nWHERE i.name LIKE '%' + @search + '%' OR u.name LIKE '%' + @search + '%'", conexion);

                    cmd.Parameters.AddWithValue("@search", search);


                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            olist.Add(new TransactionsModel
                            {
                                Id = Convert.ToInt32(dr["transaction_id"]),
                                Repurchase = Convert.ToDateTime(dr["recupe"]),
                                Shelves = new ShelvesModel
                                {

                                    Name = dr["name"].ToString(),
                                },

                                Users = new ClientModel
                                {
                                    Id= Convert.ToInt32(dr["user_id"]),
                                    Name = dr["user_name"].ToString(),
                                },
                                items = new ItemsModel
                                {
                                    Name = dr["item_name"].ToString(),
                                    Quatity = Convert.ToInt32(dr["item_quantity"])
                                },
                                TransactionType = new TransactionTypeModel
                                {
                                    Type = dr["transaction_type_name"].ToString()
                                }

                            });

                        }
                    }
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("SELECT\r\n  u.id AS user_id,\r\n  u.name AS user_name,\r\n  i.name AS item_name,\r\n  s.name as name,\r\n  t.id AS transaction_id,\r\n  tt.transaction_type AS transaction_type_name,\r\n  t.repurchase_date as recupe,\r\n  p.pawn_date as pawn_date,\r\n  ti.Quantity AS item_quantity\r\nFROM\r\n  transactions t\r\nJOIN\r\n  users u ON t.user_id = u.id\r\nJOIN\r\n  transaction_types tt ON t.transaction_type_id = tt.id\r\nJOIN\r\n  shelves s ON t.shelf_id = s.id\r\nJOIN\r\n  items ti ON s.id = ti.shelf_id\r\nJOIN\r\n  pawns p ON ti.id = p.item_id AND p.shelf_id = s.id\r\nJOIN\r\n  items i ON p.item_id = i.id", conexion);


                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            olist.Add(new TransactionsModel
                            {
                                Id = Convert.ToInt32(dr["transaction_id"]),
                                Repurchase = Convert.ToDateTime(dr["recupe"]),
                                Shelves = new ShelvesModel
                                {

                                    Name = dr["name"].ToString(),
                                },

                                Users = new ClientModel
                                {
                                    Id = Convert.ToInt32(dr["user_id"]),
                                    Name = dr["user_name"].ToString(),
                                },
                                items = new ItemsModel
                                {
                                    Name = dr["item_name"].ToString(),
                                    Quatity = Convert.ToInt32(dr["item_quantity"])
                                },
                                TransactionType = new TransactionTypeModel
                                {
                                    Type = dr["transaction_type_name"].ToString()
                                }

                            });

                        }
                    }

                }

            }


            return olist;
        }

        public bool Save(TransactionsModel transactionsModel) {
            bool resp;

            var cn = new Conexion();

            try{
                using (var conexion = new SqlConnection(cn.getConexion()))
                {
                    conexion.Open();
                    using (var cmd = new SqlCommand("InsertData", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                      
                        cmd.Parameters.AddWithValue("@itemName ", transactionsModel.items.Name);
                        cmd.Parameters.AddWithValue("@itemDescription", transactionsModel.items.Description);
                        cmd.Parameters.AddWithValue("@estimatedValue",transactionsModel.items.EstimatedValue);
                        cmd.Parameters.AddWithValue("@shelfId",transactionsModel.SelectedShelvesId );
                        cmd.Parameters.AddWithValue("@pawnDate", transactionsModel.Pawns.Creation);
                        cmd.Parameters.AddWithValue("@pawnUserId",transactionsModel.Users.Id);
                        cmd.Parameters.AddWithValue("@repurchaseDate", transactionsModel.Repurchase);

                        cmd.Parameters.AddWithValue("@transactionUserId",transactionsModel.Users.Id );
                        cmd.Parameters.AddWithValue("@transactionTypeId",transactionsModel.SelectedTransactionTypeId );
                        cmd.Parameters.AddWithValue("@transactionAmount",transactionsModel.Amount );
                        cmd.Parameters.AddWithValue("@transactionDate",transactionsModel.transactionDate);
                        cmd.Parameters.AddWithValue("@quantity",transactionsModel.items.Quatity );
    
                        int rowsAffected = cmd.ExecuteNonQuery();
                        resp = rowsAffected > 0;
                    }


                }

            }
            catch (Exception ex)
            {

                resp = false;
            }


                return resp;
        
        }





        #region ListCOMBO
        public List<TransactionTypeModel> ListType()
        {
            var olist = new List<TransactionTypeModel>();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getConexion()))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("SELECT id, transaction_type as [Name] FROM transaction_types", conexion);

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        olist.Add(new TransactionTypeModel
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            Type = dr["Name"].ToString(),

                        });
                    }
                }
            }

            return olist;
        }

        public List<ShelvesModel> ListShel()
        {
            var olist = new List<ShelvesModel>();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getConexion()))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("SELECT id, name as [Name] FROM shelves", conexion);

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        olist.Add(new ShelvesModel
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            Name = dr["Name"].ToString(),

                        });
                    }
                }
            }

            return olist;
        }

        #endregion

    }
}
