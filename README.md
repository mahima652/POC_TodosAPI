# POC_TODOS
 This Project consume 3 Party Given api(https://jsonplaceholder.typicode.com/todos)
    and create its own endpoints for accessing on it 

# Access Instructions
1. Make clone of this repository by using command "git clone https://github.com/mahima652/POC_TodosAPI". It will copy the code in a local folder.
2. Go to folder "RepositoryLocalFolderPath/POC_ConsumeAPI/" and open "POC_ConsumeAPI.sln" in Visual Studio 2022
3. Build the solution and run applictaion"
4. It will open up the Swagger UI(https://localhost:7098/swagger/index.html) with all the REST APIs available.
5. For Authentication using X-API key that will add the header key for each and  every request
6. Run the following APIs in sequence for authentication ,key should be - "MySecretKey"
7. Without Authentiocation or With wrong  X-API key ,user will not be able to access our end points 
8. This project consist 2 Controller having its own functioality 
  
  
# API Instructions
## Todos Endpoint
This Controller consume 3rd party Given api(https://jsonplaceholder.typicode.com/todos) and provide its own endpoints to access 
1. GET /v1/api/GetAllTodos
   1. X-APIKey header should not be the null and  wrong . Key should be {MySecretKey}
   2. With  null and wrong header key - Response should be fail (as Unauthroized request)
   4. POSTMAN LINK : https://localhost:7098/api/GetAllTodos
2. GET /v1/api/GetTodo/{id}
   1. Id should not be the null and should be provided
   2. Header for X-APIKey should be provided
   3. POSTMAN LINK : https://localhost:7098/api/GetTodo/{id}
   4. Example for (id-5) : https://localhost:7098/api/GetTodo/5
3. POST /v1/api/AddTodo
   1. Header for X-APIKey should be provided
   2. Request body should have {
      "userId": 0,
      "id": 0,
      "title": "string",
      "completed": true
   }
   3. POSTMAN LINK : https://localhost:7098/v1/api/AddTodo
4. PUT vi/api/UpdateTodo/{id}
   1. Header for X-APIKey should be provided
   2. Request body should have {
      "userId": 0,
      "id": 0,
      "title": "string",
      "completed": true
   }
   3. POSTMAN LINK : https://localhost:7098/v1/api/UpdateTodo/{id}
   4. Id of parameter and body should be the same
   5. Example for (id-5) : https://localhost:7098/api/UpdateTodo/5
5. DELETE /v1//api/DeleteTodo/{id}
   1. Header for X-APIKey should be provided
   2. id can not be null and should be provided
   3. POSTMAN LINK :  https://localhost:7098/v1/api/DeleteTodo/{id}
   4. Example for (id-5) : https://localhost:7098/api/DeleteTodo/5
   
## TodosLocal Endpoint
  This Controller consume list from Todos(3rd party ) api and perform CRUD operation on it Locally 

  1. CRUD operatcanion of this controller not perform untill, Fetch the list from 3rd party given api through Todos Controller- [HttpGet]
  2. Once the list save locally then start perform CRUD operation through TodosLocal controller , which will going to update , delete , add and read the Todoslist          locally 

1. GET /v1/api/GetAllLocalTodos
   1. X-APIKey header should not be the null and  wrong . Key should be {MySecretKey}
   2. With  null and wrong header key - Response should be fail (as Unauthroized request)
   3. POSTMAN LINK : https://localhost:7098/api/GetAllLocalTodos
2. GET /v1/api/GetLocalTodo/{id}
   1. Id should not be the null and should be provided
   2. Header for X-APIKey should be provided
   3. POSTMAN LINK : https://localhost:7098/api/GetLocalTodo/{id}
   4. Example for (id-5) : https://localhost:7098/api/GetLocalTodo/5
3. POST /v1/api/AddLocalTodo
   1. Header for X-APIKey should be provided
   2. Request body should have {
      "userId": 0,
      "id": 0,
      "title": "string",
      "completed": true
   }
   3. POSTMAN LINK : https://localhost:7098/v1/api/AddLocalTodo
4. PUT vi/api/UpdateLocalTodo/{id}
   1. Header for X-APIKey should be provided
   2. Request body should have {
      "userId": 0,
      "id": 0,
      "title": "string",
      "completed": true
   }
   3. POSTMAN LINK : https://localhost:7098/v1/api/UpdateLocalTodo/{id}
   4. Id of parameter and body should be the same
   5. Example for (id-5) : https://localhost:7098/api/UpdateLocalTodo/5
5. DELETE /v1//api/DeleteLocalTodo/{id}
   1. Header for X-APIKey should be provided
   2. id can not be null and should be provided
   3. POSTMAN LINK :  https://localhost:7098/v1/api/DeleteLocalTodo/{id}
   3. Example for (id-5) : https://localhost:7098/api/DeleteLocalTodo/5
   
