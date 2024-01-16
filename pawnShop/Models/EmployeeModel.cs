namespace pawnShop.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }

        public UsersModel User { get; set; } 

        public string? Role { get; set; }

        public double ? Salary { get; set; }

        public DateTime? HirringDate { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdatedDate { get;  set; }
    }
}
