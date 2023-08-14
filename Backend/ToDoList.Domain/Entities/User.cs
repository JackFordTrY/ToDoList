using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.Entities;

public class User
{
    [Key]
    public int Id { get; set; }

    [StringLength(20, MinimumLength = 5)]
    public string Username { get; set; } = string.Empty;

    [MaxLength(60)]
    public string Password { get; set; } = string.Empty;

    public List<UserTask> Tasks { get; set; } = default!;
}
