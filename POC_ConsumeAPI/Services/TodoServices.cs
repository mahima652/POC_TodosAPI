using Newtonsoft.Json;
using POC_ConsumeAPI.Data;
using POC_ConsumeAPI.ExceptionTYpe;
using POC_ConsumeAPI.Model;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;

namespace POC_ConsumeAPI.Helper
   
{
    public class TodoServices  : ITodoServices
    {

        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpResponseMessage response;
        private string url ;

        public TodoServices (IConfiguration configuration , IHttpClientFactory clientFactory)
        {
            _config = configuration;
            _httpClientFactory = clientFactory;
            url = _config.GetValue<string>("BASE_URL");

        }

        /// <summary>
        /// Get all Todo list
        /// </summary>
        /// <returns></returns>
        public async Task<List<TodoList>> GetAllAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            if (await SendRequest( request))
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<TodoList>>(responseBody);
            }
            return null;
        }

        /// <summary>
        ///  Get todos item by id  
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TodoList> GetAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{url}{id}");
            if( await SendRequest( request))
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TodoList>(responseBody);
            }
            return null;
        }

        /// <summary>
        ///  Create a new item 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(TodoList model)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,url);
            return await SendRequest( request , model );

        }

        /// <summary>
        ///  Update the item 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(TodoList model)
        {
            var  request = new HttpRequestMessage(HttpMethod.Put, $"{url}{model.id}");
            return await SendRequest( request , model);
        }

        /// <summary>
        /// Delete the requested item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{url}{id}");
            return await SendRequest(request );
        }


        #region Internal Method 

        /// <summary>
        ///  This method is used to create and send the request 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="request"></param>
        /// <param name="requestType"></param>
        /// <returns></returns>
        private async Task<Boolean> SendRequest(HttpRequestMessage request , TodoList model = null)
        {
            if (request.Method != HttpMethod.Delete && request.Method != HttpMethod.Get)
            {
                if (model == null)
                    return false;
                request.Content = new StringContent(
                   JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            }
            var client = _httpClientFactory.CreateClient();
            response = await client.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        #endregion
    }
}
