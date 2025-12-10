using EXAMEN_T2.Model;
using EXAMEN_T2.Repositorio.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EXAMEN_T2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroAPIController : ControllerBase
    {

        // LISTAR TODO
        [HttpGet("getLibros")]
        public async Task<ActionResult<List<Libro>>> getLibros()
        {
            var lista = await Task.Run(() => new libroDAO().getLibros());
            return Ok(lista);
        }

        // FILTRO POR AUTOR
        [HttpGet("getLibrosPorAutor/{autor}")]
        public async Task<ActionResult<List<Libro>>> getLibrosPorAutor(string autor)
        {
            var lista = await Task.Run(() => new libroDAO().getLibrosPorAutor(autor));
            return Ok(lista);
        }

        // FILTRO POR EDITORIAL (EXTRA)
        [HttpGet("getLibrosPorEditorial/{ideditorial}")]
        public async Task<ActionResult<List<Libro>>> getLibrosPorEditorial(string ideditorial)
        {
            var lista = await Task.Run(() => new libroDAO().getLibrosPorEditorial(ideditorial));
            return Ok(lista);
        }

        // OBTENER UNO (Recibe string)
        [HttpGet("getLibro/{id}")]
        public async Task<ActionResult<Libro>> getLibro(string id)
        {
            var obj = await Task.Run(() => new libroDAO().getLibro(id));
            if (obj == null) return NotFound("Libro no encontrado");
            return Ok(obj);
        }

        // INSERTAR
        [HttpPost("insertLibro")]
        public async Task<ActionResult<string>> insertLibro(Libro reg)
        {
            var msj = await Task.Run(() => new libroDAO().insertLibro(reg));
            return Ok(msj);
        }

        // ACTUALIZAR
        [HttpPut("updateLibro")]
        public async Task<ActionResult<string>> updateLibro(Libro reg)
        {
            var msj = await Task.Run(() => new libroDAO().updateLibro(reg));
            return Ok(msj);
        }

        // ELIMINAR (Recibe string)
        [HttpDelete("deleteLibro/{id}")]
        public async Task<ActionResult<string>> deleteLibro(string id)
        {
            var msj = await Task.Run(() => new libroDAO().deleteLibro(id));
            return Ok(msj);
        }

    }
}
