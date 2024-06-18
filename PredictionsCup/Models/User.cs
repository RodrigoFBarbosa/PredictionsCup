using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PredictionsCup.Models;

public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Username is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }

    public int TotalPoints { get; set; }

    public List<Prediction> Predictions { get; set; }

    public List<Finalist> Finalists { get; set; }

    public List<SemiFinalist> Semifinalists { get; set; }
    public TopScorer TopScorer { get; set; }
    public BestPlayer BestPlayer { get; set; }
}