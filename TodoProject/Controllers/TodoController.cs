using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using TodoProject.Data;
using TodoProject.Models;

namespace TodoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext _dbContext;

        public TodoController(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
       
        [HttpGet]
        public IActionResult GetContacts()
        {
            return Ok(_dbContext.Todo.ToList());
        }

        [HttpGet("{enumstatus}")]

        public async Task<IActionResult> GetByStatus(EnumStatus enumstatus)
        {
            var list = await _dbContext.Todo.Where(x => x.Status == enumstatus).ToListAsync();

            if (list.Count == 0)
            {

                return BadRequest("No Data Exist");
            }

            return Ok(list);
        }


        //[HttpGet]
        //[Route("{id:Guid}")]
        //public IActionResult GetTodo([FromRoute] Guid id)
        //{
        //    var todo = _dbContext.Todo.Find(id);
        //    if (todo == null)
        //    { return NotFound(); }

        //    return Ok(todo);
        //}


        [HttpPost]
        public IActionResult PostTodo(AddTodo addtodo)
        {

            var todo = new Todo();

            todo.Id = Guid.NewGuid();
            todo.Name = addtodo.Name;
            todo.Description = addtodo.Description;
            todo.CreatedTime = System.DateTime.Now;
            todo.UpdatedTime = System.DateTime.Now;
            todo.UserId = addtodo.UserId;
            todo.Status = addtodo.Status;           
            _dbContext.Add(todo);
            _dbContext.SaveChanges();
            return Ok("User Created Successfuly");
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateTodo([FromRoute] Guid id, UpdateTodo updatetodo)
        {
            var todo = _dbContext.Todo.Find(id);
            if (todo != null)
            {

                todo.Name = updatetodo.Name;
                todo.Description = updatetodo.Description;
                todo.CreatedTime = todo.CreatedTime;
                todo.UpdatedTime =DateTime.Now;
                todo.UserId = updatetodo.UserId;
                todo.Status = updatetodo.Status;


                _dbContext.SaveChanges();
                return Ok(todo);
            }

            else
                return NotFound();
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteContact([FromRoute] Guid id)
        {
            var todo = _dbContext.Todo.Find(id);
            if (todo != null)
            {

                _dbContext.Remove(todo);

                _dbContext.SaveChanges();
                return Ok(todo);
            }

            else
                return NotFound();
        }
    }
}
