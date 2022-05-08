﻿using System.ComponentModel.DataAnnotations;

namespace IES300.API.Domain.DTOs.Tema
{
    public class TemaUpdateDTO
    {
        [Required(ErrorMessage = "Campo Id é obrigatório")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo UrlTabuleiro é obrigatório")]
        public string UrlTabuleiro { get; set; }

        [Required(ErrorMessage = "Campo IdPatrocinador é obrigatório")]
        public int IdPatrocinador { get; set; }
    }
}
