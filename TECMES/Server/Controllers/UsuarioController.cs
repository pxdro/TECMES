using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TECMES.Server.Services.UsuarioServices;
using TECMES.Shared.Models;

namespace TECMES.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> ObterTodos()
        {
            var usuarios = await _usuarioService.ObterTodos();

            if (usuarios == null)
                return NotFound();

            foreach (var usuario in usuarios)
                usuario.Senha = string.Empty;

            return Ok(usuarios);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Usuario?>> ObterPorId([FromRoute] Guid id)
        {
            var usuario = await _usuarioService.ObterPorId(id);

            if (usuario != null)
            {
                usuario.Senha = string.Empty;
                return Ok(usuario);
            }

            return NotFound();
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<Usuario?>> ObterPorEmail([FromRoute] string email)
        {
            var usuario = await _usuarioService.ObterPorEmail(email);

            if (usuario != null)
            {
                usuario.Senha = string.Empty;
                return Ok(usuario);
            }

            return NotFound();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Usuario>> Adicionar([FromBody] Usuario usuario)
        {
            var usu = await _usuarioService.Adicionar(usuario);

            if (usu == null)
                return BadRequest();

            usuario.Senha = string.Empty;
            return Ok(usuario);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Usuario>> Atualizar([FromBody] Usuario usuario, [FromRoute] Guid id)
        {
            if (usuario.Id != id)
                return BadRequest();

            var usu = await _usuarioService.Atualizar(usuario);

            if (usu == null)
                return BadRequest();

            usuario.Senha = string.Empty;
            return Ok(usuario);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Usuario>> Excluir([FromRoute] Guid id)
        {
            await _usuarioService.Remover(id);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<Usuario>> Login([FromBody] Usuario usuario)
        {
            var usu = await _usuarioService.Logar(usuario);

            if (usu == null)
            {
                usuario.Error = "Usuário não cadastrado no sistema, inativo ou senha incorreta.";
                return BadRequest(usuario);
            }

            usuario.Nome = usu.Nome;
            usuario.OrdemProducao = usu.OrdemProducao;
            usuario.Senha = string.Empty;
            usuario.Jwt = _usuarioService.CriarJWTToken(usuario);

            return Ok(usuario);
        }
    }
}
