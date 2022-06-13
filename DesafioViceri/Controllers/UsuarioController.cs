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

            var usuarioExiste = await _context.Usuarios.AsNoTracking().AnyAsync(x => x.UsuarioId == id);

            if (!usuarioExiste)
            {
                return BadRequest("Usuário Não Encontrado");
            }

            var usuarios = _context.Usuarios.AsNoTracking().ToList();

            if (usuarios.Count() == 0)
            {
                return BadRequest("Sem Usuários Cadastrados");
            }

            var CpfEmailExistente = await _context.Usuarios.AsNoTracking().AnyAsync(x => x.CPF == usuarioModel.CPF || x.Email == usuarioModel.Email);

            if (CpfEmailExistente)
            {
                return BadRequest("Erro ao cadastrar Usuario: \nEmail ou CPF Já Cadastrado");
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
        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> PostUsuarioModel(UsuarioModel usuarioModel)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'Repository.Usuarios'  is null.");
            }

            var CpfEmailExistente = await _context.Usuarios.AsNoTracking().AnyAsync(x => x.CPF == usuarioModel.CPF || x.Email == usuarioModel.Email);

            if (CpfEmailExistente)
            {
                return BadRequest("Erro ao cadastrar Usuario: \nEmail ou CPF Já Cadastrado");
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
