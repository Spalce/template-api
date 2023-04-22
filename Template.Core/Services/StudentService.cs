using Template.Core.Interfaces.Repositories;
using Template.Core.Interfaces.Services;
using Template.Core.Models;

namespace Template.Core.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repository;

    public StudentService(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Student> GetById(Guid? id)
    {
        try
        {
            return await _repository.GetById(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<Student>> GetAll()
    {
        try
        {
            return await _repository.GetAll();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Student> Create(Student entity)
    {
        try
        {
            return await _repository.Create(entity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> Update(Guid? id, Student entity)
    {
        try
        {
            return await _repository.Update(id, entity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> Delete(Guid? id)
    {
        try
        {
            return await _repository.Delete(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Student> IsExist(Student entity)
    {
        try
        {
            return await _repository.IsExist(entity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool BatchDelete(IEnumerable<Student> entity)
    {
        try
        {
            return _repository.BatchDelete(entity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool BatchUpdate(IEnumerable<Student> entity)
    {
        try
        {
            return _repository.BatchUpdate(entity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> BatchCreate(IEnumerable<Student> entity)
    {
        try
        {
            return await _repository.BatchCreate(entity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
