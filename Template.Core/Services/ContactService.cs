using Template.Core.Interfaces.Repositories;
using Template.Core.Interfaces.Services;
using Template.Core.Models;

namespace Template.Core.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository _repository;

    public ContactService(IContactRepository repository)
    {
        _repository = repository;
    }

    public async Task<Contact> GetById(Guid? id)
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

    public async Task<IEnumerable<Contact>> GetAll()
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

    public async Task<Contact> Create(Contact entity)
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

    public async Task<bool> Update(Guid? id, Contact entity)
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

    public async Task<Contact> IsExist(Contact entity)
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

    public bool BatchDelete(IEnumerable<Contact> entity)
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

    public bool BatchUpdate(IEnumerable<Contact> entity)
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

    public async Task<bool> BatchCreate(IEnumerable<Contact> entity)
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

    public async Task<IEnumerable<Contact>> GetByStudentId(Guid? studentId)
    {
        try
        {
            return await _repository.GetByStudentId(studentId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
