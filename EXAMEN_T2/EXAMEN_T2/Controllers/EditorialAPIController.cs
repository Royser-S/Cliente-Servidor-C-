using EXAMEN_T2.Model;
using EXAMEN_T2.Repositorio.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EXAMEN_T2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditorialAPIController : ControllerBase
    {

        // === PAISES ===
        [HttpGet("getPaises")]
        public async Task<ActionResult<List<Pais>>> getPaises()
        {
            var lista = await Task.Run(() => new paisDAO().getPaises());
            return Ok(lista);
        }

        // === EDITORIALES ===

        [HttpGet("getEditoriales")]
        public async Task<ActionResult<List<Editorial>>> getEditoriales()
        {
            var lista = await Task.Run(() => new editorialDAO().getEditoriales());
            return Ok(lista);
        }

        [HttpGet("getEditorial/{id}")]
        public async Task<ActionResult<Editorial>> getEditorial(string id)
        {
            var obj = await Task.Run(() => new editorialDAO().getEditorial(id));
            if (obj == null) return NotFound("No existe la editorial");
            return Ok(obj);
        }

        [HttpPost("insertEditorial")]
        public async Task<ActionResult<string>> insertEditorial(Editorial reg)
        {
            var mensaje = await Task.Run(() => new editorialDAO().insertEditorial(reg));
            return Ok(mensaje);
        }

        [HttpPut("updateEditorial")]
        public async Task<ActionResult<string>> updateEditorial(Editorial reg)
        {
            var mensaje = await Task.Run(() => new editorialDAO().updateEditorial(reg));
            return Ok(mensaje);
        }

        [HttpDelete("deleteEditorial/{id}")]
        public async Task<ActionResult<string>> deleteEditorial(string id)
        {
            var mensaje = await Task.Run(() => new editorialDAO().deleteEditorial(id));
            return Ok(mensaje);
        }

        // Agrega esto dentro de tu EditorialAPIController

        [HttpGet("getEditorialesPorNombre/{nombre}")]
        public async Task<ActionResult<List<Editorial>>> getEditorialesPorNombre(string nombre)
        {
            var lista = await Task.Run(() => new editorialDAO().getEditorialesPorNombre(nombre));
            return Ok(lista);
        }

    }
}
