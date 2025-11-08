using LISCareBussiness.Interface;
using LISCareDTO;
using LISCareDTO.Projects;
using LISCareRepository.Implementation;
using LISCareRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareBussiness.Implementation
{
    public class ProjectBAL : IProject
    {
        private readonly IProjectRepository projectRepository;
        public ProjectBAL(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }
        /// <summary>
        /// used to add new project
        /// </summary>
        /// <param name="projectRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> AddProject(ProjectRequest projectRequest)
        {
            try
            {
                return await projectRepository.AddProject(projectRequest);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used to delete existing project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> DeleteProject(int projectId, string partnerId)
        {
            try
            {
                return await projectRepository.DeleteProject(projectId,partnerId);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used to get all projects
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="projectStatus"></param>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<ProjectResponse>>> GetAllProjects(string partnerId, bool? projectStatus, string? projectName)
        {
            try
            {
                return await projectRepository.GetAllProjects(partnerId,projectStatus,projectName);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used to get project by id
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<ProjectResponse>> GetProjectById(string partnerId, int projectId)
        {
            try
            {
                return await projectRepository.GetProjectById(partnerId,projectId);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used to update existing project
        /// </summary>
        /// <param name="projectRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<string>> UpdateProject(ProjectRequest projectRequest)
        {
            try
            {
                return await projectRepository.UpdateProject(projectRequest);
            }
            catch
            {
                throw;
            }
        }
    }
}
