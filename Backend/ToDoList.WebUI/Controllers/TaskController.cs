using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Contracts.UserTask;
using ToDoList.Domain.DTOs;
using ToDoList.Infrastructure.Interfaces.Services;

namespace ToDoList.WebUI.Controllers;

[Authorize]
[ApiController]
[Route("api/users/{userId:int}/tasks")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly IValidator<UserTaskUpdateRequest> _updateValidator;
    private readonly IValidator<UserTaskAddRequest> _addValidator;

    public TaskController(
        ITaskService taskService,
        IValidator<UserTaskUpdateRequest> updateValidator,
        IValidator<UserTaskAddRequest> addValidator)
    {
        _taskService = taskService;
        _updateValidator = updateValidator;
        _addValidator = addValidator;
    }

    [HttpGet]
    public async Task<ICollection<UserTaskDto>> GetUserTasks(int userId, CancellationToken cancellationToken)
    {
        var result = await _taskService.GetByUserIdAsync(userId, cancellationToken);

        return result;
    }

    [HttpPost]
    public async Task<IActionResult> Add(int userId, UserTaskAddRequest request, CancellationToken cancellationToken)
    {
        _addValidator.ValidateAndThrow(request);

        var newId = await _taskService.AddAsync(userId, request, cancellationToken);

        return Ok(newId);
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update(int id, UserTaskUpdateRequest request, CancellationToken cancellationToken)
    {
        _updateValidator.ValidateAndThrow(request);

        await _taskService.UpdateAsync(id, request, cancellationToken);

        return Ok();
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _taskService.DeleteAsync(id, cancellationToken);

        return Ok();
    }
}
