using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_With_EF.DAL;
using API_With_EF.Models;
using System.Net;

namespace API_With_EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardGameController : ControllerBase
    {
        BoardGameRepository repo = new BoardGameRepository();

        [HttpPost("add")]
        public BoardGame AddBoardGame(string title, string description, int year, int count)
        {
            BoardGame newBoardGame = new BoardGame
            {
                Title = title,
                Description = description,
                YearPublished = year,
                RecommendedPlayerCount = count
            };
            return repo.AddBoardGame(newBoardGame);
        }

        [HttpGet()]
        public List<BoardGame> GetAll()
        {
            return repo.GetAllGames();
        }

        [HttpGet("{id}")]
        public BoardGame GetById(int id)
        {
            return repo.FindById(id);
        }

        [HttpPost("delete/{id}")]
        public HttpResponseMessage DeleteById(int id)
        {
            try
            {
                if (repo.DeleteById(id) == true)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable);
            }
        }

        [HttpPost("update")]
        public HttpResponseMessage  UpdateBoardGame(int id, string title, string description, int year, int count)
        {
            BoardGame gameToUpdate = new BoardGame
            {
                Id = id,
                Title = title,
                Description = description,
                YearPublished = year,
                RecommendedPlayerCount = count
            };

            try
            {
                if (repo.UpdateBoardGame(gameToUpdate) == true)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable);
            }
        }

    }

    
}
