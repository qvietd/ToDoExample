using MediatR;
using Todos.Domain.Interfaces;
using Todos.Domain.ValueObjects;

namespace Todos.Application.Features.Todos.Commands;

public record DeleteTodoCommand(Guid Id) : IRequest;

public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand>
{
    private readonly ITodoRepository _repository;

    public DeleteTodoCommandHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _repository.GetByIdAsync(TodoId.From(request.Id), cancellationToken)
            ?? throw new KeyNotFoundException($"Todo with id {request.Id} not found");

        await _repository.DeleteAsync(TodoId.From(request.Id), cancellationToken);
    }
}