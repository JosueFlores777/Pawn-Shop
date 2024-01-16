namespace pawnShop.Models
{
    public class PawnsModel
    {
        public int Id { get; set; }
        public UsersModel Users { get; set; }
        public ItemsModel Items { get; set; }
        public ShelvesModel Shelves { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime Creation {  get; set; }
        public DateTime Update {  get; set; }
    }
}
