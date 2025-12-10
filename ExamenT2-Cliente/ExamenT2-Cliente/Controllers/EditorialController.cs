using ExamenT2_Cliente.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace ExamenT2_Cliente.Controllers
{
    public class EditorialController : Controller
    {
        // CAMBIA ESTO POR TU PUERTO REAL
        string baseurl = "https://localhost:7270/api/EditorialAPI/";

        // MÉTODO AUXILIAR: Carga la lista de Paises desde la API
        public async Task<List<Pais>> CargarPaises()
        {
            List<Pais> temporal = new List<Pais>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                HttpResponseMessage response = await client.GetAsync("getPaises");

                
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    temporal = JsonConvert.DeserializeObject<List<Pais>>(apiresponse).ToList();
                
            }
            return temporal;
        }

        // 1. LISTAR (INDEX)
        public async Task<IActionResult> Index(string busqueda = "")
        {
            List<Editorial> temporal = new List<Editorial>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);

                // Lógica del filtro (si hay búsqueda llama a uno, sino al otro)
                string endpoint = string.IsNullOrEmpty(busqueda) ? "getEditoriales" : $"getEditorialesPorNombre/{busqueda}";

                HttpResponseMessage response = await client.GetAsync(endpoint);
                string apiresponse = await response.Content.ReadAsStringAsync();
                temporal = JsonConvert.DeserializeObject<List<Editorial>>(apiresponse).ToList();
            }
            return View(await Task.Run(() => temporal));
        }

        // 2. CREAR (VISTA GET)
        public async Task<IActionResult> Create()
        {
            // Cargamos el Combo usando SelectList
            List<Pais> listaPaises = await CargarPaises();
            ViewBag.Paises = new SelectList(listaPaises, "CodigoPais", "NombrePais");

            return View();
        }

        // 2. CREAR (POST)
        [HttpPost]
        public async Task<IActionResult> Create(Editorial reg)
        {
            /* Validación básica, si quieres estricta usa ModelState.IsValid */
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                StringContent content = new StringContent(JsonConvert.SerializeObject(reg), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("insertEditorial", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Mensaje = "Error al registrar. Verifique que el código no exista.";
                }
            }

            // Si falló, recargamos el combo
            List<Pais> listaPaises = await CargarPaises();
            ViewBag.Paises = new SelectList(listaPaises, "CodigoPais", "NombrePais", reg.CodigoPais);

            return View(reg);
        }

        // 3. EDITAR (VISTA GET)
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return RedirectToAction("Index");

            Editorial reg = new Editorial();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                HttpResponseMessage response = await client.GetAsync("getEditorial/" + id);
                string apiresponse = await response.Content.ReadAsStringAsync();
                reg = JsonConvert.DeserializeObject<Editorial>(apiresponse);
            }

            // Cargamos el combo y seleccionamos el país actual
            List<Pais> listaPaises = await CargarPaises();
            ViewBag.Paises = new SelectList(listaPaises, "CodigoPais", "NombrePais", reg.CodigoPais);

            return View(reg);
        }


        // 3. EDITAR (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(Editorial reg)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                StringContent content = new StringContent(JsonConvert.SerializeObject(reg), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync("updateEditorial", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Mensaje = "Error al actualizar";
                }
            }

            List<Pais> listaPaises = await CargarPaises();
            ViewBag.Paises = new SelectList(listaPaises, "CodigoPais", "NombrePais", reg.CodigoPais);
            return View(reg);
        }



        // 4. DETALLES (VISTA)
        public async Task<ActionResult> Details(string id)
        {
            // Validación por si alguien escribe la URL a mano sin ID
            if (string.IsNullOrEmpty(id)) return RedirectToAction("Index");

            Editorial reg = new Editorial();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                // Llamamos al método que busca por ID
                HttpResponseMessage response = await client.GetAsync("getEditorial/" + id);

                if (response.IsSuccessStatusCode)
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    reg = JsonConvert.DeserializeObject<Editorial>(apiresponse);
                }
                else
                {
                    // Si no lo encuentra, volvemos al inicio
                    return RedirectToAction("Index");
                }
            }

            return View(await Task.Run(() => reg));
        }


        // 4. ELIMINAR
        public async Task<IActionResult> Delete(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                await client.DeleteAsync("deleteEditorial/" + id);
            }
            return RedirectToAction("Index");
        }
    }
}
