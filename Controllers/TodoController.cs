using Microsoft.AspNetCore.Mvc;
using System.Net;
using TodoListWebAPIs.DTOs;
using TodoListWebAPIs.DTOs.Requests;
using TodoListWebAPIs.DTOs.Responses;
using TodoListWebAPIs.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoListWebAPIs.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IRepository<TodoRequest, TodoResponse, int> _repository;

        public TodoController(IRepository<TodoRequest, TodoResponse, int> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [ProducesResponseType(200, Type=typeof(TodoResponse))]
        public async Task<IActionResult> GetAll([FromBody] QueryParameters queryParameters)
        {
            List<TodoResponse> allTodos;
            try
            {
                allTodos = await _repository.GetAll(queryParameters);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadGateway, ex.Message);
            }
            return StatusCode((int)HttpStatusCode.OK, allTodos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> Get(int id)
        {
            TodoResponse todoResponse;
            try
            {
                todoResponse = await _repository.Get(id);
            }
            catch (KeyNotFoundException ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadGateway, ex.Message);
            }
            return StatusCode((int)HttpStatusCode.OK, todoResponse);
        }

        [HttpPost]
        public async Task<ActionResult<TodoResponse>> Add([FromBody] TodoRequest todoRequest)
        {
            TodoResponse todoResponse;
            try
            {
                todoResponse = await _repository.Add(todoRequest);
            }
            catch (ArgumentNullException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadGateway, ex.Message);
            }
            return Ok("abc");
            // return StatusCode((int)HttpStatusCode.Created, todoResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TodoRequest todoRequest)
        {
            TodoResponse todoResponse;
            try
            {
                todoResponse = await _repository.Update(id, todoRequest);
            }
            catch (ArgumentNullException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadGateway, ex.Message);
            }
            return StatusCode((int)HttpStatusCode.OK, todoResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = false;
            try
            {
                isDeleted = await _repository.Delete(id);
            }
            catch (KeyNotFoundException ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadGateway, ex.Message);
            }
            return StatusCode((int)HttpStatusCode.OK, isDeleted);
        }
    }
}