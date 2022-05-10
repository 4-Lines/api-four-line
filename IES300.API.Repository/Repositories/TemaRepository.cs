﻿using IES300.API.Domain.DTOs.Tema;
using IES300.API.Domain.Entities;
using IES300.API.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IES300.API.Repository.Repositories
{
    public class TemaRepository : RepositoryBase<Tema>, ITemaRepository
    {
        public TemaRepository(ApiDbContext context) : base(context) { }

        public Tema ObterTemaPorIdComPatrocinador(int id)
        {
            return _dbSet.Include(x => x.Patrocinador).FirstOrDefault(x => x.Id == id);
        }

        public List<Tema> ObterTodosTemasComPatrocinador()
        {
            return _dbSet.Include(x => x.Patrocinador).ToList();
        }
    }
}
