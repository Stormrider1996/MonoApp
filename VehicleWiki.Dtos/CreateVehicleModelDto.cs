namespace VehicleWiki.Dtos;

public class CreateVehicleModelDto
{
    public required string Name { get; set; }
    public required string Abrv { get; set; }
    public string? Info { get; set; }
    public required Guid MakeId { get; set; }
}
