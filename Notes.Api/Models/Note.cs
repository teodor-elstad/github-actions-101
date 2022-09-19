namespace Notes.Api.Models;

using System.ComponentModel.DataAnnotations;

public class Note
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(500)]
    public string Content { get; set; }
}