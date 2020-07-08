using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ProAgil.Repository;
using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class PalestranteController : ControllerBase
    {
        private readonly IProAgilRepository _repo;

        public PalestranteController(IProAgilRepository repo)
        {
            this._repo = repo;
        }

        [HttpGet("palestranteId")]
        public async Task<IActionResult> Get(int palestranteId)
        {
            try
            {
                var result = await _repo.GetPalestranteAsync(palestranteId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro do banco de dados");
            }
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            try
            {
                var results = await _repo.GetAllPalestrantesAsyncByName(name);

                return Ok(results);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro no Bando de dados");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Palestrante model)
        {
            try
            {
                _repo.Add(model);

                if (await _repo.SaveChangesAsync())
                    return Created($"api/palestrante/{model.Id}", model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Deu ruim no Banco de dados");
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int palestranteId, Palestrante model)
        {
            try
            {
                var palestrante = _repo.GetPalestranteAsync(palestranteId);

                if (palestrante == null) return NotFound();

                _repo.Update(model);

                if (await _repo.SaveChangesAsync())
                    return Created($"api/palestrante/{model.Id}", model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro no banco de dados");
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int palestranteId)
        {
            try
            {
                var palestrante = _repo.GetPalestranteAsync(palestranteId);

                if (palestrante == null) return NotFound();
                
                _repo.Delete(palestrante);

                if (await _repo.SaveChangesAsync())
                    return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro no banco de dados");
            }

            return BadRequest();
        }
    }
}