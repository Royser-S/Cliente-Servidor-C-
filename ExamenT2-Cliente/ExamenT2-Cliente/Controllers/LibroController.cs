using ExamenT2_Cliente.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace ExamenT2_Cliente.Controllers
{
    public class LibroController : Controller
    {
        // CAMBIA ESTO POR TU PUERTO REAL
        string baseurlLibro = "https://localhost:7270/api/LibroAPI/";
        string baseurlEditorial = "https://localhost:7270/api/EditorialAPI/";


        // === MÉTODO AUXILIAR: Carga la lista de Editoriales para el Combo ===
        public async Task<List<Editorial>> CargarEditoriales()
        {
            List<Editorial> temporal = new List<Editorial>();
            using (var client = new HttpClient())
            {
                // Nota: Apuntamos al controller de EditorialAPI
                client.BaseAddress = new Uri(baseurlEditorial);
                HttpResponseMessage response = await client.GetAsync("getEditoriales");

                if (response.IsSuccessStatusCode)
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    temporal = JsonConvert.DeserializeObject<List<Editorial>>(apiresponse).ToList();
                }
            }
            return temporal;
        }

        // 1. LISTAR (INDEX) - Con Filtro por Autor
        public async Task<ActionResult> Index(string busqueda = "")
        {
            List<Libro> temporal = new List<Libro>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurlLibro);

                string endpoint = string.IsNullOrEmpty(busqueda)
                                  ? "getLibros"
                                  : $"getLibrosPorAutor/{busqueda}";

                HttpResponseMessage response = await client.GetAsync(endpoint);
                string apiresponse = await response.Content.ReadAsStringAsync();
                temporal = JsonConvert.DeserializeObject<List<Libro>>(apiresponse).ToList();
            }
            return View(temporal);
        }

        // 2. CREAR (VISTA GET)
        public async Task<ActionResult> Create()
        {
            // Llenamos el Combo
            List<Editorial> listaEds = await CargarEditoriales();
            ViewBag.Editoriales = new SelectList(listaEds, "CodigoEditorial", "NombreEditorial");

            return View();
        }

        // 2. CREAR (POST)
        [HttpPost]
        public async Task<ActionResult> Create(Libro reg)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurlLibro);
                StringContent content = new StringContent(JsonConvert.SerializeObject(reg), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("insertLibro", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Mensaje = "Error al registrar. Verifique el código.";
                }
            }

            // Si falla, recargamos el combo
            List<Editorial> listaEds = await CargarEditoriales();
            ViewBag.Editoriales = new SelectList(listaEds, "CodigoEditorial", "NombreEditorial", reg.CodigoEditorial);

            return View(reg);
        }

        // 3. EDITAR (VISTA GET)
        public async Task<ActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return RedirectToAction("Index");

            Libro reg = new Libro();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurlLibro);
                HttpResponseMessage response = await client.GetAsync("getLibro/" + id);
                string apiresponse = await response.Content.ReadAsStringAsync();
                reg = JsonConvert.DeserializeObject<Libro>(apiresponse);
            }

            // Cargar combo y seleccionar la editorial actual del libro
            List<Editorial> listaEds = await CargarEditoriales();
            ViewBag.Editoriales = new SelectList(listaEds, "CodigoEditorial", "NombreEditorial", reg.CodigoEditorial);

            return View(reg);
        }

        // 3. EDITAR (POST)
        [HttpPost]
        public async Task<ActionResult> Edit(Libro reg)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurlLibro);
                StringContent content = new StringContent(JsonConvert.SerializeObject(reg), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync("updateLibro", content);

                if (response.IsSuccessStatusCode) return RedirectToAction("Index");
                else ViewBag.Mensaje = "Error al actualizar";
            }

            List<Editorial> listaEds = await CargarEditoriales();
            ViewBag.Editoriales = new SelectList(listaEds, "CodigoEditorial", "NombreEditorial", reg.CodigoEditorial);
            return View(reg);
        }

        // 4. DETALLES
        public async Task<ActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id)) return RedirectToAction("Index");

            Libro reg = new Libro();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurlLibro);
                HttpResponseMessage response = await client.GetAsync("getLibro/" + id);
                string apiresponse = await response.Content.ReadAsStringAsync();
                reg = JsonConvert.DeserializeObject<Libro>(apiresponse);

            }
            return View(reg);
        }

        // 5. ELIMINAR
        public async Task<ActionResult> Delete(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurlLibro);
                await client.DeleteAsync("deleteLibro/" + id);
            }
            return RedirectToAction("Index");
        }
    }
}
