using cine1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace cine1.Controllers
{
    public class ModulosController : Controller
    {
        private readonly BdCineContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ModulosController(BdCineContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public IActionResult PeliculaInsertar()
        {
            if (HttpContext.Session.GetString("Username") == null || HttpContext.Session.GetString("Username") == "")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var usuario = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == HttpContext.Session.GetString("Username"));
                if (usuario.RolId != 1 && usuario.RolId != 2)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult FuncionInsertar()
        {
            if (HttpContext.Session.GetString("Username") == null || HttpContext.Session.GetString("Username") == "")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var usuario = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == HttpContext.Session.GetString("Username"));
                if (usuario.RolId != 1 && usuario.RolId != 2)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            var model = new TdFuncion();

            // Cargar opciones de películas y salas
            ViewBag.Peliculas = _context.TmPeliculas.ToList();
            ViewBag.Salas = _context.TmSalas.ToList();

            return View(model);
        }
        [HttpGet]
        public IActionResult SalaInsertar()
        {
            if (HttpContext.Session.GetString("Username") == null || HttpContext.Session.GetString("Username") == "")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var usuario = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == HttpContext.Session.GetString("Username"));
                if (usuario.RolId != 1 && usuario.RolId != 2)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult ComprarBoletos(int idFuncion, int idSala)
        {
            
            // Redirige a la página Asientos y pasa los parámetros
            return RedirectToAction("Asientos", new { idFuncion, idSala });
        }

        public IActionResult Asientos(int idFuncion, int idSala)
        {
            // Lógica para la acción Asientos
             // Obtener la información de la sala
            var sala = _context.TmSalas.Include(s => s.Asientos).FirstOrDefault(s => s.Id == idSala);
            return View(sala); // Pasar la sala como modelo a la vista
        }

        public async Task<IActionResult> Funcion()
        {
            var funciones = await _context.TdFuncions.ToListAsync();
            return View(funciones);
        }
        public async Task<IActionResult> Sala()
        {
            var salas = await _context.TmSalas.ToListAsync();
            return View(salas);
        }
        public async Task<IActionResult> Pelicula()
        {
            if (HttpContext.Session.GetString("Username") == null || HttpContext.Session.GetString("Username") == "")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var usuario = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == HttpContext.Session.GetString("Username"));
                if (usuario.RolId != 1 && usuario.RolId != 2)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            var peliculas = await _context.TmPeliculas.ToListAsync();
            peliculas.ForEach(c => c.RutaArchivo = c.RutaArchivo == null ? null : "/peliculas/" + Path.GetFileName(c.RutaArchivo));
            return View(peliculas);
        }

        // Acción para procesar el formulario e insertar un nueva pelicula en la base de datos.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PeliculaInsertar([Bind("Titulo, Duracion, Clasificacion, RutaArchivo, Director, Actor, Descripcion, Año")] TmPelicula tmPelicula, IFormFile Archivo)
        {
            try
            {
                tmPelicula.Duracion += "min";
                // Validar el archivo (puedes agregar más validaciones, como el tamaño, tipo, etc.)
                if (Archivo == null || Archivo.Length == 0)
                {
                    ModelState.AddModelError("Archivo", "El archivo es requerido.");
                    return View(tmPelicula);
                }

                // Guardar el archivo en la carpeta "peliculas" en el directorio raíz del proyecto
                var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(Archivo.FileName);
                var pathRoot = Path.Combine(_hostingEnvironment.WebRootPath, "peliculas");

                var pathArchivo = Path.Combine(pathRoot, nombreArchivo);

                // Asegurarte de que el directorio exista
                if (!Directory.Exists(pathRoot))
                {
                    Directory.CreateDirectory(pathRoot);
                }

                using (var stream = new FileStream(pathArchivo, FileMode.Create))
                {
                    await Archivo.CopyToAsync(stream);
                }

                tmPelicula.RutaArchivo = "/peliculas/" + nombreArchivo;

                if (ModelState.IsValid)
                {
                    _context.Add(tmPelicula);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage_Peli"] = "Se agregó la nueva pelicula correctamente.";
                    return RedirectToAction("Pelicula", "Modulos");
                }
            }
            catch (Exception ex)
            {
                // Almacena el mensaje de error en TempData para mostrarlo en la vista
                TempData["ErrorMessage_Peli"] = $"Error al agregar la película: {ex.Message}";

            }
            return View(tmPelicula);
        }
        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            try
            {
                var pelicula = _context.TmPeliculas.FirstOrDefault(c => c.Id == id);
                if (pelicula == null)
                {
                    TempData["ErrorMessage_Peli"] = "Pelicula no encontrado.";
                    return RedirectToAction("Pelicula");
                }

                _context.TmPeliculas.Remove(pelicula);
                _context.SaveChanges();

                TempData["SuccessMessage_Peli"] = "Pelicula eliminada correctamente.";
            }
            catch (Exception ex)
            {
               TempData["ErrorMessage_Peli"] = "Hubo un error al eliminar la pelicula. Intenta de nuevo.";
            }

            return RedirectToAction("Pelicula");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FuncionInsertar([Bind("Fecha, HoraInicio, HoraFin, IdPeli, IdSala")] TdFuncion tdfuncion)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(tdfuncion);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage_Func"] = "Se agregó la nueva función correctamente.";
                    return RedirectToAction("Funcion", "Modulos");
                }
            }
            catch (Exception ex)
            {
                // Almacena el mensaje de error en TempData para mostrarlo en la vista
                TempData["ErrorMessage_Func"] = $"Error al agregar la Función: {ex.Message}";
            }

            // Cargar opciones de películas y salas nuevamente en caso de error
            ViewBag.Peliculas = _context.TmPeliculas.ToList();
            ViewBag.Salas = _context.TmSalas.ToList();

            // Imprimir mensajes de error en la vista
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    TempData["ErrorMessage_Func"] += $"{error.ErrorMessage}\n";
                }
            }

            return View(tdfuncion);
        }

        public IActionResult EliminarFuncion(int id)
        {
            try
            {
                var funcion = _context.TdFuncions.FirstOrDefault(c => c.Id == id);
                if (funcion == null)
                {
                    TempData["ErrorMessage_Func"] = "Funcion no encontrado.";
                    return RedirectToAction("Funcion");
                }

                _context.TdFuncions.Remove(funcion);
                _context.SaveChanges();

                TempData["SuccessMessage_Func"] = "Funcion eliminada correctamente.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage_Func"] = "Hubo un error al eliminar la funcion. Intenta de nuevo.";
            }

            return RedirectToAction("Funcion");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalaInsertar([Bind("NumeroSala, CapacidadAsiento")] TmSala tmsala)
        {
            try
            {
                if (_context.TmSalas.Any(s => s.NumeroSala == tmsala.NumeroSala))
                {
                    ModelState.AddModelError("NumeroSala", "El número de sala ya existe");
                }
                if (ModelState.IsValid)
                {
                    _context.Add(tmsala);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage_Sala"] = "Se agregó la nueva Sala correctamente.";
                    return RedirectToAction("Sala", "Modulos");
                }
            }
            catch (Exception ex)
            {
                // Almacena el mensaje de error en TempData para mostrarlo en la vista
                TempData["ErrorMessage_Sala"] = $"Error al agregar la Sala: {ex.Message}";

            }
            return View(tmsala);
        }
        public IActionResult EliminarSala(int id)
        {
            try
            {
                var sala = _context.TmSalas.FirstOrDefault(c => c.Id == id);
                if (sala == null)
                {
                    TempData["ErrorMessage_Sala"] = "Sala no encontrado.";
                    return RedirectToAction("Sala");
                }

                _context.TmSalas.Remove(sala);
                _context.SaveChanges();

                TempData["SuccessMessage_Sala"] = "Sala eliminada correctamente.";
            }
            catch (Exception ex)
            {
               TempData["ErrorMessage_Sala"] = "Hubo un error al eliminar la Sala. Intenta de nuevo.";
            }

            return RedirectToAction("Sala");
        }

    }
}
