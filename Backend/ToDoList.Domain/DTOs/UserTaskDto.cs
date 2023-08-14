namespace ToDoList.Domain.DTOs;

public record UserTaskDto(
    int? Id,
    int? UserId,
    string? Title,
    bool? IsComplete,
    DateTime? CreatedOn,
    DateTime? FinishedOn);
