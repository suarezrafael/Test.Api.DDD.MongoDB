using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test.Api.DDD.MongoDB.Application.Cardapio.Commands;
using Test.Api.DDD.MongoDB.Application.Cardapio.DTOs;
using Test.Api.DDD.MongoDB.Application.Cardapio.Queries;

namespace Test.Api.DDD.MongoDB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CardapiosController : ControllerBase
{
    private readonly IMediator _mediator;

    public CardapiosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cardapios = await _mediator.Send(new GetAllCardapiosQuery());
        return Ok(cardapios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var cardapio = await _mediator.Send(new GetCardapioByIdQuery(id));
        if (cardapio == null)
            return NotFound();

        return Ok(cardapio);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCardapioDto cardapio)
    {
        var created = await _mediator.Send(new CreateCardapioCommand(cardapio));
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] CreateCardapioDto cardapio)
    {
        var updated = await _mediator.Send(new UpdateCardapioCommand(id, cardapio));
        if (updated == null)
            return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _mediator.Send(new DeleteCardapioCommand(id));
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
