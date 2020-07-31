using System.Threading.Tasks;
using CursoAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CursoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        public readonly ICursoAPI _repository;

        public EventosController(ICursoAPI repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repository.GetAllEventosAsync(true);
                return Ok(results);

            } catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no banco de dados.");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _repository.GetEventoAsyncById(id, true);
                return Ok(result);

            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no banco de dados.");
            }
        }

        [HttpGet("palestrantes")]
        public async Task<IActionResult> Get([FromQuery(Name ="nome")] string nome)
        {

            try
            {
                var results = await _repository.GetAllPalestrantesAsyncByName(nome,false);
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no banco de dados");
            }
        }


        [HttpGet("palestrantes/{id}")]
        public async Task<IActionResult> GetPalestrantesById(int id)
        {

            try
            {
                var result = await _repository.GetPalestranteAsyncById(id,false);
                return Ok(result);

            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no banco de dados.");
            }

        }
    }
}
