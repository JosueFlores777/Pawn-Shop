namespace pawnShop.Models
{
    public class ItemsModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Quatity { get; set; }
        public string Description { get; set; }
        public string EstimatedValue { get; set; }

        public ShelvesModel Shelves { get; set; }
    }
}
