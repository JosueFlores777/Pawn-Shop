namespace pawnShop.Models
{
    public class WarehousesModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public DateTime? creation { get; set; }
        public DateTime? updatedDate { get; set; }
    }
}
