using System;
using System.Collections.Generic;
using System.Text;

// DTOs: Data Transfer Object
// Descreve o formato dos dados que entram e saem da API
namespace ArcadeScore.Application.DTOs
{
    public class ScoreGetDTO
    {
        public int Position { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public int Points { get; set; }
    }
}
