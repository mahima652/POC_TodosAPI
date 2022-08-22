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

    /// <summary>
    /// ToDo Controller is used to provide endpoints for 3rd party API
    /// </summary>
    public class ToDoController : ControllerBase
    {
        #region Property 

        private readonly ITodoServices _todoServices;
        private readonly ILogger<ToDoController> _logger;
        ApiResponse ResponseMessageObject ;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize a new instance of ToDo Controller 
        /// </summary>
        /// <param name="todoServices"></param>
        /// <param name="logger"></param>
        public ToDoController (ITodoServices todoServices, ILogger<ToDoController> logger)
        {
            _todoServices = todoServices;
            _logger = logger;
        }

        #endregion

        #region HTTP Verbs

        /// <summary>
        /// Get list of ToDo
        /// </summary>
        /// <returns></returns>
        [Route("/api/GetAllTodos")]
        [HttpGet]
        public async Task<IActionResult> GetAllTodo()
        {
            List<ToDo> todoList = await _todoServices.GetAllAsync();
            if (todoList == null) return NotFound();
            _logger.LogInformation("Todos list get Executed");
            ResponseMessageObject = ApiResponse.Success("Todos List get Exceuted",200, todoList);
            return Ok(ResponseMessageObject);

        }

        /// <summary>
        /// Get individual ToDo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("/api/GetTodo/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetTodo(int id)
        {
            ToDo result = await _todoServices.GetAsync(id);
            if (result == null) return NotFound();
            _logger.LogInformation("Getting item  of", id);
            ResponseMessageObject = ApiResponse.Success($"Getting item of id {id}",200, result);
            return Ok(ResponseMessageObject);

        }

        /// <summary>
        /// Create ToDo
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("/api/AddTodo")]
        [HttpPost]
        public async Task<IActionResult> Create(ToDo model)
        {
            if (ModelState.IsValid)
            {
                await _todoServices.CreateAsync(model);
                if (_todoServices == null) return NotFound();
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

        /// <summary>
        /// Update ToDo
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
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
            await _todoServices.UpdateAsync(model);
            if (_todoServices == null) return NotFound();
            _logger.LogInformation(" Requested item Updated");
            ResponseMessageObject = ApiResponse.Success("Requted item is Updated Successfully",201, model);
            return CreatedAtAction(nameof(GetTodo), new { id = model.id },
                  ResponseMessageObject);

        }

        /// <summary>
        /// Delete ToDo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("/api/DeleteTodo/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            bool status = await _todoServices.DeleteAsync(id);
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

        #endregion 
    }

}
