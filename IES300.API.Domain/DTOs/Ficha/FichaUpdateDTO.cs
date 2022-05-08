using System.ComponentModel.DataAnnotations;

namespace IES300.API.Domain.DTOs.Ficha
{
    public class FichaUpdateDTO
    {
        [Required(ErrorMessage = "Campo id obrigatório")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo nome obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo urlFicha obrigatório")]
        public string UrlFicha { get; set; }

        [Required(ErrorMessage = "Campo idTema obrigatório")]
        public int IdTema { get; set; }
    }
}
