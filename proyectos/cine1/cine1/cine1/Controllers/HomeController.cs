using cine1.Data;
using cine1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


namespace cine1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BdCineContext _context;
        public HomeController(ILogger<HomeController> logger, BdCineContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var pelicula = _context.TmPeliculas.ToList();
            pelicula.ForEach(c => c.RutaArchivo = c.RutaArchivo == null ? null : "/peliculas/" + Path.GetFileName(c.RutaArchivo));
            return View(pelicula);

        }
        public IActionResult Login()
        {
            return View();
        }
        
        public IActionResult DetallePelicula(int id)
        {
            if (HttpContext.Session.GetString("Username") == null || HttpContext.Session.GetString("Username") == "")
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var usuario = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == HttpContext.Session.GetString("Username"));
                if (usuario.RolId != 1 && usuario.RolId != 2 && usuario.RolId != 3 && usuario.RolId != 4)
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            var pelicula = ObtenerDetallesPeliculaPorId(id);
                if (pelicula != null )
                {
                    // El usuario tiene un rol permitido y la película existe
                    return View(pelicula);
                } 
      // El usuario no está autenticado o no tiene el rol permitido
            return RedirectToAction("Login", "Home"); 

        }

        private TmPelicula ObtenerDetallesPeliculaPorId(int id)
        {
            var pelicula = _context.TmPeliculas.Find(id);
            return pelicula;
        }

        public IActionResult Funciones(int id)
        {
            // Obtén todas las funciones asociadas a la película con el ID proporcionado
            var funciones = _context.TdFuncions.Where(f => f.IdPeli == id).ToList();

            // Puedes personalizar esta lógica según tus necesidades

            return View(funciones);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
