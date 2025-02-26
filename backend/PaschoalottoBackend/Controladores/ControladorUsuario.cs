using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaschoalottoBackend.Modelos;
using PaschoalottoBackend.Servicos;
using System.Threading.Tasks;

namespace PaschoalottoBackend.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class ControladorUsuario : ControllerBase
    {
        private readonly ServicoUsuario _servicoUsuario;
        private readonly ContextoPaschoalotto _contexto;

        public ControladorUsuario(ServicoUsuario servicoUsuario, ContextoPaschoalotto contexto)
        {
            _servicoUsuario = servicoUsuario;
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usuario = await _servicoUsuario.GerarUsuario();
            return Ok(usuario);
        }

        [HttpGet("todos")]
        public async Task<IActionResult> GetTodos()
        {
            var usuarios = await _contexto.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioPorId(int id)
        {
            var usuario = await _contexto.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarUsuario(int id, [FromBody] Usuario usuarioAtualizado)
        {
            var usuario = await _contexto.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Nome = usuarioAtualizado.Nome;
            usuario.Email = usuarioAtualizado.Email;
            usuario.Telefone = usuarioAtualizado.Telefone;
            usuario.Endereco = usuarioAtualizado.Endereco;
            usuario.DataNascimento = usuarioAtualizado.DataNascimento;

            await _contexto.SaveChangesAsync();
            return Ok(usuario);
        }
    }
}