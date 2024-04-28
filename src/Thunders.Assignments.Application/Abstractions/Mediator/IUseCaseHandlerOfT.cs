using MediatR;
using Thunders.Assignments.Application.Models;

namespace Thunders.Assignments.Application.Abstractions.Mediator;

internal interface IUseCaseHandler<TUseCaseRequest, TUseCaseResponse>
    : IRequestHandler<TUseCaseRequest, Result<TUseCaseResponse>>
    where TUseCaseRequest : IRequest<Result<TUseCaseResponse>>
    where TUseCaseResponse : class
{
}
