using delphibackend.Delphi.Domain.Model.Commands;
using delphibackend.Delphi.Domain.Model.Queries;
using delphibackend.Delphi.Domain.Services;
using delphibackend.Delphi.Interfaces.Resources;
using delphibackend.Delphi.Interfaces.Transform;
using Microsoft.AspNetCore.Mvc;

namespace delphibackend.Delphi.Interfaces.Controllers;


[ApiController]
[Route("api/[controller]")]
public class QuestionController : ControllerBase
{
    private readonly IQuestionCommandService _commandService;
    private readonly IQuestionQueryService _queryService;

    public QuestionController(IQuestionCommandService commandService, IQuestionQueryService queryService)
    {
        _commandService = commandService;
        _queryService = queryService;
    }

    // Endpoint para crear una pregunta
    [HttpPost]
    public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionResource resource)
    {
        if (resource == null)
        {
            return BadRequest("Invalid data.");
        }

        var command = CreateQuestionResourceAssembler.ToCommand(resource);
        var questionId = await _commandService.Handle(command);

        return CreatedAtAction(nameof(GetQuestionById), new { id = questionId }, null);
    }

    // Endpoint para dar "like" a una pregunta
    [HttpPost("{id}/like")]
    public async Task<IActionResult> LikeQuestion(Guid id)
    {
        await _commandService.Handle(new LikeQuestionCommand(id));
        return NoContent();
    }

    // Endpoint para responder una pregunta
    [HttpPost("{id}/answer")]
    public async Task<IActionResult> AnswerQuestion(Guid id, [FromBody] string answer)
    {
        if (string.IsNullOrWhiteSpace(answer))
        {
            return BadRequest("Answer cannot be empty.");
        }

        await _commandService.Handle(new AnswerQuestionCommand(id, answer));
        return NoContent();
    }

    // Endpoint para obtener preguntas por RoomId
    [HttpGet("room/{roomId}")]
    public async Task<IActionResult> GetQuestionsByRoomId(Guid roomId)
    {
        var questions = await _queryService.Handle(new GetQuestionByRoomIdQuery(roomId));
        return Ok(questions);
    }

    // Endpoint para obtener una pregunta por su ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuestionById(Guid id)
    {
        var question = await _queryService.Handle(new GetQuestionByIdQuery(id));
        if (question == null)
        {
            return NotFound();
        }

        return Ok(question);
    }
}