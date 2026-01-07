namespace Domain.Models;

public class BeautyTechRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public virtual List<Guid> Procedures { get; set; }
}