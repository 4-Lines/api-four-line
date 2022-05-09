﻿using IES300.API.Domain.Entities;
using IES300.API.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IES300.API.Repository.Repositories
{
    public class FichaRepository : RepositoryBase<Ficha>, IFichaRepository
    {
        public FichaRepository(ApiDbContext context) : base(context) { }

        public List<Ficha> ObterTodasFichasComTema()
        {
            return _dbSet.Include(x => x.Tema).ToList();
        }
    }
}
