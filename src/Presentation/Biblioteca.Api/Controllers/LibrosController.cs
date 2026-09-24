using Biblioteca.Application.Dtos;
using Biblioteca.Application.Features.Libros.Queries.GetAllLibros;
using Biblioteca.Application.Features.Libros.Queries.GetLibroById;
using Biblioteca.Application.Features.Libros.Queries.GetLibrosByCategoria;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class LibrosController : ControllerBase
{
    private readonly IMediator _mediator;

    public LibrosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Consulta 1: Obtener todos los libros del catálogo (incluye Id, Título, ISBN, Año, Autor y Categoría).
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Colección de libros disponibles.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<LibroDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<LibroDto>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllLibrosQuery(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Consulta 2: Obtener un libro por su identificador único con información detallada de Autor y Categoría.
    /// </summary>
    /// <param name="id">Identificador único del libro.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Detalle completo del libro o 404 si no existe.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(LibroDetalleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LibroDetalleDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetLibroByIdQuery(id), cancellationToken);

        if (result is null)
        {
            return NotFound(new { mensaje = $"No se encontró ningún libro con el identificador {id}." });
        }

        return Ok(result);
    }

    /// <summary>
    /// Consulta 3: Obtener libros filtrados por el identificador de Categoría.
    /// </summary>
    /// <param name="categoriaId">Identificador único de la categoría.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Colección de libros pertenecientes a la categoría especificada.</returns>
    [HttpGet("categoria/{categoriaId:int}")]
    [ProducesResponseType(typeof(IReadOnlyList<LibroDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<LibroDto>>> GetByCategoria(int categoriaId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetLibrosByCategoriaQuery(categoriaId), cancellationToken);
        return Ok(result);
    }
}
