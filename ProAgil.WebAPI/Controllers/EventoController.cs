using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository _repo;
        public EventoController(IProAgilRepository repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repo.GetAllEventoAsync(true);

                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no Banco de dados.");
            }
        }


        [HttpGet("{eventoId}")]
        public async Task<IActionResult> Get(int eventoId)
        {
            try
            {
                var results = await _repo.GetAllEventosById(eventoId, true);

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
                var results = await _repo.GetAllEventosByTema(tema, true);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no Banco de dados.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                _repo.Add(model);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.Id}", model);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no Banco de dados.");
            }

            return BadRequest("Deu ruim");
        }

        [HttpPut]
        public async Task<IActionResult> Put(int eventoId, Evento model)
        {
            try
            {
                var evento = await _repo.GetAllEventosById(eventoId);
                
                if (evento == null) return NotFound();

                _repo.Update(model);

                if (await _repo.SaveChangesAsync())
                    return Created($"/api/evento/{model.Id}", model);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no Banco de dados.");
            }

            return BadRequest("Deu ruim");
        }
        
        [HttpDelete]
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