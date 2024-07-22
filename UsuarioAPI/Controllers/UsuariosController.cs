using Microsoft.AspNetCore.Mvc;
using UsuarioAPI.Models;
using UsuarioAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UsuarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioEntityRepository _entityRepository;
        private readonly UsusarioSpRepository _spRepository;
        private readonly UsuarioAdoRepository _adoRepository;

        public UsuariosController(UsuarioEntityRepository entityRepository, UsusarioSpRepository spRepository, UsuarioAdoRepository adoRepository)
        {
            _entityRepository = entityRepository;
            _spRepository = spRepository;
            _adoRepository = adoRepository;
        }

        #region EntityFramework

        // GET: api/Usuarios/entity
        [HttpGet("entity")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuariosEntity()
        {
            var usuarios = await _entityRepository.GetUsuariosAsync();
            return Ok(usuarios);
        }

        // GET: api/Usuarios/entity/5
        [HttpGet("entity/{id}")]
        public async Task<ActionResult<Usuario>> GetUsuarioEntity(int id)
        {
            var usuario = await _entityRepository.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        // POST: api/Usuarios/entity
        [HttpPost("entity")]
        public async Task<ActionResult<Usuario>> PostUsuarioEntity(Usuario usuario)
        {
            await _entityRepository.AddUsuarioAsync(usuario);
            return CreatedAtAction(nameof(GetUsuarioEntity), new { id = usuario.Id }, usuario);
        }

        // PUT: api/Usuarios/entity/5
        [HttpPut("entity/{id}")]
        public async Task<IActionResult> PutUsuarioEntity(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            try
            {
                await _entityRepository.UpdateUsuarioAsync(usuario);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_entityRepository.UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Usuarios/entity/5
        [HttpDelete("entity/{id}")]
        public async Task<IActionResult> DeleteUsuarioEntity(int id)
        {
            var usuario = await _entityRepository.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            await _entityRepository.DeleteUsuarioAsync(id);
            return NoContent();
        }

        #endregion

        #region StoredProcedure

        // GET: api/Usuarios/sp
        [HttpGet("sp")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuariosSp()
        {
            var usuarios = await _spRepository.GetUsuariosFromSP();
            return Ok(usuarios);
        }

        // GET: api/Usuarios/sp/5
        [HttpGet("sp/{id}")]
        public async Task<ActionResult<Usuario>> GetUsuarioSp(int id)
        {
            var usuario = await _spRepository.GetUsuariosFromIdFromSP(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        // POST: api/Usuarios/sp
        [HttpPost("sp")]
        public async Task<ActionResult<Usuario>> PostUsuarioSp(Usuario usuario)
        {
            await _spRepository.CreateUsuarioFromSP(usuario);
            return CreatedAtAction(nameof(GetUsuariosSp), new { id = usuario.Id }, usuario);
        }

        // PUT: api/Usuarios/sp/5
        [HttpPut("sp/{id}")]
        public async Task<IActionResult> PutUsuarioSp(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            var existingUsuario = await _spRepository.GetUsuariosFromIdFromSP(id);
            if (existingUsuario == null)
            {
                return NotFound();
            }

            try
            {
                await _spRepository.UpdateUsuarioFromSP(usuario);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Usuarios/sp/5
        [HttpDelete("sp/{id}")]
        public async Task<IActionResult> DeleteUsuarioSp(int id)
        {
            var usuario = await _spRepository.GetUsuariosFromIdFromSP(id);
            if (usuario == null)
            {
                return NotFound();
            }

            await _spRepository.DeleteUsuarioFromSP(id);
            return NoContent();
        }

        #endregion

        #region AdoNet

        // GET: api/Usuarios/ado
        [HttpGet("ado")]
        public ActionResult<IEnumerable<Usuario>> GetUsuariosAdo()
        {
            var usuarios = _adoRepository.GetAllUsuariosAdo();
            return Ok(usuarios);
        }

        // GET: api/Usuarios/ado/5
        [HttpGet("ado/{id}")]
        public ActionResult<Usuario> GetUsuarioAdo(int id)
        {
            var usuario = _adoRepository.GetAllUsuarioIdAdo(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        // POST: api/Usuarios/ado
        [HttpPost("ado")]
        public ActionResult<Usuario> PostUsuarioAdo(Usuario usuario)
        {
            _adoRepository.InsertUsuarioAdo(usuario);
            return CreatedAtAction(nameof(GetUsuarioAdo), new { id = usuario.Id }, usuario);
        }

        // PUT: api/Usuarios/ado/5
        [HttpPut("ado/{id}")]
        public IActionResult PutUsuarioAdo(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            try
            {
                _adoRepository.UpdateUsuarioAdo(usuario);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        // DELETE: api/Usuarios/ado/5
        [HttpDelete("ado/{id}")]
        public IActionResult DeleteUsuarioAdo(int id)
        {
            try
            {
                _adoRepository.DeleteUsuarioAdo(id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        #endregion

    }
}
