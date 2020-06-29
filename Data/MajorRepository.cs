using Microsoft.EntityFrameworkCore;
using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Data
{
    public class MajorRepository : GenericRepository<Major>, IMajorRepository
    {
        public MajorRepository(DataContext context) : base(context) { }

        new public async Task<List<Major>> GetAll()
        {
            var majorslist = await _context
                .Set<Major>()
                .Include(m => m.Institute).ToListAsync();
            return majorslist;
        }
        new public async Task<Major> Get(int id)
        {
            var major = await _context
                .Set<Major>()
                .Include(x => x.Institute)
                .FirstOrDefaultAsync(x => x.ID == id);
            return major;
        }
    }
}
