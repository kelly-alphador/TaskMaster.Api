using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMaster.Domaine.Core.Interface
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
