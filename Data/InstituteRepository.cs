using Microsoft.EntityFrameworkCore;
using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Data
{
    public class InstituteRepository : GenericRepository<Institute>, IInstituteRepository
    {
        public InstituteRepository(DataContext context) : base(context) { }

        public async Task<List<Major>> GetMajors(int id_i)
        {
            var majorsList = await _context.Set<Major>()
                .Where(x => x.Institute.ID == id_i)
                .ToListAsync();
            return majorsList;
        }
    }
}
