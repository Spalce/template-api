namespace Template.Core.Interfaces;

public interface IBaseCrud<T>
{
    Task<T> GetById(Guid? id);
    Task<IEnumerable<T>> GetAll();
    Task<T> Create(T entity);
    Task<bool> Update(Guid? id, T entity);
    Task<bool> Delete(Guid? id);
    Task<T> IsExist(T entity);
    bool BatchDelete(IEnumerable<T> entity);
    bool BatchUpdate(IEnumerable<T> entity);
    Task<bool> BatchCreate(IEnumerable<T> entity);
}
