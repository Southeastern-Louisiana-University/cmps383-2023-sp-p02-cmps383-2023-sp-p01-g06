using SP23.P02.Web.Features.Users;
using System.ComponentModel.DataAnnotations;

namespace SP23.P02.Web.Features.TrainStations;

public class TrainStation
{
    public int Id { get; set; }

    [Required]
    [MaxLength(120)]
    public string Name { get; set; }

    [Required]
    public string Address { get; set; }

    public User? Manager { get; set; }
}