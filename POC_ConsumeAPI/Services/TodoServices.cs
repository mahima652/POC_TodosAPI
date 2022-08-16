using Newtonsoft.Json;
using POC_ConsumeAPI.Data;
using POC_ConsumeAPI.ExceptionTYpe;
using POC_ConsumeAPI.Model;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;

namespace POC_ConsumeAPI.Helper
   
{
    public class TodoServices  : ITodoServices
    {

        private readonly   HttpClient client ;
        private static string BASE_URL  = "https://jsonplaceholder.typicode.com/todos/";
        private static HttpResponseMessage response;
        public static ApiResponse  ResponseObject ;

        public  TodoServices ()
        {
             client = new HttpClient();
             client.BaseAddress = new Uri(BASE_URL);
        }

        public async Task<List<TodoList>> GetToDoList()
        {

            response = await client.GetAsync(BASE_URL);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                Data.DataContext.dataCxt = JsonConvert.DeserializeObject<List<TodoList>>(responseBody);
                Console.WriteLine(responseBody);
                return Data.DataContext.dataCxt;
            }
            else
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", response.ReasonPhrase);
                throw new Exception(response.ReasonPhrase);
            }
                
        }
      
        public async Task<TodoList> GetToDoListbyID(int id)
        {
            var model = new TodoList();
            HttpResponseMessage response = await client.GetAsync(BASE_URL +id);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<TodoList>(responseBody);
                Console.WriteLine(responseBody);
                return model;
            }

            else
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", response.ReasonPhrase);
                throw new Exception(response.ReasonPhrase);
            }

        }

        public async Task<TodoList> AddTodos(TodoList model)
        {
            //HTTP POST

            HttpRequestMessage request = CretaeRequest(model);
            request = new HttpRequestMessage(HttpMethod.Post, BASE_URL);
            HttpResponseMessage response = await client.PostAsync(BASE_URL, request.Content);

            if (CheckStatusCode(response))
                return model;
            else
                throw new InvalidException("Requested Item is not valid ");

        }

        public async Task<TodoList> UpdateTodos(TodoList model, int id)
        {
            //HTTP PUT
            HttpRequestMessage request = CretaeRequest(model);
            request = new HttpRequestMessage(HttpMethod.Put, BASE_URL);
            HttpResponseMessage response = await client.PutAsync(BASE_URL + id, request.Content);

            if (CheckStatusCode(response))
                return model;
            else
                throw new Exception("Requested Item is not valid ");
        }

        public async Task<Boolean> DeleteByID(int id)
        {
            // DELETE
            var url = string.Format(BASE_URL + id, id);
            HttpResponseMessage response = await client.DeleteAsync(url);
            if(CheckStatusCode(response))
            {
                return true;
            }
            throw new NotFoundException("Requested Item not found exception");
        }

        #region Internal Method

        private Boolean CheckStatusCode(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Message :{0} ", response.ReasonPhrase);
                Console.WriteLine("Message :{0} ", response.StatusCode);
                Console.WriteLine("Message :{0} ", response.IsSuccessStatusCode);
                return true;
            }
            else
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", response.ReasonPhrase);
                return false;
            }
        }

        private HttpRequestMessage CretaeRequest(TodoList model)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            var httpRequest = JsonConvert.SerializeObject(model);
            request.Content = new StringContent(httpRequest, Encoding.UTF8);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return request;
        }

        #endregion

    }
}
