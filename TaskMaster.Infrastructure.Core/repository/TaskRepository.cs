using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskMaster.Domaine.Core.DTO;
using TaskMaster.Domaine.Core.Entities;
using TaskMaster.Domaine.Core.Interface;
using TaskMaster.Infrastructure.Core.Context;

namespace TaskMaster.Infrastructure.Core.repository
{
    public class TaskRepository:ITaskRepository
    {
        private readonly TaskContext _taskContext;
        public TaskRepository(TaskContext taskContext)
        {
            _taskContext = taskContext;
        }
        public async Task<bool> TitleExist(string title, int id=0)
        {
            var query = _taskContext.Todos.Where(t => t.Title == title);
            if (id != 0)
            {
                query=query.Where(t => t.Id != id);
            }
            return await query.AnyAsync();
        }
        public async Task<IEnumerable<TaskReadDTO>> GetAll()
        {
            try
            {
                return await _taskContext.Todos.Select(x => new TaskReadDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                    IsCompleted = x.IsCompleted,
                    CreatedAt = x.CreatedAt,
                }).OrderBy(x=>x.Id).ToListAsync();
            }
            catch (Exception ex) 
            {
                throw new Exception($"erreur {ex.Message}");
            }
        }
        public async Task<ApiResponse> AddAsync(TaskAddDTO taskAddDTO)
        {
            try
            {
                if(await TitleExist(taskAddDTO.Title))
                {
                    return new ApiResponse
                    {
                        Success= false,
                        Message="title existe deja",
                        Errors = {"title existe"}
                    };
                }
                var task = new Todo
                {
                 
                    Title = taskAddDTO.Title,
                    IsCompleted = taskAddDTO.IsCompleted,
                    CreatedAt = taskAddDTO.CreatedAt,
                };
                _taskContext.Todos.Add(task);
                return new ApiResponse
                {
                    Success = true,
                    Message = "Donnees enregistrer avec success"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = true,
                    Message = $"une erreur se produit lors de l'ajout{ex.Message}",
                    Errors = { ex.Message }
                };
            }
        }
        public async Task<ApiResponse> UpdateAsync(TaskUpdateDTO taskUpdateDTO)
        {
            try
            {
                var exist = await _taskContext.Todos.FindAsync(taskUpdateDTO.Id);
                if (exist == null)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "id existe",
                        Errors = { "id existe" }
                    };
                }
                if (await TitleExist(taskUpdateDTO.Title, taskUpdateDTO.Id))
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Title existe",
                        Errors = { "title existe" }
                    };
                }
                exist.Title = taskUpdateDTO.Title;
                exist.IsCompleted = taskUpdateDTO.IsCompleted;
                exist.CreatedAt = taskUpdateDTO.CreatedAt;
                _taskContext.Todos.Update(exist);
                return new ApiResponse
                {
                    Success = true,
                    Message = "Donnees modifier avec success",
                    Errors = { "Donnees modifier avec success" }
                };

            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = $"une erreur se produit lors de la modification {ex.Message}",
                    Errors = { ex.Message }
                };
            }
        }
        public async Task<ApiResponse> DeleteAsync(int id)
        {
            try
            {
                var exist = await _taskContext.Todos.FindAsync(id);
                if (exist == null)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "id existe",
                        Errors = { "id existe" }
                    };
                }
                _taskContext.Todos.Remove(exist);
                return new ApiResponse
                {
                    Success = true,
                    Message = "Donnees supprimer avec succes",
                   
                };
            }
            catch(Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = $"une erreur se produit lors de la suppression {ex.Message}",
                    Errors = { ex.Message }
                };
            }
        }
        public async Task<IEnumerable<TaskReadDTO>> TAskByStatus(bool status)
        {
            try
            {
                return await _taskContext.Todos
                    .Where(t => t.IsCompleted == status)
                    .Select(x => new TaskReadDTO
                    {
                        Id = x.Id,
                        Title = x.Title,
                        IsCompleted = x.IsCompleted,
                        CreatedAt = x.CreatedAt,
                    })
                    .OrderBy(x => x.Id)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la récupération des tâches: {ex.Message}");
            }
        }
        public async Task<TaskReadDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _taskContext.Todos
               .Where(x => x.Id == id)
               .Select(x => new TaskReadDTO
               {
                   Id = x.Id,
                   Title = x.Title,
                   IsCompleted = x.IsCompleted,
                   CreatedAt = x.CreatedAt,
               })
               .FirstOrDefaultAsync();
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
