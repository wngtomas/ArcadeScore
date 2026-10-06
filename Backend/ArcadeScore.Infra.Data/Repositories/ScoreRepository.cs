using ArcadeScore.Domain.Entities;
using ArcadeScore.Domain.Interfaces;

using System;
using System.Collections.Generic;
using System.Text;


// Repositories : métodos responsáveis pelo acesso de dados
namespace ArcadeScore.Infra.Data.Repositories
{
    public class ScoreRepository : IScoreRepository
    {
        // Banco de dados em memória : lista de Scores
        // Quando a API for desligada, os dados se perdem
        private readonly List<Score> _scores = new List<Score>();

        public void Add(Score score)
        {
            // Adiciona a score ao final do nosso "Banco de dados"
            _scores.Add(score);
        }

        public List<Score> GetAll()
        {
            // Retorna uma cópia da lista de Scores
            return new List<Score>(_scores);
        }

        public List<Score> GetByPlayerName(string playerName)
        {
            // Cria uma lista de resultados vazia
            List<Score> result = new List<Score>();

            // Percorre todas as score do nosso "Banco de dados"
            foreach(Score score in _scores)
            {
                // Se o nome do jogador da score percorrida for igual ao nome do jogador passado como parâmetro da função
                if(string.Equals(score.PlayerName, playerName, StringComparison.OrdinalIgnoreCase))
                {
                    // Adicionamos essa pontuação à nossa lista de resultados
                    result.Add(score);
                }
            }

            return result;
        }

    }
}
