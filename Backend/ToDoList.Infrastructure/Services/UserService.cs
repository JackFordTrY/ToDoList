using ToDoList.Contracts.User;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Exceptions;
using ToDoList.Infrastructure.Interfaces.Repositories;
using ToDoList.Infrastructure.Interfaces.Services;
using Encryptor = BCrypt.Net.BCrypt;


namespace ToDoList.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task AddAsync(UserAuthRequest request, CancellationToken cancellationToken = default)
    {
        if (await _repository.GetByUsernameAsync(request.Username, cancellationToken) is not null)
        {
            throw new UserAlreadyExistsException();
        }

        await _repository.AddAsync(new User() 
        { 
            Username = request.Username, 
            Password = Encryptor.HashPassword(request.Password)
        }, cancellationToken);
    }

    public async Task<UserDto> GetByCredentialsAsync(UserAuthRequest request, CancellationToken cancellationToken = default)
    {
        if (await _repository.GetByUsernameAsync(request.Username, cancellationToken) is not User resultUser)
        {
            throw new UserDoesntExistException();
        }

        if (!Encryptor.Verify(request.Password, resultUser.Password))
        {
            throw new UserDoesntExistException();
        }

        return new UserDto(resultUser.Id, resultUser.Username);
    }
}
