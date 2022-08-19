using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using POC_ConsumeAPI.Data;
using POC_ConsumeAPI.ExceptionTYpe;
using POC_ConsumeAPI.Middleware;
using POC_ConsumeAPI.Model;
using POC_ConsumeAPI.Services.IServices;

namespace POC_ConsumeAPI.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/Todo")]
    [ApiController]

    public class TodosController : ControllerBase
    {
        private readonly ITodoServices _todoServices_;
        private readonly ILogger<TodosController> _logger;
        ApiResponse ResponseMessageObject ;


        public TodosController (ITodoServices todoServices, ILogger<TodosController> logger)
        {
            _todoServices_ = todoServices;
            _logger = logger;
        }

        #region HTTP Verbs

        [Route("/api/GetAllTodos")]
        [HttpGet]
        public async Task<IActionResult> GetAllTodo()
        {
            List<ToDo> result = await _todoServices_.GetAllAsync();
            if (result == null)
            {
                _logger.LogWarning("List is empty");
                return NotFound($"Item not found Successfully ");
            }

            _logger.LogInformation("Todos list get Executed");
            ResponseMessageObject = ApiResponse.Success("Todos List get Exceuted",200, result);
            return Ok(ResponseMessageObject);

        }
        [Route("/api/GetTodo/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetTodo(int id)
        {
            ToDo result = await _todoServices_.GetAsync(id);
            if (result == null)
            {
                _logger.LogWarning("Get NOTFOUND of ", id);
                return NotFound($"Item not found Successfully with id = {id} ");
            }

            _logger.LogInformation("Getting item  of", id);
            ResponseMessageObject = ApiResponse.Success($"Getting item of id {id}",200, result);
            return Ok(ResponseMessageObject);

        }

        [Route("/api/AddTodo")]
        [HttpPost]
        public async Task<IActionResult> Create(ToDo model)
        {
            if (ModelState.IsValid)
            {
                await _todoServices_.CreateAsync(model);
                _logger.LogInformation("New item created");
                ResponseMessageObject = ApiResponse.Success("Requted item is created Successfully" ,201, model);
                return CreatedAtAction(nameof(GetTodo), new { id = model.id },
                   ResponseMessageObject);
            }
            else
            {
                _logger.LogWarning("Input not valid");
                ResponseMessageObject = ApiResponse.Fail("Input not valid", 400);
                return BadRequest(ResponseMessageObject);
            }
               
        }

        [Route("/api/UpdateTodo/{id}")]
        [HttpPut]
        public async Task<IActionResult> Update(ToDo model, int id)
        {
           
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Input not valid");
                ResponseMessageObject = ApiResponse.Fail("Input not valid", 400);
                return BadRequest(ResponseMessageObject);
            }

            var status = await _todoServices_.UpdateAsync(model);
            if (!status)
            {
                _logger.LogWarning("Get({Id}) NOT FOUND", id);
                ResponseMessageObject = ApiResponse.Fail("Requted item not found", 404);
                return NotFound(ResponseMessageObject);
            }

            _logger.LogInformation(" Requested item Updated");
            ResponseMessageObject = ApiResponse.Success("Requted item is Updated Successfully",201, model);
            return CreatedAtAction(nameof(GetTodo), new { id = model.id },
                  ResponseMessageObject);

        }

        [Route("/api/DeleteTodo/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            bool status = await _todoServices_.DeleteAsync(id);
            if (!status)
            {
                _logger.LogWarning($"Requted item of id {id} not found");
                ResponseMessageObject = ApiResponse.Fail("Requted item not found" , 404);
                return NotFound(ResponseMessageObject);
            }

            _logger.LogInformation(" Requested item is Deleted Successfully");
            ResponseMessageObject = ApiResponse.Success("Requted item is Deleted Successfully",200, null);
            return Ok(ResponseMessageObject);
        }
    }

    #endregion 
}
