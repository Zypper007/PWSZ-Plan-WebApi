using Microsoft.EntityFrameworkCore;
using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Data
{
    public class ClassRepository : GenericRepository<Class>, IClassRepository
    {
        public ClassRepository(DataContext context) : base(context) { }

        new public async Task<List<Class>> GetAll()
        {
            var classesList = await _context.Set<Class>()
                .Include(c => c.Subject)
                .Include(c => c.Lecturer)
                .Include(c => c.Room)
                .ToListAsync();

            return classesList;
        }

        new public async Task<Class> Get (int id)
        {
            var @class = await _context.Set<Class>()
                .Include(c => c.Subject)
                .Include(c => c.Lecturer)
                .Include(c => c.Room)
                .Include(c => c.Plans)
                .FirstOrDefaultAsync(x => x.ID == id);

            return @class;
        }
    }
}
