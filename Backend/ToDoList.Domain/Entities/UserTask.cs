using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.Entities;

public class UserTask
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    public User User { get; set; } = default!;

    [Required]
    [StringLength(100, MinimumLength = 5)]
    public string Title { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }

    public DateTime CreatedOn { get; set; }
    
    public DateTime? FinishedOn { get; set; }    
}
