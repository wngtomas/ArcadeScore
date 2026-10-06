using ArcadeScore.Application.DTOs;
using ArcadeScore.Application.Interfaces;
using ArcadeScore.Domain.Entities;
using ArcadeScore.Domain.Interfaces;

using System;
using System.Collections.Generic;
using System.Text;

// Implementa as regras de negócio relacionadas às pontuações dos jogadores
namespace ArcadeScore.Application.Services
{
    public class ScoreService : IScoreService
    {
        // Invés de usar o banco de dados, como fizemos no repositório
        // Aq vamos usar justamente o repositório => IScoreRepository
        private readonly IScoreRepository _scoreRepository;

        // Método construtor
        // Fazemos a injeção de dependência e atribuimos o valor do IScoreRepository à _scoreRepository
        public ScoreService(IScoreRepository scoreRepository)
        {
            _scoreRepository = scoreRepository;
        }

        // Recebe o objeto do tipo ScorePostDTO
        public void RegisterScore(ScorePostDTO dto)
        {
            // Se alguns dos campos for null, gera o erro: Pontuação e data da partida são obrigatórias.
            if (dto.Points == null || dto.PlayedAt == null)
            {
                throw new ArgumentException("Pontuação e data da partida são obrigatórias.");
            }

            // Cria um objeto score
            Score score = new Score();
            // Preenche o id, gerando esse id por meio de Guid.NewGuid();
            score.Id = Guid.NewGuid();
            // Preenche o nome do jogador
            score.PlayerName = dto.PlayerName.Trim();
            // Preenche o número de pontos
            score.Points = dto.Points.Value;
            // Preenche a data em que a partida foi jogada
            score.PlayedAt = dto.PlayedAt.Value;

            // Chama a função Add do repositório passando o item score
            _scoreRepository.Add(score);

        }

        public List<ScoreGetDTO> GetRanking()
        {
            // Cria uma lista com todas as pontuações
            List<Score> allScores = _scoreRepository.GetAll();

            List<Score> topScores = allScores
                .OrderByDescending(score => score.Points) // Ordena as pontuações da maior para a menor
                .Take(10) // Pega os 10 primeiros
                .ToList(); // Transforma o resultado em lista

            // Cria uma lista do tipo ScoreGetDTO, para exibir o ranking
            List<ScoreGetDTO> ranking = new List<ScoreGetDTO>();
            int position = 1;

            // Percorre as 10 melhores pontuações
            foreach(Score score in topScores)
            {
                // Para cada score em topScores, vamos criar um item para guardar os seu dados e dps adcionar esse item ao ranking
                ScoreGetDTO item = new ScoreGetDTO();
                item.Position = position; 
                item.PlayerName = score.PlayerName;
                item.Points = score.Points;

                ranking.Add(item); 
                position++; // Depois de adicionar a score, temos que aumentar uma posição no ranking

            }

            return ranking;
        }

        public PlayerStatisticsGetDTO? GetPlayerStatistics(string playerName)
        {
            // Cria uma lista playerScores com todoas pontuações de determinado usuário
            List<Score> playerScores = _scoreRepository.GetByPlayerName(playerName);

            // Se o jogador n tiver nenhuma pontuação, retorna null
            if(playerScores.Count == 0)
            {
                return null;
            }

            // Cria uma lista scoresByDate, gerada a partir de da lista playerScores
            List<Score> scoresByDate = playerScores
                .OrderBy(score => score.PlayedAt) // Ordena a lista, usando PlayedAt como ordenação, OrderBy usa crescente como padrão
                .ToList(); // Transforma o resultado em lista

            int recordBrokenCount = 0; // Cria um contador de recordes
            int currentRecord = scoresByDate[0].Points; // Estabelece o recode inicial com o valor do primeiro item de scoresByDate

            // Para cada item da lista scoresByDate
            for(int i = 1;  i < scoresByDate.Count; i++)
            {
                // Se a pontuação desse item for maior que o recorde atual
                if(scoresByDate[i].Points > currentRecord)
                {
                    recordBrokenCount++; // Aumentamos o contador de recordes
                    currentRecord = scoresByDate[i].Points; // Atualizamos o valor do currentRecord
                }
            }

            // Pegamos a data do primeiro jogo, acessando o PlayedAt do primeiro item da lista scoresByDate
            DateTime firstGame = scoresByDate[0].PlayedAt;
            // Pegamos a data do último jogo, acessando o PlayedAt do último item da lista scoresByDate
            DateTime lastGame = scoresByDate[scoresByDate.Count - 1].PlayedAt;

            // Cria um objeto statistics
            PlayerStatisticsGetDTO statistics = new PlayerStatisticsGetDTO();

            // Preenche o nome
            statistics.PlayerName = scoresByDate[0].PlayerName;

            // Preenche o número de jogos
            statistics.GamesPlayed = playerScores.Count;

            // Preenche a média de pontos
            // Average(score => score.Points) => gera a média de todas as pontuações
            // Math.Round(playerScores.Average(score => score.Points), 2) => arredonda a média em duas casas decimais
            statistics.AveragePoints = Math.Round(playerScores.Average(score => score.Points), 2);

            // Preenche a maior pontuação, .Max() gera o maior valor

            statistics.HighestScore = playerScores.Max(score => score.Points);
            // Preenche a menor pontuação, .Min() gera o menor valor
            statistics.LowestScore = playerScores.Min(score => score.Points);

            // Preenche o contador de recordes batidos
            statistics.RecordBrokenCount = recordBrokenCount;

            // Preenche o tempo de jogo, por meio de uma subtração entre a data do último e do primeiro jogo
            statistics.PlayingTimeInDays = (lastGame - firstGame).Days;

            return statistics;

        }
    }
}
