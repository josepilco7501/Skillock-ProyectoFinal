using MediatR;
using Lab10.Application2.UseCases.CourseUseCase.Commands;
using Lab10.Application2.UseCases.CourseUseCase.Querys;
using Microsoft.AspNetCore.Mvc;

namespace Lab10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CoursesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // 1. Endpoint para Crear Curso (Command)
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCourseCommand command)
    {
        var courseId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = courseId }, new { Id = courseId, Message = "Curso creado con éxito 🚀" });
    }

    // 2. Endpoint para Obtener por ID (Query)
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute]GetCourseByIdQuery request)
    {
        var course = await _mediator.Send(request);
       
        return Ok(course);
    }

    // 3.NUEVO: Endpoint para Listar Cursos Activos (Query)
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var courses = await _mediator.Send(new GetAllActiveCoursesQuery());
        return Ok(courses);
    }
}