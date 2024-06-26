﻿using MediatR;
using Thunders.Assignments.Application.Models;

namespace Thunders.Assignments.Application.Abstractions.Mediator;

public abstract record UseCaseRequest<TResponse> : IRequest<Result<TResponse>>
    where TResponse : class
{
}
