using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test.Api.DDD.MongoDB.Application.Usuario.Commands;
using Test.Api.DDD.MongoDB.Application.Usuario.DTOs;
using Test.Api.DDD.MongoDB.Application.Usuario.Queries;

namespace Test.Api.DDD.MongoDB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsuariosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        var token = await _mediator.Send(new LoginCommand(login));
        if (token == null)
            return Unauthorized(new { message = "Email ou senha inválidos" });

        return Ok(new { token });
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var usuarios = await _mediator.Send(new GetAllUsuariosQuery());
        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(string id)
    {
        var usuario = await _mediator.Send(new GetUsuarioByIdQuery(id));
        if (usuario == null)
            return NotFound();

        return Ok(usuario);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] CreateUsuarioDto usuario)
    {
        var created = await _mediator.Send(new CreateUsuarioCommand(usuario));
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Update(string id, [FromBody] CreateUsuarioDto usuario)
    {
        var updated = await _mediator.Send(new UpdateUsuarioCommand(id, usuario));
        if (updated == null)
            return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _mediator.Send(new DeleteUsuarioCommand(id));
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
