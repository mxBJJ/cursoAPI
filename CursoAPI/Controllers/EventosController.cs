using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CursoAPI.Domain;
using CursoAPI.Dto;
using CursoAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;

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

                double totalPages = Math.Ceiling(_repository.TotalPages()/5);

                Response.Headers.Add("X-Total-Count", Convert.ToInt64(totalPages).ToString());

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
                var evento = await _repository.GetEventoAsyncById(id, true);
                var result = _mapper.Map<EventoDto>(evento);

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
                var palestrantes = await _repository.GetAllPalestrantesAsyncByName(nome,true);

                var results = _mapper.Map<IEnumerable<PalestranteDto>>(palestrantes);
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
                var evento = await _repository.GetPalestranteAsyncById(id,false);
                var result = _mapper.Map<EventoDto>(evento);

                return Ok(result);

            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no banco de dados.");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {

            try
            {
                _repository.Add(model);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/evento/{model.Id}", model);
                }
            }

            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no banco de dados.");
            }

            return BadRequest();
        
        }

        [HttpPut("{EventoId}")]
        public async Task<IActionResult> Put(int EventoId, Evento model)
        {
            try
            {
                var evento = await _repository.GetEventoAsyncById(EventoId, false);

                if (evento == null) return NotFound();

                _repository.Update(model);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/evento/{model.Id}", model);
                }
            }

            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no banco de dados.");
            }

            return BadRequest();
        }

        [HttpDelete("{EventoId}")]
        public async Task<IActionResult> Delete(int EventoId, Evento model)
        {
            try
            {
                var evento = await _repository.GetEventoAsyncById(EventoId, false);

                if (evento == null) return NotFound();

                _repository.Delete(evento);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
            }

            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no banco de dados.");
            }

            return BadRequest();
        }
    }
}
