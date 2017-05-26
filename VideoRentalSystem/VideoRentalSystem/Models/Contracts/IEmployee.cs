namespace VideoRentalSystem.Models.Contracts
{
    public interface IEmployee
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        int Salary { get; set; }
        int ManagerId { get; set; }
    }
}