using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Thunders.Assignments.Application.Enums;
using Thunders.Assignments.Application.Features.Assignment.Add;
using Thunders.Assignments.Application.Features.Assignment.Delete;
using Thunders.Assignments.Application.Features.Assignment.Get;
using Thunders.Assignments.Application.Features.Assignment.List;
using Thunders.Assignments.Application.Features.Assignment.Update;
using Thunders.Assignments.Application.Models;

namespace Thunders.Assignments.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class AssignmentController : ControllerBase
{
    private ISender _sender;

    public AssignmentController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    ///     Recupera uma tarefa pelo seu identificador único.
    /// </summary>
    /// <param name="id">Identificador único da tarefa à recuperar.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Tarefa.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GetAssignmentResponse))]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAssignmentRequest(id), cancellationToken);
        return ToActionResult(result);
    }

    /// <summary>
    ///     Recuperar uma listagem de tarefas.
    /// </summary>
    /// <param name="request">Parametros para filtragem das tarefas.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Listagem de tarefas.</returns>
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<ListAssignmentResponse>))]
    public async Task<IActionResult> Get([FromQuery] ListAssignmentRequest request, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);
        return ToActionResult(result);
    }

    /// <summary>
    ///     Cria uma nova tarefa.
    /// </summary>
    /// <param name="request">Dados da tarefa à ser criada.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Identificador único da tarefa criada.</returns>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(AddAssignmentResponse))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Post([FromBody] AddAssignmentRequest request, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);
        return ToActionResult(result);
    }

    /// <summary>
    ///     Atualiza uma tarefa existente.
    /// </summary>
    /// <param name="request">Dados da tarefa à ser atualizada.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Identificador único da tarefa atualizada.</returns>
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UpdateAssignmentResponse))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Put([FromBody] UpdateAssignmentRequest request, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);
        return ToActionResult(result);
    }

    /// <summary>
    ///     Excluí uma tarefa existente.
    /// </summary>
    /// <param name="request">Identificador único da tarefa à ser excluída.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Mensagem de confirmação</returns>
    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete([FromBody] DeleteAssignmentRequest request, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request, cancellationToken);
        return ToActionResult(result);
    }

    private IActionResult ToActionResult<TValue>(Result<TValue> result, string redirectUri = "")
        where TValue: class
    {
        if (result.Type == ResultType.Created)
        {
            return Created(redirectUri, result.Value);
        }

        if (result.Type == ResultType.Ok)
        {
            return Ok(result.Value);
        }

        if (result.Type == ResultType.NotFound)
        {
            return NotFound(result.Message);
        }

        if (result.Type == ResultType.Conflict)
        {
            return Conflict(result.Message);
        }

        if (result.Type == ResultType.BadRequest)
        {
            return BadRequest(result.Message);
        }

        return StatusCode(500);
    }

    private IActionResult ToActionResult(Result result, string redirectUri = "")
    {
        if (result.Type == ResultType.Created)
        {
            return Created(redirectUri, result.Message);
        }

        if (result.Type == ResultType.Ok)
        {
            return Ok(result.Message);
        }

        if (result.Type == ResultType.NotFound)
        {
            return NotFound(result.Message);
        }

        if (result.Type == ResultType.Conflict)
        {
            return Conflict(result.Message);
        }

        if (result.Type == ResultType.BadRequest)
        {
            return BadRequest(result.Message);
        }

        return StatusCode(500);
    }
}
