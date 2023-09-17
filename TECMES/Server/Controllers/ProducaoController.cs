using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using TECMES.Server.Repositories.Repository;
using TECMES.Shared.Models;

namespace TECMES.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProducaoController : Controller
    {
        private readonly IRepository<Producao> _producaoRepository;
        public ProducaoController(IRepository<Producao> producaoRepository)
        {
            _producaoRepository = producaoRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Producao>?> ObterTodos()
        {
            var includes = new List<Expression<Func<Producao, object?>>>
            {
                producao => producao.OrdemProducao,
            };

            return await _producaoRepository.ObterTodos(includes.ToArray());
        }

        [HttpGet("{id:guid}")]
        public async Task<Producao?> ObterPorId(Guid id)
        {
            return await _producaoRepository.ObterPorId(id);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] Producao producao)
        {
            await _producaoRepository.Adicionar(producao);
            return Ok(producao);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] Producao producao)
        {
            if (id != producao.Id)
                return BadRequest("Id informado não é o mesmo da produção passada");

            await _producaoRepository.Atualizar(producao);
            return Ok(producao);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var producao = await _producaoRepository.ObterPorId(id);
            _producaoRepository.EncerrarTracker();

            if (producao == null)
                return NotFound("Produção não existe");

            await _producaoRepository.Remover(id);
            return NoContent();
        }
    }
}
