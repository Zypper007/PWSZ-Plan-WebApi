using Microsoft.EntityFrameworkCore;
using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Data
{
    public class PlanRepository : GenericRepository<Plan>, IPlanRepository
    {
        public PlanRepository(DataContext context) : base(context) { }

        new public async Task<List<Plan>> GetAll()
        {
            var plans = await _context.Set<Plan>()
                .Include(p => p.Year)
                    .ThenInclude(y => y.Specialization)
                        .ThenInclude(s => s.Major)
                            .ThenInclude(m => m.Institute)
                .Include(p => p.Classes.Where(pc => pc.PlanID == p.ID))
                    .ThenInclude(pc => pc.Class)
                        .ThenInclude(c => c.Subject)
                .Include(p => p.Classes.Where(pc => pc.PlanID == p.ID))
                    .ThenInclude(pc => pc.Class)
                        .ThenInclude(c => c.Room)
                        .ToListAsync();

            return plans;
        }

        new public async Task<Plan> Get(int id)
        {
            var plan = await _context.Set<Plan>()
                .Include(p => p.Year)
                    .ThenInclude(y => y.Specialization)
                        .ThenInclude(s => s.Major)
                            .ThenInclude(m => m.Institute)
                .Include(p => p.Classes.Where(pc => pc.PlanID == p.ID))
                    .ThenInclude(pc => pc.Class)
                        .ThenInclude(c => c.Subject)
                .Include(p => p.Classes.Where(pc => pc.PlanID == p.ID))
                    .ThenInclude(pc => pc.Class)
                        .ThenInclude(c => c.Room)
                .FirstOrDefaultAsync(x => x.ID == id);

            return plan;
        }

    }
}
