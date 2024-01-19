using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaTareas.Models.DTO;
using PruebaTecnicaTareas.Models.Tables;
using PruebaTecnicaTareas.Repository;

namespace PruebaTecnicaTareas.Services
{
    public class TareaService : ControllerBase
    {
        public readonly TareaRepository _tareaRepository;
        public TareaService(TareaRepository tareaRepository)
        {
            _tareaRepository = tareaRepository;
        }

        public async Task<List<Tarea>> listarTareas()
        {
            return await _tareaRepository.listarTareas();
        }

        public async Task<Tarea?> getByTarea(int id)
        {
            return await _tareaRepository.getByTarea(id);
        }

        public async Task<IActionResult> crearTarea(TareaDTO tarea)
        {
            Resul results = new Resul();
            Tarea newTarea = new Tarea();


            newTarea.Título = tarea.Título;
            newTarea.Descripcion = tarea.Descripcion;
            newTarea.FechaCreacion = tarea.FechaCreacion;
            newTarea.FechaVancimiento = tarea.FechaVancimiento;
            newTarea.Estado = tarea.Estado;

            newTarea.Estado = newTarea.Estado.ToLower().Trim();

           
            if (validatEstado(newTarea.Estado) != false)
            {
                //creamos una nueva tarea
                var resultNewTare = await _tareaRepository.crearTarea(newTarea);

                //validamos si la terea fue creada correctamente
                if (resultNewTare != null)
                {

                    results.IsSucces = true;
                    results.Data = resultNewTare;
                    results.Message = "Datos guardados con exito";

                    return Ok(results);
                }
                results.IsSucces = false;
                results.Message = "No se pudo crear la tarea";
                return BadRequest(results);
            }

            results.IsSucces = false;
            results.Message = "La tarea no con los parametros establecidos, " +
                "(pendiente, en progreso, completada).";
            return BadRequest(results);

        }

        public async Task<IActionResult> actualizarTarea(TareaDTO tarea)
        {

            Resul results = new Resul();

            var tareaExis = await _tareaRepository.getByTarea(tarea.Id);
            if (tareaExis != null)
            {

                tareaExis.Id = tarea.Id;
                tareaExis.Título = tarea.Título;
                tareaExis.Descripcion = tarea.Descripcion;
                tareaExis.FechaCreacion = tarea.FechaCreacion;
                tareaExis.FechaVancimiento = tarea.FechaVancimiento;
                tareaExis.Estado = tarea.Estado;

                tareaExis.Estado = tareaExis.Estado.ToLower();

                if (validatEstado(tareaExis.Estado) == false)
                {
                    results.IsSucces = false;
                    results.Message = "La tarea no con los parametros establecidos, " +
                        "(pendiente, en progreso, completada).";
                    return BadRequest(results);
                }
                //actualizamos la tera
                var resultCrearTarea = _tareaRepository.actualizarTarea(tareaExis);

                //validamos si la tarea fue actualizada correctamente
                if (resultCrearTarea != null)
                {

                    results.IsSucces = true;
                    results.Data = tareaExis;
                    results.Message = "Datos actualizados con exito";

                    return Ok(results);
                }
                results.IsSucces = false;
                results.Message = "No se pudo actualizar la tarea";
                return BadRequest(results);
            }
            results.IsSucces = false;
            results.Message = "No se pudo actualizar la tarea";

            return BadRequest(results);
        }

        public async Task<IActionResult> deleteTarea(int id)
        {
            Resul results = new Resul();
            //validar si la terea se encuentra registrada en el sistema
            var tareaExis = await _tareaRepository.getByTarea(id);
            if (tareaExis != null)
            {
                //eliminamos la tarea
                var resultDeleteTarea = _tareaRepository.deleteTarea(tareaExis);

                //validarcion de si la tarea fue eliminada con exito
                if (resultDeleteTarea != null)
                {

                    results.IsSucces = true;
                    results.Message = "Datos eliminados con exito";

                    return Ok(results);
                }
                results.IsSucces = false;
                results.Message = "No se pudo eliminar la tarea";
                return BadRequest(results);
            }
            results.IsSucces = false;
            results.Message = "No se pudo eliminar la tarea";

            return BadRequest(results);
        }


        //validar las tareas proximas a vencer
        public async Task<List<Tarea>> validarTareas()
        {
           var result = await _tareaRepository
                .listarTareas();

            List<Tarea> listTarea = new List<Tarea>();

            foreach (var tarea in result) 
            { //recorre todas las tareas de la base de datos y validar una a una
              //cuales estan proximas a vencer que su estado sea diferente de completada
                if ((tarea.FechaCreacion - tarea.FechaVancimiento).Days < 2 && tarea.Estado != "completada")
                {
                    listTarea.Add(tarea);
                }
            }

            return listTarea;
        }


        //validar que los estados ingresados coincidan con los que se solicitaron en le prueba
        public bool validatEstado(string estado)
        {
            if(estado == "pendiente" ||
                    estado == " en progreso" ||
                    estado == "completada")
            {
                return true;
            }
            return false;
        }
    }
}
