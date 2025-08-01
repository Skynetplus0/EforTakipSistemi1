/* using Baykasoglu.API.Data;
using Baykasoglu.API.Models.Domain;
using Baykasoglu.API.Models.DTO;
using Baykasoglu.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace Baykasoglu.API.Controllers
{

    // https://localhost:portnum/api/projects
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly MyDbContext dbContext;
        private readonly IProjectRepository projectRepository;

        public ProjectsController(MyDbContext dbContext,IProjectRepository projectRepository )
        {
            this.dbContext = dbContext;
            this.projectRepository = projectRepository;
        }




        //Get all projects
        //GET: https://localhost:portnum/api/projects
        [HttpGet("GetAllProjects")]

        //eskisi public IActionResult GetAll   
        public async Task<IActionResult> GetAll()
        {
            var projects = new List<Projects>
            {
                new Projects
                {
                    Id = Guid.NewGuid(),
                    Name = "Skynet Project",
                    Description="Very dangereous and hard",
                    ProjectImageUrl="https://static.wikia.nocookie.net/moviemorgue/images/b/bf/Skynet.jpeg/revision/latest?cb=20200408192913"
                },
                new Projects
                {
                    Id = Guid.NewGuid(),
                    Name = "Baykas Project",
                    Description="Very caotic and hard",
                    ProjectImageUrl="https://static.wikia.nocookie.net/moviemorgue/images/b/bf/Skynet.jpeg/revision/latest?cb=20200408192913"
                }

            };

            //Get data from database-domain models

            var projects_from_db = dbContext.Projects.ToList();

            var projectsDomain= await dbContext.Projects.ToListAsync();
            
            var projectsDomain_with_reposiyory =await projectRepository.GetAllAsync();


            //Map Domain models to DTOS

            var projectsDTO = new List<ProjectsDTO>();

            foreach (var project in projectsDomain)
            {
                projectsDTO.Add(new ProjectsDTO()
                {

                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    ProjectImageUrl = project.ProjectImageUrl


                });

            }

            //return DTOs

         //   return Ok(projects);
         //  return Ok(projects_from_db);

            return Ok(projectsDTO);

        }


        //get single projects (get project by id)
        // GET : https://localhost:portnum/api/projects/{id}

        //[HttpGet("GetByIdProjects")]
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id) 
        {
            var project= dbContext.Projects.Find(id);

            var project2=dbContext.Projects.FirstOrDefault(x =>x.Id==id );


            var projectsDTO = new ProjectsDTO
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                ProjectImageUrl = project.ProjectImageUrl


            };



            if (project == null)
            {
                return NotFound();
            }


            // return Ok(project);
            return Ok(projectsDTO); 
        }



        //Post to create new project
        // POST: https://localhost:portnumber/api/projects
        [HttpPost]
    public IActionResult Create([FromBody] AddProjectRequestDTO addProjectRequestDTO)
        {
            //map or convert dto to domain model


            if (!dbContext.workers.Any(w => w.Id == addProjectRequestDTO.WorkerId))
                return BadRequest("Geçersiz WorkerId.");


            var projectdomainModel = new Projects
            {
                Name = addProjectRequestDTO.Name,
                Description = addProjectRequestDTO.Description,
                ProjectImageUrl = addProjectRequestDTO.ProjectImageUrl,
                 WorkerId = addProjectRequestDTO.WorkerId

            };
            
            dbContext.Projects.Add(projectdomainModel);
            dbContext.SaveChanges();

            var projectDTO = new ProjectsDTO
            {
                Id = projectdomainModel.Id,
                Name = projectdomainModel.Name,
                Description = projectdomainModel.Description,
                ProjectImageUrl = projectdomainModel.ProjectImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = projectDTO.Id }, projectDTO);

        }


        //Uptade project
        //PUT:hhtps://localhost:portnumber/api/projects/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UptadeProjectRequestDTO uptadeProjectRequestDTO)
        {
            // check if prject exist
            var projectDomainModel = dbContext.Projects.FirstOrDefault(x => x.Id == id);

            if (projectDomainModel == null)
            { 
                return NotFound(); 
            }

            //Map DTO to domain model
            projectDomainModel.Name = uptadeProjectRequestDTO.Name;
            projectDomainModel.Description = uptadeProjectRequestDTO.Description;
            projectDomainModel.ProjectImageUrl = uptadeProjectRequestDTO.ProjectImageUrl;

            dbContext.SaveChanges();

            var projectsDTO = new ProjectsDTO
            {
                Id = projectDomainModel.Id,
                Name = projectDomainModel.Name,
                Description = projectDomainModel.Description,
                ProjectImageUrl = projectDomainModel.ProjectImageUrl



            };

            return Ok(projectsDTO);

        }


        //Delete Project
        //DELETE: hhtps://localhost:portnumber/api/projects/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id) 
        {
            var projectDomainModel=dbContext.Projects.FirstOrDefault(x => x.Id == id);

            if(projectDomainModel == null)
            {
                return NotFound();
            }

            dbContext.Projects.Remove(projectDomainModel);
            dbContext.SaveChanges();


            //return deleted project back
            //map domain model in DTO

            var projectDTO = new ProjectsDTO
            {
                Id = projectDomainModel.Id,
                Name = projectDomainModel.Name,
                Description = projectDomainModel.Description,
                ProjectImageUrl = projectDomainModel.ProjectImageUrl
            };





            return Ok(projectDTO);
        }

    }
}
*/

//Busimess ile repository katmanları eklencek 
//şu anda controleer katmanı var


// Kişi listesi olucak kişiye belli bir gün
//için tıkladığında o günde yaptığı işler gözükçek çalışan
// işi kendi girebilsin

using Baykasoglu.API.Data;
using Baykasoglu.API.Models.Domain;
using Baykasoglu.API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Baykasoglu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly MyDbContext dbContext;

        public ProjectsController(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/projects
        [HttpGet("GetAllProjects")]
       // [Authorize(Roles="")]
        public async Task<IActionResult> GetAll()
        {
            var projects = await dbContext.Projects
                .Where(p=>p.DeletedDateTime==null)
                .Include(p => p.Worker)
                .ToListAsync();

            return Ok(projects);
        }

        // GET: api/projects/{id}
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var project = await dbContext.Projects
                .Include(p => p.Worker)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (project == null)
                return NotFound();

            return Ok(project);
        }

        // POST: api/projects
        [HttpPost("CreateProjects")]
        
        public async Task<IActionResult> Create([FromBody] Projects project)
        {
            if (!dbContext.workers.Any(w => w.Id == project.WorkerId))
                return BadRequest("Geçersiz WorkerId.");

            project.Id = Guid.NewGuid();
            project.Worker = null;
            dbContext.Projects.Add(project);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
        }

        // PUT: api/projects/{id}
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Projects updatedProject)
        {
            var existingProject = await dbContext.Projects.FindAsync(id);
            if (existingProject == null)
                return NotFound();

            // güncelle
            existingProject.Name = updatedProject.Name;
            existingProject.Description = updatedProject.Description;
            existingProject.ProjectImageUrl = updatedProject.ProjectImageUrl;
            existingProject.Date = updatedProject.Date;
            existingProject.WorkerId = updatedProject.WorkerId;
            //
            existingProject.Hours = updatedProject.Hours;
            await dbContext.SaveChangesAsync();
            return Ok(existingProject);
        }

        // DELETE: api/projects/{id}
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var project = await dbContext.Projects.FindAsync(id);
            if (project == null)
                return NotFound();

            // dbContext.Projects.Remove(project);
            //changing soft delete
            project.DeletedDateTime = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            return Ok(project);
        }

        // GET: api/projects/bydate?date=2025-07-09
        [HttpGet("bydate")]
        public async Task<IActionResult> GetByDate([FromQuery] DateTime date)
        {
            var projects = await dbContext.Projects
                .Include(p => p.Worker)
                .Where(p => p.Date.Date == date.Date)
                .ToListAsync();

            return Ok(projects);
        }


        [HttpPut("GetId{id:Guid}")]
        public async Task<IActionResult> UpdateProject(Guid id, [FromBody] Projects updatedProject)
        {
            var existing = await dbContext.Projects.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Hours = updatedProject.Hours;
            existing.Description = updatedProject.Description;
            existing.Name = updatedProject.Name;

            await dbContext.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpPost("CreateProjects1")]
        public async Task<IActionResult> Create1([FromBody] Projects project)
        {
            if (string.IsNullOrWhiteSpace(project.Name))
                return BadRequest("Proje adı gerekli.");

            project.Id = Guid.NewGuid();
            dbContext.Projects.Add(project);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
        }

   



        [Authorize(Roles = "Reader")]
        [HttpPost("AddSimpleProject")]
        //  [AllowAnonymous]
       // [Authorize(Roles = "Reader")]
        public async Task<IActionResult> AddSimpleProject([FromBody] AddBasicProjectRequestDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Project Name is required.");

            var project = new Projects
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Hours = 0,
                Description = string.IsNullOrWhiteSpace(dto.Description) ? "Açıklama girilmedi." : dto.Description,
                Date = DateTime.Now ,// örnek
                WorkerId = null
            };

            dbContext.Projects.Add(project);
            await dbContext.SaveChangesAsync();

            return Ok(project);
        }

        [HttpGet("Unassigned")]
        public async Task<IActionResult> GetUnassignedProjects()
        {
            var result = await dbContext.Projects
                .Where(p => p.WorkerId == null && p.DeletedDateTime == null)
                .ToListAsync();
            return Ok(result);
        }

        // Soft Delete için
        [HttpPut("SoftDelete/{id}")]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            var project = await dbContext.Projects.FindAsync(id);
            if (project == null) return NotFound();

            project.DeletedDateTime = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();

            return Ok();
        }



    }
}