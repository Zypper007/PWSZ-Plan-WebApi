using Microsoft.EntityFrameworkCore;
using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Data
{
    public class YearRepository : GenericRepository<Year>, IYearRepository
    {
        public YearRepository(DataContext context) : base(context) { }

        new public async Task<Year> Get(int id)
        {
            var year = await _context.Set<Year>()
                .Include(y => y.Specialization)
                .ThenInclude(s => s.Major)
                .ThenInclude(m => m.Institute)
                .FirstOrDefaultAsync(x => x.ID == id);

            return year;
        }

        new public async Task<List<Year>> GetAll()
        {
            var yearsList = await _context.Set<Year>()
                .Include(y => y.Specialization)
                .ThenInclude(s => s.Major)
                .ThenInclude(m => m.Institute)
                .ToListAsync();
            return yearsList;
        }
    }
}
