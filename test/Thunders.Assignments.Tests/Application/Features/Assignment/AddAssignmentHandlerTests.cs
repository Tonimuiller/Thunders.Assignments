using FluentAssertions;
using NSubstitute;
using Thunders.Assignments.Application.Enums;
using Thunders.Assignments.Application.Features.Assignment;
using Thunders.Assignments.Application.Features.Assignment.Add;
using Thunders.Assignments.Application.Models;
using Thunders.Assignments.Domain.Repositories;
using Thunders.Assignments.Domain.Repositories.Assignment;

namespace Thunders.Assignments.Tests.Application.Features.Assignment;

public sealed class AddAssignmentHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAssignmentRepository _assignmentRepository;

    public AddAssignmentHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _assignmentRepository = Substitute.For<IAssignmentRepository>();
    }

    [Fact]
    public async Task NullRequestParameter_ThrowsArgumentNullException()
    {
        //Arrange
        var handler = BuildHandler();

        //Act
        Func<Task<Result<AddAssignmentResponse>>> func = () => handler.Handle(null!, CancellationToken.None);
        
        //Assert
        await func
            .Should()
            .ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task EmptyTitle_BadRequestFail()
    {
        //Arrange
        var request = new AddAssignmentRequest
        {
            Description = "Assignment",
            Done = false,
            Schedule = DateTime.Now.AddDays(5)
        };
        var handler = BuildHandler();

        //Act
        var result = await handler.Handle(request, CancellationToken.None);

        //Assert
        _assignmentRepository
            .DidNotReceive()
            .Add(Arg.Any<Domain.Entities.Assignment>());
        await _unitOfWork.DidNotReceive().CommitAsync(CancellationToken.None);

        result
            .Should()
            .NotBeNull();
        result
            .IsSuccess
            .Should()
            .BeFalse();
        result
            .Type
            .Should()
            .Be(ResultType.BadRequest);
        result
            .Message
            .Should()
            .Be(AssignmentResources.TitleIsRequired);
    }

    [Fact]
    public async Task EmptySchedule_BadRequestFail()
    {
        //Arrange
        var request = new AddAssignmentRequest
        {
            Title = "Some title",
            Description = "Assignment",
            Done = false
        };
        var handler = BuildHandler();

        //Act
        var result = await handler.Handle(request, CancellationToken.None);

        //Assert
        _assignmentRepository
            .DidNotReceive()
            .Add(Arg.Any<Domain.Entities.Assignment>());
        await _unitOfWork.DidNotReceive().CommitAsync(CancellationToken.None);

        result
            .Should()
            .NotBeNull();
        result
            .IsSuccess
            .Should()
            .BeFalse();
        result
            .Type
            .Should()
            .Be(ResultType.BadRequest);
        result
            .Message
            .Should()
            .Be(AssignmentResources.ScheduleIsRequired);
    }

    [Fact] async Task CompleteRequest_ReturnsCreatedResult()
    {
        //Arrange
        var request = new AddAssignmentRequest
        {
            Title = "Some title",
            Description = "Assignment",
            Done = false,
            Schedule = DateTime.Now.AddDays(2)
        };
        var handler = BuildHandler();

        //Act
        var result = await handler.Handle(request, CancellationToken.None);

        //Assert
        _assignmentRepository
            .Received()
            .Add(Arg.Is<Domain.Entities.Assignment>(a => 
                a.Title == request.Title
                && a.Description == request.Description
                && a.Done == request.Done
                && a.Schedule == request.Schedule
                && a.CreatedAt != default
                && a.UpdatedAt == null
                && a.Id != Guid.Empty));
        await _unitOfWork.Received().CommitAsync(CancellationToken.None);
        
        result
            .Should()
            .NotBeNull();
        result
            .IsSuccess
            .Should()
            .BeTrue();
        result
            .Type
            .Should()
            .Be(ResultType.Created);
    }

    private AddAssignmentHandler BuildHandler() => 
        new AddAssignmentHandler(
            _assignmentRepository,
            _unitOfWork);
}