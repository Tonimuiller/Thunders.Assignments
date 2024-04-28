using MediatR;
using Thunders.Assignments.Application.Models;

namespace Thunders.Assignments.Application.Abstractions.Mediator;

internal abstract class UseCaseHandler<TUseCaseRequest, TUseCaseResponse>
    : IRequestHandler<TUseCaseRequest, Result<TUseCaseResponse>>
    where TUseCaseRequest : IRequest<Result<TUseCaseResponse>>
    where TUseCaseResponse : class
{
    public abstract Task<Result<TUseCaseResponse>> Handle(TUseCaseRequest request, CancellationToken cancellationToken);
}
