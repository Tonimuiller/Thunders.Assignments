using MediatR;
using Thunders.Assignments.Application.Models;

namespace Thunders.Assignments.Application.Abstractions.Mediator;

internal abstract class UseCaseHandler<TUseCaseRequest>
    : IRequestHandler<TUseCaseRequest, Result>
    where TUseCaseRequest : IRequest<Result>
{
    public abstract Task<Result> Handle(TUseCaseRequest request, CancellationToken cancellationToken);
}
