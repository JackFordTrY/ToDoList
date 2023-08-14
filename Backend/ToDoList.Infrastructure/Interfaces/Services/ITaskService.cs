using ToDoList.Contracts.UserTask;
using ToDoList.Domain.DTOs;

namespace ToDoList.Infrastructure.Interfaces.Services;

public interface ITaskService
{
    Task<ICollection<UserTaskDto>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);

    Task<int> AddAsync(int userId, UserTaskAddRequest request, CancellationToken cancellationToken = default);

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);

    Task UpdateAsync(int id, UserTaskUpdateRequest requset,CancellationToken cancellationToken = default);
}
