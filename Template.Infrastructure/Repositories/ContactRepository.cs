using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Template.Core.Interfaces.Repositories;
using Template.Core.Models;
using Template.Infrastructure.Data;

namespace Template.Infrastructure.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public ContactRepository(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Contact> GetById(Guid? id)
    {
        var record = await _db.Contacts.FindAsync(id);
        return record != null! ? _mapper.Map<Contact>(record) : null!;
    }

    public async Task<IEnumerable<Contact>> GetAll()
    {
        var record = await _db.Contacts.ToListAsync();
        return record != null! ? _mapper.Map<IEnumerable<Contact>>(record) : null!;
    }

    public async Task<Contact> Create(Contact entity)
    {
        var record = _mapper.Map<Infrastructure.Models.Contact>(entity);
        record.DateCreated = DateTime.UtcNow;
        var created = await _db.Contacts.AddAsync(record);
        var result = await _db.SaveChangesAsync();
        return result > 0 ? _mapper.Map<Contact>(created.Entity) : null!;
    }

    public async Task<bool> Update(Guid? id, Contact entity)
    {
        var record = await _db.Contacts.FindAsync(id);
        if (record == null)
        {
            return false;
        }

        record.DateUpdated = DateTime.UtcNow;
        record.StudentId = entity.StudentId;
        record.Content = entity.Content;
        record.Name = entity.Name;
        record.IsActive = entity.IsActive;

        _db.Contacts.Update(record);
        var result = await _db.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> Delete(Guid? id)
    {
        var record = await _db.Contacts.FindAsync(id);
        if (record == null)
        {
            return false;
        }

        _db.Contacts.Remove(record);
        var result = await _db.SaveChangesAsync();
        return result > 0;
    }

    public async Task<Contact> IsExist(Contact entity)
    {
        var record = await _db.Contacts.FirstOrDefaultAsync(x => x.StudentId == entity.StudentId && x.Content == entity.Content);
        return record != null! ? _mapper.Map<Contact>(record) : null!;
    }

    public bool BatchDelete(IEnumerable<Contact> entity)
    {
        var records = _mapper.Map<IEnumerable<Infrastructure.Models.Contact>>(entity);
        _db.Contacts.RemoveRange(records);
        return true;
    }

    public bool BatchUpdate(IEnumerable<Contact> entity)
    {
        var records = _mapper.Map<IEnumerable<Infrastructure.Models.Contact>>(entity);
        _db.Contacts.UpdateRange(records);
        return true;
    }

    public async Task<bool> BatchCreate(IEnumerable<Contact> entity)
    {
        var records = _mapper.Map<IEnumerable<Infrastructure.Models.Contact>>(entity);
        await _db.Contacts.AddRangeAsync(records);
        return true;
    }

    public async Task<IEnumerable<Contact>> GetByStudentId(Guid? studentId)
    {
        var record = await _db.Contacts.Where(x => x.StudentId == studentId).ToListAsync();
        return record != null! ? _mapper.Map<IEnumerable<Contact>>(record) : new List<Contact>();
    }
}
