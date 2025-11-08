using Barcoder;
using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.Barcode;
using LISCareDTO.Projects;
using LISCareUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LISCareLimited.Controllers
{
    [Route(ConstantResource.APIRoute)]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProject project;
        private readonly ILogger<ProjectController> logger;

        public ProjectController(IProject project, ILogger<ProjectController> logger)
        {
            this.project = project;
            this.logger = logger;
        }

        /// <summary>
        /// usedd to add new project
        /// </summary>
        /// <param name="projectRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(ConstantResource.AddNewProject)]
        public async Task<IActionResult> AddProject(ProjectRequest projectRequest)
        {
            logger.LogInformation($"AddNewProject, API execution started at:{DateTime.Now}");
            if (!string.IsNullOrEmpty(projectRequest.ProjectName))
            {
                var result = await project.AddProject(projectRequest);
                logger.LogInformation($"AddNewProject, API execution comleted at:{DateTime.Now} with response:{result}");
                return StatusCode(result.StatusCode, result);
            }
            logger.LogInformation($"AddNewProject, API execution failed at:{DateTime.Now}");
            return BadRequest("Invalid project request");
        }

        /// <summary>
        /// usedd to update existing project
        /// </summary>
        /// <param name="projectRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(ConstantResource.UpdateProject)]
        public async Task<IActionResult> UpdateExistingProject(ProjectRequest projectRequest)
        {
            logger.LogInformation($"UpdateProject, API execution started at:{DateTime.Now}");
            if (!string.IsNullOrEmpty(projectRequest.ProjectName))
            {
                var result = await project.UpdateProject(projectRequest);
                logger.LogInformation($"UpdateProject, API execution comleted at:{DateTime.Now} with response:{result}");
                return StatusCode(result.StatusCode, result);
            }
            logger.LogInformation($"UpdateProject, API execution failed at:{DateTime.Now}");
            return BadRequest("Invalid project request");
        }

        /// <summary>
        /// used to delete existing project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route(ConstantResource.DeleteProject)]
        public async Task<IActionResult> DeleteExistingProject([FromQuery] int projectId, string partnerId)
        {
            logger.LogInformation($"DeleteProject, API execution started at:{DateTime.Now}");
            if (projectId>0)
            {
                var result = await project.DeleteProject(projectId,partnerId);
                logger.LogInformation($"DeleteProject, API execution comleted at:{DateTime.Now} with response:{result}");
                return StatusCode(result.StatusCode, result);
            }
            logger.LogInformation($"DeleteProject, API execution failed at:{DateTime.Now}");
            return BadRequest("Project Id not found.");
        }

        /// <summary>
        /// used to get all projects
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="projectStatus"></param>
        /// <param name="projectName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ConstantResource.GetAllProjects)]
        public async Task<IActionResult> GetAllProjectDetails([FromQuery] string partnerId, bool? projectStatus, string? projectName)
        {
            logger.LogInformation($"GetAllProjects, API execution started at:{DateTime.Now}");
            var response = new APIResponseModel<List<ProjectResponse>>
            {
                Data = []
            };
            try
            {
                response = await project.GetAllProjects(partnerId,projectStatus,projectName);
                logger.LogInformation($"GetAllProjects, API execution completed at:{DateTime.Now}");
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.ResponseMessage = $"An error occurred while processing your request: {ex.Message}";
                logger.LogInformation($"GetAllProjects, API execution failed at:{DateTime.Now} with response {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet]
        [Route(ConstantResource.GetProjectById)]
        public async Task<IActionResult> GetProjectDetails([FromQuery] string partnerId, int projectId)
        {
            logger.LogInformation($"GetProjectById, API execution started at:{DateTime.Now}");
            var response = new APIResponseModel<ProjectResponse>
            {
                Data = new ProjectResponse()
            };
            try
            {
                response = await project.GetProjectById(partnerId, projectId);
                logger.LogInformation($"GetProjectById, API execution completed at:{DateTime.Now}");
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.ResponseMessage = $"An error occurred while processing your request: {ex.Message}";
                logger.LogInformation($"GetProjectById, API execution failed at:{DateTime.Now} with response {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

    }
}
