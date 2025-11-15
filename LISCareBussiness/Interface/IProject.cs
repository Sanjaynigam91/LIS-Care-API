using LISCareDTO;
using LISCareDTO.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareBussiness.Interface
{
    public interface IProject
    {
        /// <summary>
        /// used to add new project
        /// </summary>
        /// <param name="projectRequest"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> AddProject(ProjectRequest projectRequest);
        /// <summary>
        /// used to update existing project
        /// </summary>
        /// <param name="projectRequest"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> UpdateProject(ProjectRequest projectRequest);
        /// <summary>
        /// used to delete existing project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> DeleteProject(int projectId, string partnerId);
        /// <summary>
        /// used to get project by id
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        Task<APIResponseModel<ProjectResponse>> GetProjectById(string partnerId, int projectId);
        /// <summary>
        /// used to get all projects
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="projectStatus"></param>
        /// <param name="projectName"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<ProjectResponse>>> GetAllProjects(string partnerId, bool? projectStatus, string? projectName);
        /// <summary>
        /// used to get project special rates
        /// </summary>
        /// <param name="optype"></param>
        /// <param name="projectId"></param>
        /// <param name="partnerId"></param>
        /// <param name="testCode"></param>
        /// <returns></returns>
        Task<APIResponseModel<List<ProjectSpecialRateResponse>>> GetProjectSecialRates(string optype, int projectId, string partnerId, string? testCode);
        /// <summary>
        /// used to update project special rates
        /// </summary>
        /// <param name="projectTestMapping"></param>
        /// <returns></returns>
        Task<APIResponseModel<string>> UpdateProjectSpecialRates(ProjectTestMappingRequest projectTestMapping);
    }
}
