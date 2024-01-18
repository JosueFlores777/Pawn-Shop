namespace pawnShop.Models
{
    public class ShelvesModel
    {
        public int Id { get; set; }
        public WarehousesModel? Warehouses { get; set; }

        public string Name { get; set; }
        public int? Capacity { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set;}

    }
}
