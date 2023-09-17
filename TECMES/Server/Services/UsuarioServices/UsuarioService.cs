using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TECMES.Server.Encrypt;
using TECMES.Server.Repositories.Repository;
using TECMES.Shared.Enums;
using TECMES.Shared.Models;

namespace TECMES.Server.Services.UsuarioServices
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IRepository<Usuario> _usuarioRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IConfiguration _configuration;

        public UsuarioService(IRepository<Usuario> usuarioRepository, IPasswordHasher passwordHasher, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        public async Task<List<Usuario>?> ObterTodos()
        {
            return await _usuarioRepository.ObterTodos();
        }

        public async Task<Usuario?> ObterPorId(Guid id)
        {
            return await _usuarioRepository.ObterPorId(id);
        }

        public async Task<Usuario?> ObterPorEmail(string email)
        {
            return (await _usuarioRepository.Buscar(usuario => usuario.Email == email))?.FirstOrDefault();
        }

        public async Task<Usuario?> Adicionar(Usuario usuario)
        {
            usuario.Senha = _passwordHasher.HashPassword(usuario.Senha);
            usuario.OrdemProducao = EnumStatus.Inativo; // Novos usuário sempre bloqueados para acessar Ordens de Produção
            await _usuarioRepository.Adicionar(usuario);
            return usuario;
        }

        public async Task<Usuario?> Atualizar(Usuario usuario)
        {
            await _usuarioRepository.Atualizar(usuario);
            return usuario;
        }

        public async Task Remover(Guid id)
        {
            await _usuarioRepository.Remover(id);
        }

        public async Task<Usuario?> Logar(Usuario usuarioInserido)
        {
            Usuario? usuario = (await _usuarioRepository.Buscar(usu => usu.Email == usuarioInserido.Email))?.FirstOrDefault();

            // Usuário não existe ou usuário inativo ou senha incorreta
            if (usuario == null ||
                usuario.Status == EnumStatus.Inativo ||
                !_passwordHasher.VerifyPassword(usuarioInserido.Senha, usuario.Senha))
                return null;

            return usuario;
        }

        public string CriarJWTToken(Usuario usuario)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Role, "OrdemProducao" + usuario.OrdemProducao.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:JWTToken").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public void Dispose()
        {
            _usuarioRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
