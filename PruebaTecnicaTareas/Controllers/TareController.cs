using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaTareas.Models.DTO;
using PruebaTecnicaTareas.Models.Tables;
using PruebaTecnicaTareas.Services;

namespace PruebaTecnicaTareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        public readonly TareaService _tareaService;
        public TareaController(TareaService tareaService) 
        {
            _tareaService = tareaService;
        }


        //listar todas la tereas
        [HttpGet]
        public async Task<List<Tarea>> listarTareas()
        {
            return await _tareaService.listarTareas();
        }


        //buscar tareas por id
        [HttpGet("{id}")]
        public async Task<Tarea?> getByTarea(int id)
        {
            return await _tareaService.getByTarea(id);
        }

        //validar las tareas que estan procimas a vencer
        [HttpGet("[action]")]
        public async Task<List<Tarea>> TarasAVencer()
        {
            return await _tareaService.validarTareas();
        }


        //agregar una nueva tarea
        [HttpPost]
        public async Task<IActionResult> crearTarea(TareaDTO tarea)
        {
            return await _tareaService.crearTarea(tarea);
        }


        //actualizamos tareas existentes
        [HttpPut]
        public async Task<IActionResult> actualizarTarea(TareaDTO tarea)
        {
            return await _tareaService.actualizarTarea(tarea);
        }

        //eliminamos una tarea
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteTarea(int id)
        {
            return await _tareaService.deleteTarea(id);
        }
    }
}
