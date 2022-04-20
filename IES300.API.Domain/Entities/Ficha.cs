using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES300.API.Domain.Entities
{
    public class Ficha : EntityBase
    {
        public Ficha() { }

        public Ficha(int id, string nome, string urlFicha, int idTema) : base(id)
        {
            this.Nome = nome;
            this.UrlFicha = urlFicha;
            this.IdTema = idTema;
        }

        public string Nome { get; set; }

        public string UrlFicha { get; set; }

        public int IdTema { get; set; }

        public bool Ativado { get; set; }


        public virtual Tema Tema { get; set; }
    }
}
