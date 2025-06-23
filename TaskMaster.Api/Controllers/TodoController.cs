using Microsoft.AspNetCore.Mvc;
using TaskMaster.Domaine.Core.DTO;
using TaskMaster.Domaine.Core.Entities;
using TaskMaster.Domaine.Core.Interface;

namespace TaskMaster.Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITaskRepository _taskRepository;

        public TodoController(IUnitOfWork unitOfWork, ITaskRepository taskRepository)
        {
            _unitOfWork = unitOfWork;
            _taskRepository = taskRepository;
        }

        /// <summary>
        /// Récupère toutes les tâches
        /// </summary>
        /// <returns>Liste de toutes les tâches</returns>
        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tasks = await _taskRepository.GetAll();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de la récupération des tâches", error = ex.Message });
            }
        }

        /// <summary>
        /// Récupère une tâche par son ID
        /// </summary>
        /// <param name="id">ID de la tâche</param>
        /// <returns>Tâche correspondant à l'ID</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var task = await _taskRepository.GetByIdAsync(id);
                if (task == null)
                {
                    return NotFound(new { message = "Tâche non trouvée" });
                }
                return Ok(task);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de la récupération de la tâche", error = ex.Message });
            }
        }

        /// <summary>
        /// Récupère les tâches par statut (complétées ou non)
        /// </summary>
        /// <param name="isCompleted">Statut des tâches à récupérer</param>
        /// <returns>Liste des tâches filtrées par statut</returns>
        [HttpGet("ByStatus/{isCompleted}")]
        public async Task<IActionResult> GetByStatus(bool isCompleted)
        {
            try
            {
                var tasks = await _taskRepository.TAskByStatus(isCompleted);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de la récupération des tâches par statut", error = ex.Message });
            }
        }

        /// <summary>
        /// Ajoute une nouvelle tâche
        /// </summary>
        /// <param name="taskAddDTO">Données de la tâche à ajouter</param>
        /// <returns>Résultat de l'opération d'ajout</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TaskAddDTO taskAddDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _taskRepository.AddAsync(taskAddDTO);

                if (result.Success)
                {
                    await _unitOfWork.SaveChangesAsync();
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de l'ajout de la tâche", error = ex.Message });
            }
        }

        /// <summary>
        /// Met à jour une tâche existante
        /// </summary>
        /// <param name="id">ID de la tâche à mettre à jour</param>
        /// <param name="taskUpdateDTO">Nouvelles données de la tâche</param>
        /// <returns>Résultat de l'opération de mise à jour</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskUpdateDTO taskUpdateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                

                var result = await _taskRepository.UpdateAsync(taskUpdateDTO);

                if (result.Success)
                {
                    await _unitOfWork.SaveChangesAsync();
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de la mise à jour de la tâche", error = ex.Message });
            }
        }

        /// <summary>
        /// Supprime une tâche
        /// </summary>
        /// <param name="id">ID de la tâche à supprimer</param>
        /// <returns>Résultat de l'opération de suppression</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _taskRepository.DeleteAsync(id);

                if (result.Success)
                {
                    await _unitOfWork.SaveChangesAsync();
                    return Ok(result);
                }
                else
                {
                    return NotFound(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de la suppression de la tâche", error = ex.Message });
            }
        }
    }
}
