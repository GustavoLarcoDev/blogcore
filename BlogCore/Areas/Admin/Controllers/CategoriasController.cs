using BlogCore.AccesoDatos.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabjo;

        public CategoriasController(IContenedorTrabajo contenedorTrabjo)
        {
            _contenedorTrabjo = contenedorTrabjo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        #region Llamadas a la API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabjo.Categoria.GetAll() });
        }
        #endregion

    }
}
