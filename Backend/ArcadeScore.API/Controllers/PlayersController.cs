using ArcadeScore.Application.DTOs;
using ArcadeScore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArcadeScore.API.Controllers
{
    // Os controllers recebem as requisições, chamam o Service e devolvem a resposta

    [ApiController] // Marca a classe como controller, é ele que ativa as valiações Required/StringLenght, etc na DTO
    // [Route("api/[controller]")] Define o nome da rota, controller é substituído pelo nome da classe, gerando /api/Scores e /api/Players
    [Route("api/players")]
    public class PlayersController : ControllerBase
    {
        private readonly IScoreService _scoreService;

        // Método construtor
        // Fazemos a injeção de dependência e atribuimos o valor do IScoreService à _scoreService
        // Usamos o IScoreService, pois precisamos dele para acessar operações GetStatistics
        public PlayersController(IScoreService scoreService)
        {
            _scoreService = scoreService;
        }


        [HttpGet("{playerName}/statistics")] // Monta: GET /api/players/{playerName}/statistics
        public ActionResult<PlayerStatisticsGetDTO> GetStatistics(string playerName)
        {
            PlayerStatisticsGetDTO? statistics = _scoreService.GetPlayerStatistics(playerName);

            if(statistics == null)
            {
                return NotFound("Nenhuma parida encontrada para este jogador"); // Responde com o código 404("não encontrado")
            }

            return Ok(statistics); // Responde com o código 200 + statistics

        }
    }
}
