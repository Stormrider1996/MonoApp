using System.ComponentModel.DataAnnotations;

namespace VehicleWiki.Model;

public class VehicleModel
{
    public Guid Id { get; set; }

    [MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(10)]
    public required string Abrv { get; set; }

    public string? Info { get; set; }

    public required VehicleMake Make { get; set; }

    public Guid MakeId { get; set; }
}
