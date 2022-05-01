using System.ComponentModel.DataAnnotations;

namespace IES300.API.Domain.DTOs.Patrocinador
{
    public class PatrocinadorInputDTO
    {
        [Required(ErrorMessage = "Campo nome obruigatório")]
        public string Nome { get; set; }

        [Required]
        public string Website { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Celular { get; set; }

        [Required]
        public string UrlLogo { get; set; }
    }
}
