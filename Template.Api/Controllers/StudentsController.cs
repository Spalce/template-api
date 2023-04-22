using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Template.Core.Interfaces.Services;
using Template.Core.Models;
using Template.Core.ViewModels;

namespace Template.Api.Controllers;

[Produces(MediaTypeNames.Application.Json)]
[Route("api/[controller]")]
[ApiController]
public class StudentsController : Controller
{
    private readonly IStudentService _service;

    public StudentsController(IStudentService service)
    {
        _service = service;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Response<Student>>> GetById(Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest(new Response<Student>
            {
                Message = "Id is null",
                Errors = new List<string> { "Id is null" },
                Data = null,
                Success = false
            });
        }

        var record = await _service.GetById(id);
        if (record == null!)
        {
            return NotFound(new Response<Student>
            {
                Message = "Not record matches the id provided",
                Errors = new List<string> { "Not record matches the id provided" },
                Data = null,
                Success = false
            });
        }

        return Ok(new Response<Student>
        {
            Message = "Record found",
            Errors = null,
            Data = record,
            Success = true
        });
    }

    [HttpGet]
    public async Task<ActionResult<Response<IEnumerable<Student>>>> GetAll()
    {
        var records = await _service.GetAll();
        if (records == null!)
        {
            return NotFound(new Response<IEnumerable<Student>>
            {
                Message = "No records found",
                Errors = new List<string> { "No records found" },
                Data = null,
                Success = false
            });
        }

        return Ok(new Response<IEnumerable<Student>>
        {
            Message = "Records found",
            Errors = null,
            Data = records,
            Success = true
        });
    }

    [HttpPost]
    public async Task<ActionResult<Response<Student>>> Create(Student student)
    {
        if (student == null!)
        {
            return BadRequest(new Response<Student>
            {
                Message = "Student is null",
                Errors = new List<string> { "Student is null" },
                Data = null,
                Success = false
            });
        }

        var isExists = await _service.IsExist(student);
        if (isExists != null!)
        {
            return BadRequest(new Response<Student>
            {
                Message = "Student already exists",
                Errors = new List<string> { "Student already exists" },
                Data = null,
                Success = false
            });
        }

        var record = await _service.Create(student);
        if (record == null!)
        {
            return BadRequest(new Response<Student>
            {
                Message = "Student could not be created",
                Errors = new List<string> { "Student could not be created" },
                Data = null,
                Success = false
            });
        }

        return Ok(new Response<Student>
        {
            Message = "Student created",
            Errors = null,
            Data = record,
            Success = true
        });
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Response<bool>>> Update(Guid id, Student student)
    {
        if (id == Guid.Empty)
        {
            return BadRequest(new Response<bool>
            {
                Message = "Id is null",
                Errors = new List<string> { "Id is null" },
                Data = false,
                Success = false
            });
        }

        if (student == null!)
        {
            return BadRequest(new Response<bool>
            {
                Message = "Student is null",
                Errors = new List<string> { "Student is null" },
                Data = false,
                Success = false
            });
        }

        var isExists = await _service.GetById(id);
        if (isExists == null!)
        {
            return BadRequest(new Response<bool>
            {
                Message = "Student does not exist",
                Errors = new List<string> { "Student does not exist" },
                Data = false,
                Success = false
            });
        }

        var record = await _service.Update(id, student);
        if (!record)
        {
            return BadRequest(new Response<bool>
            {
                Message = "Student could not be updated",
                Errors = new List<string> { "Student could not be updated" },
                Data = false,
                Success = false
            });
        }

        return Ok(new Response<bool>
        {
            Message = "Student updated",
            Errors = null,
            Data = record,
            Success = true
        });
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Response<bool>>> Delete(Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest(new Response<bool>
            {
                Message = "Id is null",
                Errors = new List<string> { "Id is null" },
                Data = false,
                Success = false
            });
        }

        var isExists = await _service.GetById(id);
        if (isExists == null!)
        {
            return BadRequest(new Response<bool>
            {
                Message = "Student does not exist",
                Errors = new List<string> { "Student does not exist" },
                Data = false,
                Success = false
            });
        }

        var record = await _service.Delete(id);
        if (!record)
        {
            return BadRequest(new Response<bool>
            {
                Message = "Student could not be deleted",
                Errors = new List<string> { "Student could not be deleted" },
                Data = false,
                Success = false
            });
        }

        return Ok(new Response<bool>
        {
            Message = "Student deleted",
            Errors = null,
            Data = record,
            Success = true
        });
    }


}
