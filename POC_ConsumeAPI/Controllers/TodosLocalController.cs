//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using POC_ConsumeAPI.Data;
//using POC_ConsumeAPI.ExceptionTYpe;
//using POC_ConsumeAPI.Helper;
//using POC_ConsumeAPI.Middleware;
//using POC_ConsumeAPI.Model;

//namespace POC_ConsumeAPI.Controllers
//{
//    [Consumes("application/json")]
//    [Produces("application/json")]
//    [Route("api/Todo")]
//    [ApiController]
//    public class TodosLocalController : ControllerBase
//    {
//        private readonly ITodoLocalServices _helperService;
//        private readonly ILogger<TodosController> _logger;
//        ApiResponse ResponseMessageObject;


//        public TodosLocalController(ITodoLocalServices helperServices, ILogger<TodosController> logger)
//        {
//            _helperService = helperServices;
//            _logger = logger;
//        }

//        #region HTTP Verbs

//        [Route("/api/GetAllLocalTodos")]
//        [HttpGet]
//        public async Task<IActionResult> GetTodosList()
//        {
//            List<TodoList> result = await _helperService.GetToDoList();
//            if (result != null)
//            {
//                _logger.LogInformation("Todos loacl list get Executed");
//                ResponseMessageObject = ApiResponse.Success(200, result);
//                ResponseMessageObject.Message = "Todos local List get Exceuted";
//                return Ok(ResponseMessageObject);
//            }
//            throw new Exception("Error feteching data : Internal Server Error");
//        }

//        [Route("/api/GetLocalTodo/{id}")]
//        [HttpGet]
//        public async Task<IActionResult> GetTodosById(int id)
//        {
//            TodoList result = await _helperService.GetToDoListbyID(id);
//            if (result == null)
//            {
//                _logger.LogWarning(" NOT FOUND", id);
//                throw new NotFoundException("Requested Item Not Found");
//            }
//            _logger.LogInformation("Getting item of id :", id);
//            ResponseMessageObject = ApiResponse.Success(200, result);
//            ResponseMessageObject.Message = "Getting item of id :-" + id.ToString();
//            return Ok(ResponseMessageObject);

//        }

//        [Route("/api/AddLocalTodo")]
//        [HttpPost]
//        public async Task<IActionResult> CreateLocalTodo(TodoList model)
//        {

//            if (model == null)
//            {
//                _logger.LogWarning("Input not valid ");
//                return BadRequest("Input not valid ");
//            }

//            var expected = await _helperService.GetToDoListbyID(model.id);
//            if (expected != null)
//            {
//                Console.WriteLine("Model is already in use");
//                _logger.LogWarning("Requested input is already present");
//                return BadRequest("Requested input is already present");
//            }

//            var createdModel = await _helperService.AddTodos(model);
//            ResponseMessageObject = ApiResponse.Success(201, createdModel);
//            ResponseMessageObject.Message = "Successfully Requted item Cretaed ";
//            _logger.LogInformation("New item created");
//            return CreatedAtAction(nameof(GetTodosList), new { id = createdModel.id },
//               ResponseMessageObject);

//        }

//        [Route("/api/UpdateLocalTodo/{id}")]
//        [HttpPut]
//        public async Task<IActionResult> UpdateLocalTodo(TodoList model, int id)
//        {

//            if (id != model.id)
//            {
//                _logger.LogWarning("Input not valid :- Requested id is not matched");
//                return BadRequest("Input not valid :- Requested id is not matched");
//            }
//            var modelToupdate = await _helperService.GetToDoListbyID(model.id);
//            if (modelToupdate == null)
//            {
//                _logger.LogWarning("Get({Id}) NOT FOUND", id);
//                Console.WriteLine("Model with Id = {id} not found");
//                return NotFound($"Model with Id = {id} not found");
//            }

//            var updatedModel = await _helperService.UpdateTodos(model, id);
//            _logger.LogInformation(" Requested item Updated");
//            ResponseMessageObject = ApiResponse.Success(200, updatedModel);
//            ResponseMessageObject.Message = "Successfully Requted item Updated ";
//            return CreatedAtAction(nameof(GetTodosList), new { id = updatedModel.id },
//                  ResponseMessageObject);

//        }

//        [Route("/api/DeleteLocalTodo/{id}")]
//        [HttpDelete]
//        public async Task<IActionResult> DeleteLocalTodoByID(int id)
//        {
//            bool result = await _helperService.DeleteByID(id);
//            if (!result)
//            {
//                _logger.LogWarning("Get({Id}) NOT FOUND", id);
//                throw new NotFoundException("Requested Item Not Found");
//            }
//            _logger.LogInformation(" Requested item is Deleted");
//            ResponseMessageObject = ApiResponse.Success(200, null);
//            ResponseMessageObject.Message = "Successfully Requted item Deleted ";
//            return Ok(ResponseMessageObject);

//        }


//    }

//    #endregion
//}


