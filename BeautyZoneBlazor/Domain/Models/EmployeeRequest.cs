namespace Domain.Models;

public class EmployeeRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public virtual List<Guid> Procedures { get; set; }
}