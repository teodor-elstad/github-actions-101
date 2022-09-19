namespace Notes.Api.Models;

using System.ComponentModel.DataAnnotations;

public class UpdateNote
{
    [Required]
    [MinLength(1)]
    [MaxLength(500)]
    public string Content { get; set; }
}