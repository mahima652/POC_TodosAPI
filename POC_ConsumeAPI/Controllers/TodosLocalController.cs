using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POC_ConsumeAPI.Data;
using POC_ConsumeAPI.ExceptionTYpe;
using POC_ConsumeAPI.Helper;
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
    ///  This Controller is used perform CRUD operation on local list 
    /// </summary>
    /// <param name="helperServices"></param>
    /// <param name="logger"></param>
    public class ToDoLocalController : ControllerBase
    {
        #region Property

        private readonly ITodoLocalServices _toDoLocalServices;
        private readonly ILogger<ToDoLocalController> _logger;
        ApiResponse ResponseMessageObject;

        #endregion

        #region HTTP Method

        /// <summary>
        /// Initialize the new instance with Servies and logger 
        /// </summary>
        /// <param name="helperServices"></param>
        /// <param name="logger"></param>
        public ToDoLocalController(ITodoLocalServices toDoLocalServices, ILogger<ToDoLocalController> logger)
        {
            _toDoLocalServices = toDoLocalServices;
            _logger = logger;
        }

        #region HTTP Verbs

        /// <summary>
        /// Get local ToDo list 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [Route("/api/GetAllLocalTodos")]
        [HttpGet]
        public async Task<IActionResult> GetAllToDo()
        {
            List<ToDo> result = await _toDoLocalServices.GetAllAsync();
            _logger.LogInformation("Todos loacl list get Executed");
            ResponseMessageObject = ApiResponse.Success("Todos local List get Exceuted", 200, result);
            return Ok(ResponseMessageObject);
        }

        /// <summary>
        /// Get todo item from local Todo list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        [Route("/api/GetLocalTodo/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetToDo(int id)
        {
            ToDo result = await _toDoLocalServices.GetAsync(id);
            _logger.LogInformation("Getting item of id :", id);
            ResponseMessageObject = ApiResponse.Success("Getting item of id :-" + id.ToString(),200, result);
            return Ok(ResponseMessageObject);

        }

        /// <summary>
        /// Create ToDo item in local ToDo list
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("/api/AddLocalTodo")]
        [HttpPost]
        public async Task<IActionResult> Create(ToDo model)
        {
            if (ModelState.IsValid)
            {
                await _toDoLocalServices.CreateAsync(model);
                _logger.LogInformation("New item created");
                ResponseMessageObject = ApiResponse.Success("Requted item is created Successfully", 201, model);
                return CreatedAtAction(nameof(GetToDo), new { id = model.id },
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
        /// Get Update item in local ToDo list
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("/api/UpdateLocalTodo/{id}")]
        [HttpPut]
        public async Task<IActionResult> Update(ToDo model, int id)
        {
            if (model.id != id)
            {
                _logger.LogWarning("Input not valid");
                ResponseMessageObject = ApiResponse.Fail("Input not valid", 400);
                return BadRequest(ResponseMessageObject);
            }
            await _toDoLocalServices.UpdateAsync(model);
            _logger.LogInformation(" Requested item Updated");
            ResponseMessageObject = ApiResponse.Success("Requted item is Updated Successfully", 201, model);
            return CreatedAtAction(nameof(GetToDo), new { id = model.id },
                  ResponseMessageObject);


        }

        /// <summary>
        /// Delete item for local ToDo list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        [Route("/api/DeleteLocalTodo/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete (int id)
        {
                await _toDoLocalServices.DeleteAsync(id);
                _logger.LogInformation(" Requested item is Deleted");
                ResponseMessageObject = ApiResponse.Success("Successfully Requted item Deleted ", 200, null);
                return Ok(ResponseMessageObject);
        }

        #endregion

    }

    #endregion
}


