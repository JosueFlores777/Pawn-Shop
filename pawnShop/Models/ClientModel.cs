using System.ComponentModel.DataAnnotations;

namespace pawnShop.Models
{
    public class ClientModel
    {   
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "IDClient is required")]
        public string IDClient { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^\+1 \d{2}-\d{4}-\d{3}$", ErrorMessage = "Invalid phone format. It should be for example +1 26-4867-856")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string? Role { get; set; }

        [Required(ErrorMessage = "CreationDate is required")]
        public DateTime? CreationDate { get; set; }

        [Required(ErrorMessage = "UpdateDate is required")]
        public DateTime? UpdateDate { get; set; }

        public int UpdatedByEmployeeId { get; set; }

        public string EmployedCreate { get; set; }
        public int CreateEmployedId { get; set; }
        public int LoggedInUserId { get; set; }
    }
}
