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
                    SqlCommand cmd = new SqlCommand("SELECT\r\n    pawns.id AS pawnid,\r\n    users.name AS username,\r\n    items.name AS itemname,\r\n    pawns.recovery as recovery,\r\n\titems.Quantity as Quantity,\r\n\tpawns.creation_date as creation,\r\n    shelves.name AS shelfname\r\nFROM\r\n    pawns\r\nJOIN\r\n    users ON pawns.user_id = users.id\r\nJOIN\r\n    items ON pawns.item_id = items.id\r\nJOIN\r\n    shelves ON pawns.shelf_id = shelves.id\r\nwhere items.name  like '%' +@search+'%' or users.name like '%' +@search+'%'", conexion);

                    cmd.Parameters.AddWithValue("@search", search);


                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            olist.Add(new TransactionsModel
                            {
             

                                Shelves = new ShelvesModel
                                {

                                    Name = dr["shelfname"].ToString(),
                                },

                                Users = new ClientModel
                                {

                                    Name = dr["username"].ToString(),
                                },
                                items = new ItemsModel
                                {
                                    Name = dr["itemname"].ToString(),
                                    Quatity = Convert.ToInt32(dr["Quantity"])
                                },

                                Pawns = new PawnsModel {
                                    Id = Convert.ToInt32(dr["pawnid"]),
                                    Creation= Convert.ToDateTime(dr["creation"]),
                                    pawn_date = Convert.ToDateTime(dr["recovery"]),
                                }

                            });

                        }
                    }
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("SELECT\r\n    pawns.id AS pawnid,\r\n    users.name AS username,\r\n    items.name AS itemname,\r\n    pawns.recovery as recovery,\r\n\titems.Quantity as Quantity,\r\n\tpawns.creation_date as creation,\r\n    shelves.name AS shelfname\r\nFROM\r\n    pawns\r\nJOIN\r\n    users ON pawns.user_id = users.id\r\nJOIN\r\n    items ON pawns.item_id = items.id\r\nJOIN\r\n    shelves ON pawns.shelf_id = shelves.id", conexion);


                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            olist.Add(new TransactionsModel
                            {
                               
                                Shelves = new ShelvesModel
                                {

                                    Name = dr["shelfname"].ToString(),
                                },

                                Users = new ClientModel
                                {
         
                                    Name = dr["username"].ToString(),
                                },
                                items = new ItemsModel
                                {
                                    Name = dr["itemname"].ToString(),
                                    Quatity = Convert.ToInt32(dr["Quantity"])
                                },

                                Pawns = new PawnsModel { 
                                    Id = Convert.ToInt32(dr["pawnid"]),
                                    Creation= Convert.ToDateTime(dr["creation"]),
                                    pawn_date = Convert.ToDateTime(dr["recovery"]),
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
                        cmd.Parameters.AddWithValue("@recovery", transactionsModel.Pawns.pawn_date);
                        cmd.Parameters.AddWithValue("@pawnUserId",transactionsModel.Users.Id);
                       
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
