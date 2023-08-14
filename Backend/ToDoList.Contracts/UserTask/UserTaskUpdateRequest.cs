namespace ToDoList.Contracts.UserTask;

public record UserTaskUpdateRequest(
    string? Title,
    bool? IsComplete);
