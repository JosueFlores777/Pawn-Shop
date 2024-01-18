using System.ComponentModel.DataAnnotations;

namespace pawnShop.Models
{
    public class WarehousesModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Location is required")]
        public string? Location { get; set; }
        [Required(ErrorMessage = "creation is required")]
        public DateTime? creation { get; set; }
        public ShelvesModel Shelves { get; set; }
    
        public DateTime? updatedDate { get; set; }

    }
}
