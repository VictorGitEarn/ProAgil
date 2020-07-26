using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;
using ProAgil.WebAPI.Dtos;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository _repo;
        private readonly IMapper _mapper;
        public EventoController(IProAgilRepository repo, IMapper mapper)
        {
            this._mapper = mapper;
            this._repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _repo.GetAllEventoAsync(true);

                var results = _mapper.Map<IEnumerable<EventoDTO>>(eventos);
                
                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro no Banco de dados. {ex.Message}");
            }
        }


        [HttpGet("{eventoId}")]
        public async Task<IActionResult> Get(int eventoId)
        {
            try
            {
                var evento = await _repo.GetAllEventosById(eventoId, true);

                var results = _mapper.Map<EventoDTO>(evento);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no Banco de dados.");
            }
        }

        [HttpGet("GetByTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                var eventos = await _repo.GetAllEventosByTema(tema, true);
                
                var results = _mapper.Map<EventoDTO[]>(eventos);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no Banco de dados.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDTO model)
        {
            try
            {
                var evento = _mapper.Map<Evento>(model);

                _repo.Add(evento);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.Id}", _mapper.Map<EventoDTO>(evento));
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro no Banco de dados. {ex.Message}");
            }

            return BadRequest("Deu ruim");
        }

        [HttpPut("{eventoId}")]
        public async Task<IActionResult> Put(int eventoId, EventoDTO model)
        {
            try
            {
                var evento = await _repo.GetAllEventosById(eventoId);

                if (evento == null) return NotFound();

                _mapper.Map(model, evento);

                _repo.Update(model);

                if (await _repo.SaveChangesAsync())
                    return Created($"/api/evento/{model.Id}", _mapper.Map<EventoDTO>(evento));
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no Banco de dados.");
            }

            return BadRequest("Deu ruim");
        }

        [HttpDelete("{eventoId}")]
        public async Task<IActionResult> Delete(int eventoId)
        {
            try
            {
                var evento = await _repo.GetAllEventosById(eventoId);

                if (evento == null) return NotFound();

                _repo.Delete(evento);

                if (await _repo.SaveChangesAsync())
                    return Ok();
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no Banco de dados.");
            }

            return BadRequest("Deu ruim");
        }
    }
}