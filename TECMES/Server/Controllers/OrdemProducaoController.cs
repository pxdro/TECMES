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
    public class OrdemProducaoController : Controller
    {
        private readonly IRepository<OrdemProducao> _ordemProducaoRepository;
        public OrdemProducaoController(IRepository<OrdemProducao> ordemProducaoRepository)
        {
            _ordemProducaoRepository = ordemProducaoRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<OrdemProducao>?> ObterTodos()
        {
            var includes = new List<Expression<Func<OrdemProducao, object?>>>
            {
                ordemProducao => ordemProducao.Produto,
            };

            return await _ordemProducaoRepository.ObterTodos(includes.ToArray());
        }

        [HttpGet("{id:guid}")]
        public async Task<OrdemProducao?> ObterPorId(Guid id)
        {
            return await _ordemProducaoRepository.ObterPorId(id);
        }

        [Authorize(Roles = "OrdemProducaoAtivo")]
        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] OrdemProducao ordemProducao)
        {
            await _ordemProducaoRepository.Adicionar(ordemProducao);
            return Ok(ordemProducao);
        }

        [Authorize(Roles = "OrdemProducaoAtivo")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] OrdemProducao ordemProducao)
        {
            if (id != ordemProducao.Id)
                return BadRequest("Id informado não é o mesmo da ordem de produção passada");

            await _ordemProducaoRepository.Atualizar(ordemProducao);
            return Ok(ordemProducao);
        }

        [Authorize(Roles = "OrdemProducaoAtivo")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ordemProducao = await _ordemProducaoRepository.ObterPorId(id);
            _ordemProducaoRepository.EncerrarTracker();

            if (ordemProducao == null)
                return NotFound("Ordem de produção não existe");

            await _ordemProducaoRepository.Remover(id);
            return NoContent();
        }
    }
}
