namespace MyMusicStore.Domain.Interfaces;

public interface IRepository<T>
{
    Task Create(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    Task<T> GetById(int? id);
}