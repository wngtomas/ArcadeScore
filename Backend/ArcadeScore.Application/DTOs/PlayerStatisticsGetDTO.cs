using System;
using System.Collections.Generic;
using System.Text;

// DTOs: Data Transfer Object
// Descreve o formato dos dados que entram e saem da API
namespace ArcadeScore.Application.DTOs
{
    public class PlayerStatisticsGetDTO
    {
        public string PlayerName { get; set; } = string.Empty;
        public int GamesPlayed { get; set; }
        public double AveragePoints { get; set; }
        public int HighestScore { get; set; }
        public int LowestScore { get; set; }
        public int RecordBrokenCount { get; set; }
        public int PlayingTimeInDays { get; set; }
    }
}
