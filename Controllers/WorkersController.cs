using Baykasoglu.API.Data;
using Baykasoglu.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Baykasoglu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly MyDbContext dbContext;

        public WorkerController(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/worker
        [HttpGet("GetAllWorkers")]
        public async Task<IActionResult> GetAllWorkers()
        {
            var workers = await dbContext.workers
                .Where(w=>w.DeletedDateTime==null)
                .Include(w => w.Projects)
                .ToListAsync();

            return Ok(workers);
        }

        // GET: api/worker/{id}
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var worker = await dbContext.workers
                .Include(w => w.Projects)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (worker == null)
                return NotFound();

            return Ok(worker);
        }

        // GET: api/worker/bydate?date=2025-07-09
        [HttpGet("bydate")]
        public async Task<IActionResult> GetWorkersWithProjectsByDate([FromQuery] DateTime date)
        {
            var workers = await dbContext.workers
                .Include(w => w.Projects.Where(p => p.Date.Date == date.Date))
                .ToListAsync();

            return Ok(workers);
        }

        // POST: api/worker
        [HttpPost("CreateWorkers")]
        public async Task<IActionResult> Create([FromBody] Worker worker)
        {
            worker.Id = Guid.NewGuid();
            dbContext.workers.Add(worker);
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = worker.Id }, worker);
        }

        // PUT: api/worker/{id}
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Worker updatedWorker)
        {
            var existingWorker = await dbContext.workers.FindAsync(id);
            if (existingWorker == null)
                return NotFound();

            existingWorker.Name = updatedWorker.Name;
            existingWorker.Surname = updatedWorker.Surname;

            await dbContext.SaveChangesAsync();
            return Ok(existingWorker);
        }

        // DELETE: api/worker/{id}
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var worker = await dbContext.workers.FindAsync(id);
            if (worker == null)
                return NotFound();

           // dbContext.workers.Remove(worker);
           //change soft delete
            worker.DeletedDateTime = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
            return Ok(worker);
        }


        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthlyReport(Guid workerId, int year, int month)
        {
            var worker = await dbContext.workers
                .Include(w => w.Projects.Where(p =>
                    p.Date.Year == year && p.Date.Month == month))
                .FirstOrDefaultAsync(w => w.Id == workerId);

            if (worker == null)
                return NotFound();

            var daysInMonth = DateTime.DaysInMonth(year, month);
            var report = new List<object>();

            for (int day = 1; day <= daysInMonth; day++)
            {
                var date = new DateTime(year, month, day);
                var project = worker.Projects.FirstOrDefault(p => p.Date.Date == date);

                report.Add(new
                {
                    Id= project?.Id,
                    Name = worker.Name,
                    Surname = worker.Surname,
                    Date = date.ToString("yyyy-MM-dd"),
                    ProjectName = project?.Name,
                    Hours = project?.Hours,
                    Description = project?.Description
                });
            }

            return Ok(report);
        }


        [HttpGet("summary")]
        public async Task<IActionResult> GetMonthlySummary(int year, int month)
        {
            var workers = await dbContext.workers
                .Where(w => w.DeletedDateTime == null)
                .Include(w => w.Projects.Where(p =>
                    p.Date.Year == year &&
                    p.Date.Month == month &&
                    p.DeletedDateTime == null))
                .ToListAsync();

            var result = workers.Select(w => new
            {
                w.Name,
                w.Surname,
                TotalHours = w.Projects.Sum(p => p.Hours),
                DaysWorked = w.Projects.Select(p => p.Date.Date).Distinct().Count(),
                Projects = w.Projects
                    .GroupBy(p => p.Name)
                    .Select(g => new { Name = g.Key, Hours = g.Sum(p => p.Hours) })
            });

            return Ok(result);
        }




    }
}