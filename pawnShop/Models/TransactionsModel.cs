namespace pawnShop.Models
{
    public class TransactionsModel
    {
        public int Id { get; set; }
        public ClientModel Users { get; set; }
        public TransactionTypeModel TransactionType { get; set; }
        public ShelvesModel Shelves { get; set; }
        public double Amount { get; set; }
        public DateTime Creation {  get; set; }
        public DateTime Update {  get; set; }

    }
}
