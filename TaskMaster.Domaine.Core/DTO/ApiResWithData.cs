using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMaster.Domaine.Core.DTO
{
    public class ApiResWithData<T>:ApiResponse
    {
        public T? Data { get; set; }
    }
}
