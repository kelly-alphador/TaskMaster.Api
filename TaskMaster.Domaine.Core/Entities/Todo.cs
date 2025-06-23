using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMaster.Domaine.Core.Entities
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string Title {  get; set; }
        public bool IsCompleted {  get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
