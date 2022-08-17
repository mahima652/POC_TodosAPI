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
    [Route("api/Todo")]
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

        [Route("~/api/GetAllTodos")]
        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            List<TodoList> result = await _helperService.GetToDoList();
            if (result == null)
            {
                _logger.LogWarning("List is empty");
                return NotFound($"Item not found Successfully ");
            }
            ResponseMessageObject = ApiResponse.Success(200, result);
            ResponseMessageObject.Message = "Todos List get Exceuted";

            _logger.LogInformation("Todos list get Executed");
            return Ok(ResponseMessageObject);

        }
        [Route("~/api/GetTodo/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetDataById(int id)
        {
            TodoList result = await _helperService.GetToDoListbyID(id);
            if (result == null)
            {
                _logger.LogWarning("Get NOTFOUND of ", id);
                return NotFound($"Item not found Successfully with id = {id} ");
            }
            ResponseMessageObject = ApiResponse.Success(200, result);
            ResponseMessageObject.Message = "Getting item of id: " + id.ToString();

            _logger.LogInformation("Getting item  of", id);
            return Ok(ResponseMessageObject);

        }

        [Route("~/api/AddTodo")]
        [HttpPost]
        public async Task<IActionResult> CreateData(TodoList model)
        {
            if (model == null)
            {
                _logger.LogWarning("Input not valid ");
                return BadRequest("Input not valid ");
            }
            var createdModel = await _helperService.AddTodos(model);
            if (createdModel == null)
            {
                _logger.LogWarning("Item not added Successfully with id :-", model.id);
                Console.WriteLine($"Item not added Successfully with id = {model.id} ");
                return NotFound($"Item not added Successfully with id = {model.id} ");
            }
            ResponseMessageObject = ApiResponse.Success(201, createdModel);
            ResponseMessageObject.Message = " Requted item is created Successfully";

            _logger.LogInformation("New item created");
            return CreatedAtAction(nameof(GetData), new { id = createdModel.id },
               ResponseMessageObject);
        }

        [Route("~/api/UpdateTodo/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateData(TodoList model, int id)
        {
            if (id != model.id)
            {
                _logger.LogWarning("Input not valid ");
                return BadRequest("Input id is not valid ");
            }
            var modelToupdate = await _helperService.GetToDoListbyID(model.id);
            if (modelToupdate == null)
            {
                _logger.LogWarning("Get({Id}) NOT FOUND", id);
                Console.WriteLine($"Model with Id = {id} not found");
                return NotFound($"Model with Id = {id} not found");
            }
            var updatedModel = await _helperService.UpdateTodos(model, id);
            if (updatedModel == null)
            {

                _logger.LogWarning("Item not updated Successfully with id :-", model.id);
                Console.WriteLine($"Item not updated Successfully with id = {model.id} ");
                return NotFound($"Item not updated Successfully with id = {model.id} ");
            }
            ResponseMessageObject = ApiResponse.Success(201, updatedModel);
            ResponseMessageObject.Message = "Requted item is Updated Successfully";

            _logger.LogInformation(" Requested item Updated");
            return CreatedAtAction(nameof(GetData), new { id = updatedModel.id },
                  ResponseMessageObject);

        }

        [Route("~/api/DeleteTodo/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteDataByID(int id)
        {
            bool result = await _helperService.DeleteByID(id);
            if (!result)
            {
                _logger.LogWarning("Get NOT FOUND", id);
                return NotFound($"Item not deleted Successfully with id = {id} ");
            }
            ResponseMessageObject = ApiResponse.Success(200, null);
            ResponseMessageObject.Message = "Requted item is Deleted Successfully";
            
            _logger.LogInformation(" Requested item is Deleted Successfully");
            return Ok(ResponseMessageObject);
        }
    }

    #endregion 
}
