using System.ComponentModel.DataAnnotations;

namespace VehicleWiki.Model;

public class VehicleMake
{
    public Guid Id { get; set; }

    [MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(10)]
    public required string Abrv { get; set; }

    public required ICollection<VehicleModel> Models { get; set; }
}
