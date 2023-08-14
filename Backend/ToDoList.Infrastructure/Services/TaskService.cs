using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ToDoList.Contracts.UserTask;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Exceptions;
using ToDoList.Infrastructure.Interfaces.Repositories;
using ToDoList.Infrastructure.Interfaces.Services;

namespace ToDoList.Infrastructure.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> AddAsync(int userId, UserTaskAddRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _repository.AddAsync(new UserTask()
            {
                UserId = userId,
                Title = request.Title!,
                IsCompleted = false,
                CreatedOn = DateTime.UtcNow,
            }, cancellationToken);
        }
        catch (DbUpdateException exception) when (((SqlException)exception.InnerException!).Number == 547) 
        {
            throw new UserDoesntExistException();
        }
        
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (!await _repository.TaskExists(id, cancellationToken))
        {
            throw new TaskDoesntExistException();
        }

        await _repository.DeleteAsync(id, cancellationToken);
    }

    public async Task<ICollection<UserTaskDto>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetByUserIdAsync(userId, cancellationToken);

        return result.Select(u => new UserTaskDto(
            u.Id,
            u.UserId,
            u.Title,
            u.IsCompleted,
            u.CreatedOn,
            u.FinishedOn))
            .OrderBy(u => u.IsComplete)
            .ToList();
    }

    public async Task UpdateAsync(int id, UserTaskUpdateRequest request, CancellationToken cancellationToken = default)
    {
        if(!await _repository.TaskExists(id, cancellationToken))
        {
            throw new TaskDoesntExistException();
        }

        await _repository.UpdateAsync(new UserTask()
        {
            Id = id,
            Title = request.Title!,
            IsCompleted = request.IsComplete ?? false
        }, cancellationToken);
    }
}
