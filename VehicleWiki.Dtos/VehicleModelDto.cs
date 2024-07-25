namespace VehicleWiki.Dtos;

public class VehicleModelDto
{
    public Guid Id { get; set; }
    public Guid MakeId { get; set; }
    public required string Name { get; set; }
    public required string Abrv { get; set; }
}
