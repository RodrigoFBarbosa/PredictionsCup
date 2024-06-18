using System.ComponentModel.DataAnnotations;

namespace PredictionsCup.Models;

public class SemiFinalist
{
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }
    public User User { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }
}
