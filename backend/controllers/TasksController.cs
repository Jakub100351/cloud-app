using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CloudBackend.DTOs; // Twoje DTOsy

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskReadDto>>> GetAll()
        {
            var tasks = await _context.Tasks.ToListAsync();
            
            // MAPOWANIE: Bierzemy 'Title' z bazy i wrzucamy do 'Name' w DTO
            var tasksDto = tasks.Select(t => new TaskReadDto
            {
                Id = t.Id,
                Name = t.Title, 
                IsCompleted = t.IsCompleted
            });
            return Ok(tasksDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskReadDto>> GetById(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound(); 
            
            return Ok(new TaskReadDto 
            { 
                Id = task.Id, 
                Name = task.Title, // MAPOWANIE
                IsCompleted = task.IsCompleted 
            });
        }

        [HttpPost]
        public async Task<ActionResult<TaskReadDto>> Create(TaskCreateDto taskDto)
        {
            // Używamy poprawnej klasy: TaskItem
            var newTask = new TaskItem 
            {
                Title = taskDto.Name, // MAPOWANIE z DTO do bazy
                IsCompleted = false 
            };

            _context.Tasks.Add(newTask);
            await _context.SaveChangesAsync();

            var readDto = new TaskReadDto
            {
                Id = newTask.Id,
                Name = newTask.Title,
                IsCompleted = newTask.IsCompleted
            };

            return CreatedAtAction(nameof(GetById), new { id = readDto.Id }, readDto);
        }

        // Używamy TaskReadDto również do Update'a, żeby API w ogóle nie przyjmowało TaskItem (wymóg prowadzącego)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TaskReadDto taskDto)
        {
            if (id != taskDto.Id) return BadRequest("Niezgodność ID");
            
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            // Aktualizacja encji
            task.Title = taskDto.Name;
            task.IsCompleted = taskDto.IsCompleted;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();
            
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}