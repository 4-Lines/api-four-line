using System.ComponentModel.DataAnnotations;

namespace IES300.API.Domain.DTOs.Tema
{
    public class TemaInputDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }

        [Required]
        public string UrlTabuleiro { get; set; }

        [Required]
        public int IdPatrocinador { get; set; }

        [Required]
        public bool Ativado { get; set; }
    }
}
