using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.Domaine.Core.DTO;
using TaskMaster.Domaine.Core.Entities;

namespace TaskMaster.Domaine.Core.Interface
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskReadDTO>> GetAll();
        Task<bool> TitleExist(string title, int id=0);
        Task<ApiResponse> AddAsync(TaskAddDTO taskAddDTO);
        Task<ApiResponse> UpdateAsync(TaskUpdateDTO taskUpdateDTO);
        Task<ApiResponse> DeleteAsync(int id);
        Task<IEnumerable<TaskReadDTO>> TAskByStatus(bool status);
        Task<TaskReadDTO?> GetByIdAsync(int id);
    }
}
