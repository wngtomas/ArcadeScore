using ArcadeScore.Application.DTOs;

using System;
using System.Collections.Generic;
using System.Text;

// Aq é onde definimos os métodos que ficaram no ScoreService
// Semelhante ao IScoreRepository, só que temos uma diferença
// No repositório lidamos com a entidade Score, já aqui vamos lidar com os DTOs, pois os serviços são chamados lá nas APIs,
// e não podemos expor os campos da entidade
namespace ArcadeScore.Application.Interfaces
{

    public interface IScoreService
    {
        void RegisterScore(ScorePostDTO dto);
        List<ScoreGetDTO> GetRanking();
        PlayerStatisticsGetDTO? GetPlayerStatistics(string playerName);

    }
}
