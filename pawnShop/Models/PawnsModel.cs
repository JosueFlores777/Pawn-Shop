namespace pawnShop.Models
{
    public class PawnsModel
    {
        public int Id { get; set; }
        public ClientModel Users { get; set; }
        public ItemsModel Items { get; set; }
        public ShelvesModel Shelves { get; set; }

        public DateTime pawn_date { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime Creation {  get; set; }
        public DateTime Update {  get; set; }
    }
}
