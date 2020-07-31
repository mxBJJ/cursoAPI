using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CursoAPI.Dto;
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
        public readonly IMapper _mapper;

        public EventosController(ICursoAPI repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name ="page")] int pageNumber = 1)
        {
            try
            {
                var eventos = await _repository.GetAllEventosAsync(pageNumber, true);

                var results = _mapper.Map<IEnumerable<EventoDto>>(eventos);
                return Ok(results);

            } catch (System.Exception ex)
            {
                return this
                    .StatusCode(StatusCodes.Status500InternalServerError, $"Erro no banco de dados.{ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
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
