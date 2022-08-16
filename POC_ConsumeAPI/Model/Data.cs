using POC_ConsumeAPI.Model;

namespace POC_ConsumeAPI.Data
{
    public class TodoList
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public bool completed { get; set; }


    }

    public class DataContext
    {
        public static  List<TodoList> dataCxt { get; set; }

    }
}
