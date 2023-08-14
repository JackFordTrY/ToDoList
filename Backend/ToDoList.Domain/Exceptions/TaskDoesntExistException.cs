namespace ToDoList.Domain.Exceptions;

public class TaskDoesntExistException : Exception
{
    public TaskDoesntExistException(string? message = "Task doesn't exist.") : base(message)
    {
    }
}
