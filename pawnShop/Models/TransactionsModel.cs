namespace pawnShop.Models
{
    public class TransactionsModel
    {
        public int Id { get; set; }
        public ClientModel Users { get; set; }
        public TransactionTypeModel TransactionType { get; set; }
        public ShelvesModel Shelves { get; set; }

        public List<TransactionTypeModel> TransactionTypes { get; set; } = new List<TransactionTypeModel>();
        public List<ShelvesModel> ShelvesList { get; set; } = new List<ShelvesModel>();
        public int SelectedShelvesId { get; set; }
        public int SelectedTransactionTypeId { get; set; }
        public ItemsModel items{ get; set; }
        public double Amount { get; set; }
        
        public PawnsModel Pawns { get; set; }
        public DateTime transactionDate { get; set; }
        public DateTime Repurchase { get; set; }
        public DateTime Creation {  get; set; }
        public DateTime Update {  get; set; }

    }
}
