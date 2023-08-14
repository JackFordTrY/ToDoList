namespace ToDoList.Domain.Exceptions;

public class UserDoesntExistException : Exception
{
    public UserDoesntExistException(string? message = "User doesn't exist.") : base(message)
    {
    }
}
