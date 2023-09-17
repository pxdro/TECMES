using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TECMES.Server.Repositories.Repository;
using TECMES.Shared.Models;

namespace TECMES.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : Controller
    {
        private readonly IRepository<Produto> _produtoRepository;
        public ProdutoController(IRepository<Produto> produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Produto>?> ObterTodos()
        {
            return await _produtoRepository.ObterTodos();
        }

        [HttpGet("{id:guid}")]
        public async Task<Produto?> ObterPorId(Guid id)
        {
            return await _produtoRepository.ObterPorId(id);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] Produto produto)
        {
            await _produtoRepository.Adicionar(produto);
            return Ok(produto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] Produto produto)
        {
            if (id != produto.Id)
                return BadRequest("Id informado não é o mesmo do produto passado");

            await _produtoRepository.Atualizar(produto);
            return Ok(produto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var produto = await _produtoRepository.ObterPorId(id);
            _produtoRepository.EncerrarTracker();

            if (produto == null)
                return NotFound("Produto não existe");

            await _produtoRepository.Remover(id);
            return NoContent();
        }
    }
}
