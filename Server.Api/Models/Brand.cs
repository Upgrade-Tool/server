namespace Server.Api.Models;

public class Brand
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<Car>  Cars { get; set; } = new List<Car>();
}