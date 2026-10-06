using System;
using System.Collections.Generic;
using System.Text;

// Entities : representa o dado dentro do sisetema
namespace ArcadeScore.Domain.Entities
{
    public class Score
    {
        public Guid Id { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public int Points { get; set; }
        public DateTime PlayedAt { get; set; }

    }
}
