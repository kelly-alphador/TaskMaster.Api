using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.Domaine.Core.Interface;
using TaskMaster.Infrastructure.Core.Context;

namespace TaskMaster.Infrastructure.Core.repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly TaskContext _context;
        public UnitOfWork(TaskContext context)
        {
            _context = context;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
