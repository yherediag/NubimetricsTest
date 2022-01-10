using API.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Repository
{
    public class EFRepository<TEntity> : IEFRepository<TEntity> where TEntity : class
    {
        private readonly NubimetricsExampleContext _context;
        private DbSet<TEntity> _dbSet;

        public EFRepository(NubimetricsExampleContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<IList<TEntity>> Get() => await _dbSet.ToListAsync();

        public async Task<TEntity> Get(int id) => await _dbSet.FindAsync(id);

        public async Task Post(TEntity data) => await _dbSet.AddAsync(data);

        public async Task Put(TEntity data)
        {
            _dbSet.Attach(data);
            _context.Entry(data).State = EntityState.Modified;
        }

        public async Task Delete(TEntity data)
        {
            _dbSet.Attach(data);
            _context.Entry(data).State = EntityState.Deleted;
        }

        public async Task Save() => await _context.SaveChangesAsync();
    }
}
