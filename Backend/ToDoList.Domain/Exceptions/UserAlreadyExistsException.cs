namespace ToDoList.Domain.Exceptions;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException(string? message = "User with provided credentials already exists.") : base(message)
    {
    }
}
