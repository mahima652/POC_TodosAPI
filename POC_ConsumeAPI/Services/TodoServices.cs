using Newtonsoft.Json;
using POC_ConsumeAPI.Data;
using POC_ConsumeAPI.ExceptionTYpe;
using POC_ConsumeAPI.Model;
using POC_ConsumeAPI.Services.IServices;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;

namespace POC_ConsumeAPI.Helper

{
    /// <summary>
    /// This class is used to perform the CRUD operation on 3rd party todo list
    /// </summary>
    public class TodoServices  : ITodoServices
    {
        #region Property

        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpResponseMessage response;
        private string url ;

        #endregion

        #region Constructor

        /// <summary>
        ///  Initializes  a new instance of the class  
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="clientFactory"></param>
        public TodoServices (IConfiguration configuration , IHttpClientFactory clientFactory)
        {
            _config = configuration;
            _httpClientFactory = clientFactory;
            url = _config.GetValue<string>("BASE_URL");

        }

        #endregion 

        #region HTTP Method

        /// <summary>
        /// Get all Todo list
        /// </summary>
        /// <returns></returns>
        public async Task<List<ToDo>> GetAllAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            if (await SendRequest( request))
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                DataContext.dataCxt = JsonConvert.DeserializeObject<List<ToDo>>(responseBody);
                return DataContext.dataCxt;
            }
            return null;
        }

        /// <summary>
        ///  Get todos item by id  
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ToDo> GetAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{url}{id}");
            if( await SendRequest( request))
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ToDo>(responseBody);
            }
            return null;
        }

        /// <summary>
        ///  Create a new item 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(ToDo model)
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
        public async Task<bool> UpdateAsync(ToDo model)
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

        #endregion

        #region Internal Method 

        /// <summary>
        ///  This method is used to create and send the request 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="request"></param>
        /// <param name="requestType"></param>
        /// <returns></returns>
        private async Task<Boolean> SendRequest(HttpRequestMessage request , ToDo model = null)
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
            if(!response.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
