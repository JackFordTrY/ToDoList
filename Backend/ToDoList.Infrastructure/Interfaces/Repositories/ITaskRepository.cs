using ToDoList.Domain.Entities;

namespace ToDoList.Infrastructure.Interfaces.Repositories;

public interface ITaskRepository
{
    Task<ICollection<UserTask>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);

    Task<int> AddAsync(UserTask task, CancellationToken cancellationToken = default);

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);

    Task UpdateAsync(UserTask task, CancellationToken cancellationToken = default);

    Task<bool> TaskExists(int id, CancellationToken cancellationToken = default);
}
