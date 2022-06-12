using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesafioViceri.Models;
using DesafioViceri.Repositories;
using DesafioViceri.Services;

namespace DesafioViceri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly Repository _context;

        public UsuarioController(Repository context)
        {
            _context = context;
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioModel>>> GetUsuarios()
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'Repository.Usuarios'  is null.");
            }
            if (_context.Usuarios.Count() == 0)
            {
                return Ok("Sem Usuários Cadastrados");
            }
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioModel>> GetUsuarioModel(int id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'Repository.Usuarios'  is null.");
            }
            var usuarioModel = await _context.Usuarios.FindAsync(id);

            if (usuarioModel == null)
            {
                return BadRequest("Usuário Não Encontrado");
            }

            return usuarioModel;
        }

        // PUT: api/Usuario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioModel(int id, UsuarioModel usuarioModel)
        {
            if (id != usuarioModel.UsuarioId)
            {
                return BadRequest("UsuarioId Inválido");
            }

            var usuarioExiste = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.UsuarioId == id);

            if (usuarioExiste == null)
            {
                return BadRequest("Usuário Não Encontrado");
            }

            var usuarios = _context.Usuarios.AsNoTracking().ToList();

            if (usuarios.Count() == 0)
            {
                return BadRequest("Sem Usuários Cadastrados");
            }

            foreach (var usuario in usuarios)
            {
                if (usuarioModel.Email == usuario.Email && usuarioModel.CPF == usuario.CPF)
                {
                    return BadRequest("Erro ao Atualizar Usuario: \nEmail e CPF Já Cadastrado");
                }
                if (usuarioModel.Email == usuario.Email)
                {
                    return BadRequest("Erro ao Atualizar Usuario: \nEmail Já Cadastrado");
                }
                if (usuarioModel.CPF == usuario.CPF)
                {
                    return BadRequest("Erro ao Atualizar Usuario: \nCPF Já Cadastrado");
                }
            }

            _context.Entry(usuarioModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Usuário Atualizado com Sucesso",
                data = usuarioModel
            });
        }

        // POST: api/Usuario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> PostUsuarioModel(UsuarioModel usuarioModel)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'Repository.Usuarios'  is null.");
            }
            var usuarios = _context.Usuarios.ToList();

            foreach (var usuario in usuarios)
            {
                if (usuarioModel.Email == usuario.Email && usuarioModel.CPF == usuario.CPF)
                {
                    return BadRequest("Erro ao cadastrar Usuario: \nEmail e CPF Já Cadastrado");
                }
                if (usuarioModel.Email == usuario.Email)
                {
                    return BadRequest("Erro ao cadastrar Usuario: \nEmail Já Cadastrado");
                }
                if (usuarioModel.CPF == usuario.CPF)
                {
                    return BadRequest("Erro ao cadastrar Usuario: \nCPF Já Cadastrado");
                }
            }

            usuarioModel.Senha = CriptografiaService.ConvertToSha256Hash(usuarioModel.Senha);
            _context.Usuarios.Add(usuarioModel);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Usuário Cadastrado com Sucesso",
                data = usuarioModel
            });
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioModel(int id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'Repository.Usuarios'  is null.");
            }
            var usuarioModel = await _context.Usuarios.FindAsync(id);
            if (usuarioModel == null)
            {
                return BadRequest("Usuário Não Encontrado");
            }

            _context.Usuarios.Remove(usuarioModel);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Usuário Deletado com Sucesso"
            });
        }
    }
}
