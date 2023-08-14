using ToDoList.Contracts.User;
using ToDoList.Domain.DTOs;

namespace ToDoList.Infrastructure.Interfaces.Services;

public interface IUserService
{
    Task AddAsync(UserAuthRequest request, CancellationToken cancellationToken = default);

    Task<UserDto> GetByCredentialsAsync(UserAuthRequest request, CancellationToken cancellationToken = default);
}
