using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using POC_ConsumeAPI.Data;
using POC_ConsumeAPI.ExceptionTYpe;
using POC_ConsumeAPI.Helper;
using POC_ConsumeAPI.Middleware;
using POC_ConsumeAPI.Model;

namespace POC_ConsumeAPI.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]

    public class TodosController : ControllerBase
    {
        private readonly ITodoServices _helperService;
        private readonly ILogger<TodosController> _logger;
        ApiResponse ResponseMessageObject ;


        public TodosController (ITodoServices helperService, ILogger<TodosController> logger)
        {
            _helperService = helperService ;
            _logger = logger;
        }

        #region HTTP Verbs

        [Route("/Todolist/Get")]
        [HttpGet()]
        public async Task<IActionResult> GetData()
        {
            List<TodoList> result = await _helperService.GetToDoList();
            if (result != null)
            {
                _logger.LogInformation("Todos list get Executed");
                ResponseMessageObject = ApiResponse.Success(200, result);
                ResponseMessageObject.Message = "Todos List get Exceuted";
                return Ok(ResponseMessageObject);
            }
            throw new Exception("Error feteching data : Server Error");
           
        }

        [Route("Todolist/GetById")]
        [HttpGet("{paramOne}")]
        public async Task<IActionResult> GetDataById(int paramOne)
        {
            TodoList result = await _helperService.GetToDoListbyID(paramOne);
            if (result == null)
            {
                _logger.LogWarning("Get({Id}) NOT FOUND", paramOne);
                throw new NotFoundException("Requested Item Not Found");
            }
            _logger.LogInformation("Getting item {paramOne}", paramOne);
            ResponseMessageObject = ApiResponse.Success(200, result);
            ResponseMessageObject.Message = "Getting item of {Id}" + paramOne.ToString();
            return Ok(ResponseMessageObject);

        }

        [Route("/Todolist/Create")]
        [HttpPost()]
        public async Task<IActionResult> CreateData(TodoList model)
        {

            if (model == null)
            {
                _logger.LogWarning("Input not valid ");
                return BadRequest("Input not valid ");
            }

            var expected = await _helperService.GetToDoListbyID(model.id);
            if (expected != null)
            {
                Console.WriteLine("Model is already in use");
                _logger.LogWarning("Requested input is already present");
                return BadRequest("Requested input is already present");
            }

            var createdModel = await _helperService.AddTodos(model);
            ResponseMessageObject = ApiResponse.Success(201, createdModel);
            ResponseMessageObject.Message = "Successfully Requted item Cretaed ";
            _logger.LogInformation("New item created");
            return CreatedAtAction(nameof(GetData), new { id = createdModel.id },
               ResponseMessageObject);
        }


        [Route("/Todolist/Update")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateData(TodoList model, int id)
        {

            if (id != model.id)
            {
                _logger.LogWarning("Input not valid ");
                return BadRequest();
            }
            var modelToupdate = await _helperService.GetToDoListbyID(model.id);
            if (modelToupdate == null)
            {
                _logger.LogWarning("Get({Id}) NOT FOUND", id);
                Console.WriteLine("Model with Id = {id} not found");
                return NotFound($"Model with Id = {id} not found");
            }

            var updatedModel = await _helperService.UpdateTodos(model, id);
            _logger.LogInformation(" Requested item Updated");
            ResponseMessageObject = ApiResponse.Success(200, updatedModel);
            ResponseMessageObject.Message = "Successfully Requted item Updated ";
            return CreatedAtAction(nameof(GetData), new { id = updatedModel.id },
                  ResponseMessageObject);

        }


        [Route("/Todolist/Delete")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDataByID(int id)
        {

            bool result = await _helperService.DeleteByID(id);
            if (!result)
            {
                _logger.LogWarning("Get({Id}) NOT FOUND", id);
                throw new NotFoundException("Requested Item Not Found");
            }
            _logger.LogInformation(" Requested item is Deleted");
            ResponseMessageObject = ApiResponse.Success(200, null);
            ResponseMessageObject.Message = "Successfully Requted item Deleted ";
            return Ok(ResponseMessageObject);
        }
    }

    #endregion 
}
