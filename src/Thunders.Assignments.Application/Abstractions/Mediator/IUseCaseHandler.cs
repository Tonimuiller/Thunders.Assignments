using MediatR;
using Thunders.Assignments.Application.Models;

namespace Thunders.Assignments.Application.Abstractions.Mediator;

internal interface IUseCaseHandler<TUseCaseRequest>
    : IRequestHandler<TUseCaseRequest, Result>
    where TUseCaseRequest : IRequest<Result>
{
}
