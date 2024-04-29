using FluentAssertions;
using NSubstitute;
using Thunders.Assignments.Application.Enums;
using Thunders.Assignments.Application.Features.Assignment;
using Thunders.Assignments.Application.Features.Assignment.Get;
using Thunders.Assignments.Application.Features.Assignment.List;
using Thunders.Assignments.Application.Models;
using Thunders.Assignments.Domain.Repositories.Assignment;

namespace Thunders.Assignments.Tests.Application.Features.Assignment;

public sealed class ListAssignmentHandlerTests
{
    private readonly IAssignmentRepository _assignmentRepository;

    public ListAssignmentHandlerTests()
    {
        _assignmentRepository = Substitute.For<IAssignmentRepository>();
    }

    [Fact]
    public async Task NullRequestParameter_ThrowsArgumentNullException()
    {
        //Arrange
        var handler = BuildHandler();

        //Act
        Func<Task<Result<IReadOnlyList<ListAssignmentResponse>>>> func = () => handler.Handle(null!, CancellationToken.None);

        //Assert
        await func
            .Should()
            .ThrowAsync<ArgumentNullException>();
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    public async Task MissingBegginingOrEndScheduleDate_BadRequestFail(
        bool setBeginningDateValue,
        bool setEndDateValue)
    {
        //Arrange
        var request = new ListAssignmentRequest
        {
            ScheduleBeginning = setBeginningDateValue ? DateOnly.FromDateTime(DateTime.Now) : null,
            ScheduleEnd = setEndDateValue ? DateOnly.FromDateTime(DateTime.Now) : null,
        };
        var handler = BuildHandler();

        //Act
        var result = await handler.Handle(request, CancellationToken.None);

        //Assert
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
            .Be(AssignmentResources.CompleteScheduleDateRangeIsRequired);
    }

    [Fact]
    public async Task InvalidScheduleRange_BadRequestFail()
    {
        //Arrange
        var request = new ListAssignmentRequest
        {
            ScheduleBeginning = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
            ScheduleEnd = DateOnly.FromDateTime(DateTime.Now),
        };
        var handler = BuildHandler();

        //Act
        var result = await handler.Handle(request, CancellationToken.None);

        //Assert
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
            .Be(AssignmentResources.InvalidDateRange);
    }

    [Fact]
    public async Task ValidScheduleRange_ReturnsOk()
    {
        //Arrange
        var request = new ListAssignmentRequest
        {
            Description = "desc",
            Done = true,
            Title = "title",
            ScheduleBeginning = DateOnly.FromDateTime(DateTime.Now),
            ScheduleEnd = DateOnly.FromDateTime(DateTime.Now),
        };

        var assignments = new List<Domain.Entities.Assignment>
        {
            new Domain.Entities.Assignment
            {
                CreatedAt = DateTime.Now.AddDays(-1),
                Description = "Description",
                Done = true,
                Id = Guid.NewGuid(),
                Schedule = DateTime.Now,
                Title = "Title",
                UpdatedAt = DateTime.Now
            }
        };
        _assignmentRepository
            .GetAsync(Arg.Any<GetAssignmentsOptions>(), CancellationToken.None)
            .Returns(assignments);

        var handler = BuildHandler();

        //Act
        var result = await handler.Handle(request, CancellationToken.None);

        //Assert
        await _assignmentRepository
            .Received()
            .GetAsync(Arg.Is<GetAssignmentsOptions>(o =>
                o.Done == request.Done
                && o.Title == request.Title
                && o.Description == request.Description
                && o.ScheduleBeginning == request.ScheduleBeginning
                && o.ScheduleEnd == request.ScheduleEnd), CancellationToken.None);

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
            .Be(ResultType.Ok);

        result
            .Value
            .Should()
            .Match<IReadOnlyList<ListAssignmentResponse>>(g =>
                g.First().Id == assignments.First().Id
                && g.First().Schedule == assignments.First().Schedule
                && g.First().CreatedAt == assignments.First().CreatedAt
                && g.First().Description == assignments.First().Description
                && g.First().Done == assignments.First().Done
                && g.First().Title == assignments.First().Title);
    }

    [Fact]
    public async Task NoFilters_ReturnsOk()
    {
        //Arrange
        var request = new ListAssignmentRequest();
        var handler = BuildHandler();
        var assignments = new List<Domain.Entities.Assignment>
        {
            new Domain.Entities.Assignment
            {
                CreatedAt = DateTime.Now.AddDays(-1),
                Description = "Description",
                Done = true,
                Id = Guid.NewGuid(),
                Schedule = DateTime.Now,
                Title = "Title",
                UpdatedAt = DateTime.Now
            }
        };
        _assignmentRepository
            .GetAsync(Arg.Any<GetAssignmentsOptions>(), CancellationToken.None)
            .Returns(assignments);

        //Act
        var result = await handler.Handle(request, CancellationToken.None);

        //Assert
        await _assignmentRepository
            .Received()
            .GetAsync(Arg.Is<GetAssignmentsOptions>(o =>
                o.Done == request.Done
                && o.Title == request.Title
                && o.Description == request.Description
                && o.ScheduleBeginning == request.ScheduleBeginning
                && o.ScheduleEnd == request.ScheduleEnd), CancellationToken.None);

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
            .Be(ResultType.Ok);

        result
            .Value
            .Should()
            .Match<IReadOnlyList<ListAssignmentResponse>>(g =>
                g.First().Id == assignments.First().Id
                && g.First().Schedule == assignments.First().Schedule
                && g.First().CreatedAt == assignments.First().CreatedAt
                && g.First().UpdatedAt == assignments.First().UpdatedAt
                && g.First().Description == assignments.First().Description
                && g.First().Done == assignments.First().Done
                && g.First().Title == assignments.First().Title);
    }

    private ListAssignmentHandler BuildHandler() => new ListAssignmentHandler(_assignmentRepository);
}
