using IES300.API.Domain.Entities;
using IES300.API.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES300.API.Repository.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected readonly ApiDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public RepositoryBase(ApiDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public T ObterPorId(int id, bool asNoTracking = true)
        {
            throw new NotImplementedException();
        }

        public IList<T> ObterTodos()
        {
            throw new NotImplementedException();
        }

        public void Inserir(T entity)
        {
            throw new NotImplementedException();
        }

        public void Alterar(T entity)
        {
            throw new NotImplementedException();
        }

        public void Deletar(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
