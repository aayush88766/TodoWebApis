using TodoListWebAPIs.DTOs;

namespace TodoListWebAPIs.Repository
{
    public interface IRepository<TRequest, TResponse, TKey>
        where TRequest : class
        where TResponse : class
        where TKey : struct
    {
        Task<List<TResponse>> GetAll(QueryParameters queryParameters);
        Task<TResponse> Get(TKey id);
        Task<TResponse> Add(TRequest item);
        Task<TResponse> Update(TKey id, TRequest item);
        Task<bool> Delete(TKey id);
        Task<List<TResponse>> GetByCategory(string category);

    }
}
