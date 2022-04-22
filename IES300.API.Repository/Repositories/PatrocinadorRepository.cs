using IES300.API.Domain.Entities;
using IES300.API.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES300.API.Repository.Repositories
{
    public class PatrocinadorRepository : RepositoryBase<Patrocinador>, IPatrocinadorRepository
    {
        public PatrocinadorRepository(ApiDbContext context) : base(context) { }
    }
}
