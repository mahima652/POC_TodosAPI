//using Newtonsoft.Json;
//using POC_ConsumeAPI.Data;
//using POC_ConsumeAPI.ExceptionTYpe;
//using POC_ConsumeAPI.Model;
//using System.Net.Http.Formatting;
//using System.Net.Http.Headers;
//using System.Text;


//namespace POC_ConsumeAPI.Helper
//{
  
//    public class TodoLocalServices : ITodoLocalServices
//    {

//        private readonly IHttpClientFactory _httpClientFactory;
//        private static List<TodoList> result;

//        public TodoLocalServices(IHttpClientFactory clientFactory)
//        {
//            _httpClientFactory = clientFactory;
//        }
//        public async Task<List<TodoList>> GetToDoList()
//        {
//            result = Data.DataContext.dataCxt;
//            CheckList();
//            return result;
//        }

//        public async Task<TodoList> GetToDoListbyID(int id)
//        {
//            CheckList();
//            var resultModel = result.FirstOrDefault(x => x.id == id);
//            if (resultModel != null)
//            {
//                return resultModel;
//            }
//            Console.WriteLine("Requested Id not found ");
//            return null;
//        }

//        public async Task<TodoList> AddTodos(TodoList model)
//        {
//            CheckList();
//            var updatedModel = result.FirstOrDefault(x => x.id == model.id);
//            if (updatedModel == null)
//            {
//                Data.DataContext.dataCxt.Add(model);
//                return model;
//            }
//            Console.WriteLine("Requested Item is already present ");
//            throw new InvalidException("Requested Item is already present ");
//        }

//        public async Task<TodoList> UpdateTodos(TodoList model, int id)
//        {
//            CheckList();
//            var updatedModel = result.FirstOrDefault(x => x.id == model.id);
//            if (updatedModel != null)
//            {
//                updatedModel.id = model.id;
//                updatedModel.userId = model.userId;
//                updatedModel.completed = model.completed;
//                updatedModel.title = model.title;

//                return updatedModel;
//            }
//            Console.WriteLine("Requested Id not found ");
//            throw new NotFoundException("Requested Id not found "); 
//        }

//        public async Task<Boolean> DeleteByID(int id)
//        {
//            CheckList();
//            var model = result.FirstOrDefault(x => x.id == id);
//            if (model != null)
//            {
//                Data.DataContext.dataCxt.Remove(model);
//                return true;
//            }
//            Console.WriteLine("Requested Id not found ");
//            throw new NotFoundException("Requested Id not found ");

//        }


//        #region Internal method 

//        private void CheckList()
//        {
//            if (result == null)
//            {
//                Console.WriteLine("List is not present locally");
//                throw new InvalidException("List is not present locally ," +
//                   " First need to fetch the list from Todos");
//            }
//        }

//        private void CheckItemById(int id)
//        {
//            var resultModel = result.FirstOrDefault(x => x.id == id);
//        }

//        #endregion
//    }
//}
