using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TECMES.Server.Repositories.Repository;
using TECMES.Shared.Models;

namespace TECMES.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : Controller
    {
        private readonly IRepository<Pedido> _pedidoRepository;
        private readonly IRepository<Producao> _producaoRepository;
        private readonly IRepository<OrdemProducao> _ordemProducaoRepository;
        private readonly IRepository<Produto> _produtoRepository;

        public PedidoController(IRepository<Pedido> pedidoRepository,
                                IRepository<Producao> producaoRepository,
                                IRepository<OrdemProducao> ordemProducaoRepository,
                                IRepository<Produto> produtoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _producaoRepository = producaoRepository;
            _ordemProducaoRepository = ordemProducaoRepository;
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Pedido>?> ObterTodos()
        {
            return await _pedidoRepository.ObterTodos();
        }

        [HttpGet("{id:guid}")]
        public async Task<Pedido?> ObterPorId(Guid id)
        {
            return await _pedidoRepository.ObterPorId(id);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] Pedido pedido)
        {
            Producao? producao = new();

            if (pedido.ProducaoId != Guid.Empty && pedido.ProducaoId != null)
                producao = await _producaoRepository.ObterPorId((Guid)pedido.ProducaoId);
            if (producao != null && producao.OrdemProducaoId != Guid.Empty && producao.OrdemProducaoId != null)
                producao.OrdemProducao = await _ordemProducaoRepository.ObterPorId((Guid)producao.OrdemProducaoId);
            if (producao != null && producao.OrdemProducao != null && producao.OrdemProducao.ProdutoId != Guid.Empty && producao.OrdemProducao.ProdutoId != null)
                producao.OrdemProducao.Produto = await _produtoRepository.ObterPorId((Guid)producao.OrdemProducao.ProdutoId);

            if (producao != null && producao.OrdemProducao != null && producao.OrdemProducao.Produto != null)
                pedido.Produto = producao.OrdemProducao.Produto.Nome;
            if (producao != null && producao.OrdemProducao != null && producao.Produzido == producao.OrdemProducao.Quantidade)
                pedido.Quantidade = producao.Produzido;

            _producaoRepository.EncerrarTracker();
            _ordemProducaoRepository.EncerrarTracker();
            _produtoRepository.EncerrarTracker();

            if (producao != null)
                await _producaoRepository.Remover(producao.Id);
            if (producao != null && producao.OrdemProducao != null)
                await _ordemProducaoRepository.Remover(producao.OrdemProducao.Id);

            await _pedidoRepository.Adicionar(pedido);
            return Ok(pedido);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] Pedido pedido)
        {
            if (id != pedido.Id)
                return BadRequest("Id informado não é o mesmo do pedido passado");

            await _pedidoRepository.Atualizar(pedido);
            return Ok(pedido);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var pedido = await _pedidoRepository.ObterPorId(id);
            _pedidoRepository.EncerrarTracker();

            if (pedido == null)
                return NotFound("Pedido não existe");

            await _pedidoRepository.Remover(id);
            return NoContent();
        }
    }
}
