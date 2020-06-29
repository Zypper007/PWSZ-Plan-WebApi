using Microsoft.EntityFrameworkCore;
using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Data
{
    public class SpecializationRepository : GenericRepository<Specialization>, ISpecializationsRepository
    {
        public SpecializationRepository(DataContext context) : base(context)
        {
        }

        new public async Task<Specialization> Get(int id)
        {
            var spec = await _context.Set<Specialization>()
                .Include(s => s.Major)
                .ThenInclude(m => m.Institute)
                .FirstOrDefaultAsync(x => x.ID == id);
            return spec;
        }

        new public async Task<List<Specialization>> GetAll()
        {
            var specList = await _context.Set<Specialization>()
                .Include(s => s.Major)
                .ThenInclude(m => m.Institute)
                .ToListAsync();
            return specList;
        }
    }
}
