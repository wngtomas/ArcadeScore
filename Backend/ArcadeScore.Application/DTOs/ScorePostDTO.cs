using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

// DTOs: Data Transfer Object
// Descreve o formato dos dados que entram e saem da API
namespace ArcadeScore.Application.DTOs
{
    // Em requisições do tipo POST (envio de dados), temos que definir regras (Data Annotations) para os dados que estão sendo enviados
    // Caso o dado enviado não cumpra a regra,  a API retorna status 400 - Bad Request junto da sua respectiva ErrorMessage
    public class ScorePostDTO
    {
        [Required(ErrorMessage = "O nome do jogador é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome do jogador deve ter no máximo 50 caracteres.")]
        public string PlayerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "A pontuação é obrigatória.")]
        [Range(0, int.MaxValue, ErrorMessage = "A pontuação não é negativa.")]
        public int? Points { get; set; }

        [Required(ErrorMessage = "A data da partida é obrigatória.")]
        public DateTime? PlayedAt { get; set; }

        // Diferença de int e int?
        // Com int: se o Angular esquecer de enviar o campo, vira 0, Required não barra
        // Com int? : se o Angular esquecer de enviar o campo, vira null, o Required barra

    }
}
