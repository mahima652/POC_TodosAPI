using POC_ConsumeAPI.Data;


namespace POC_ConsumeAPI.Helper
{
    public interface IServices
    {
        Task<List<TodoList>> GetToDoList();

        Task<TodoList> GetToDoListbyID(int id);

        Task<Boolean> DeleteByID(int id );

        Task<TodoList> AddTodos(TodoList model);

        Task<TodoList> UpdateTodos(TodoList model, int id);
    }

    public interface ITodoServices : IServices
    {

    }

    public interface ITodoLocalServices : IServices
    {

    }
}

