using POC_ConsumeAPI.Data;


namespace POC_ConsumeAPI.Helper
{
    public interface IServices
    {
        Task<List<TodoList>> GetAllAsync();

        Task<TodoList> GetAsync(int id);

        Task<bool> CreateAsync(TodoList model);

        Task<bool> UpdateAsync(TodoList model);

        Task<bool> DeleteAsync( int id);
    }

    public interface ITodoServices : IServices
    {

    }

    public interface ITodoLocalServices : IServices
    {

    }
}

