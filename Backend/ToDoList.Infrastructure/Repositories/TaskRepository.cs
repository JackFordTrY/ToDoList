using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;
using ToDoList.Infrastructure.Data;
using ToDoList.Infrastructure.Interfaces.Repositories;

namespace ToDoList.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _context;

    public TaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(UserTask task, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(task, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return task.Id;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        await _context.Tasks.Where(t => t.Id == id).ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<ICollection<UserTask>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await _context.Tasks.Where(t => t.UserId == userId).ToListAsync(cancellationToken);
    }

    public async Task<bool> TaskExists(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Tasks.AnyAsync(t => t.Id == id, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(UserTask task, CancellationToken cancellationToken = default)
    {
        var selectedTask = _context.Tasks.Where(t => t.Id == task.Id);

        if (task.Title is not null)
        {
            await selectedTask.ExecuteUpdateAsync(upd => upd.SetProperty(t => t.Title, task.Title), cancellationToken);
        }

        if (task.IsCompleted)
        {
            await selectedTask.ExecuteUpdateAsync(upd => 
                upd.SetProperty(t => t.IsCompleted, task.IsCompleted)
                   .SetProperty(t => t.FinishedOn, DateTime.UtcNow),
                   cancellationToken);
        }
    }
}
