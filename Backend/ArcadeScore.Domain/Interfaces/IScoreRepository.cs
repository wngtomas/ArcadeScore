using ArcadeScore.Domain.Entities;

namespace ArcadeScore.Domain.Interfaces;

public interface IScoreRepository
{
    void Add(Score score);
    List<Score> GetAll();
    List<Score> GetByPlayerName(string playerName);

}
