namespace VehicleWiki.Dtos;

public class VehicleMakeDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Abrv { get; set; }
}
