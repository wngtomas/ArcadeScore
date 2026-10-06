using ArcadeScore.Application.DTOs;
using ArcadeScore.Application.Interfaces;
using ArcadeScore.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArcadeScore.API.Controllers
{
    // Os controllers recebem as requisições, chamam o Service e devolvem a resposta

    [ApiController] // Marca a classe como controller, é ele que ativa as valiações Required/StringLenght, etc na DTO
    // [Route("api/[controller]")] => Define o nome da rota, controller é substituído pelo nome da classe, gerando /api/Scores e /api/Players
    [Route("api/scores")] // Como queremos que a rota da api fique toda minúscula criamos a rota manualmente
    public class ScoresController : ControllerBase // ControllerBase herda as ferramentar de um controller de API, como: Ok(), NotFound()
    {

        private readonly IScoreService _scoreService;

        // Método construtor
        // Fazemos a injeção de dependência e atribuimos o valor do IScoreService à _scoreService
        // Usamos o IScoreService, pois precisamos dele para acessar operações RegisterScore/GetRanking
        public ScoresController(IScoreService scoreService) {
            _scoreService = scoreService;
        }

        [HttpPost] // Monta: POST /api/scores
        public IActionResult Post(ScorePostDTO dto)
        {
            _scoreService.RegisterScore(dto);
            return StatusCode(StatusCodes.Status201Created); // Responde com o código 201 ("Criado com sucesso!)

        }


        [HttpGet("ranking")] // Monta: GET /api/scores/ranking
        public ActionResult<List<ScoreGetDTO>> GetRanking()
        {
            List<ScoreGetDTO> ranking = _scoreService.GetRanking();

            return Ok(ranking); // Responde com o código 200 + Ranking
        }

        // IActionResult => representa uma resposta HTTP de forma mais genérica. Não retorna um objeto em específico.
        // ActionResult<T> => representa uma resposta HTTP que pode carregar um resultado do tipo T.

    }
}
