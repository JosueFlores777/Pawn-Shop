using System.ComponentModel.DataAnnotations;
namespace pawnShop.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "IDUser is required")]
        public string IDUser { get; set; }


        [Required(ErrorMessage = "name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string? Role { get; set; }

        [Required(ErrorMessage = "HirringDate is required")]
        public DateTime? HirringDate { get; set; }

        public DateTime? CreationDate { get; set; }
       
        public DateTime? LastUpdatedDate { get;  set; }
    }
}
