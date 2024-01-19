using PruebaTecnicaTareas.Models.Tables;

namespace PruebaTecnicaTareas.Repository
{
    public class TareaRepository
    {
        public readonly DBTareaContext _context;
        public TareaRepository(DBTareaContext context)
        {
            _context = context;
        }

        public async Task<List<Tarea>> listarTareas()
        {
            return _context.Tareas.ToList();
        }

        public async Task<Tarea?> getByTarea(int id)
        {
            var result = _context.Tareas.Find(id);
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<Tarea?> crearTarea(Tarea tarea)
        {
            _context.Tareas.Add(tarea);
            var result = _context.SaveChanges();

            if (result > 0)
            {
                return tarea;
            }

            return null;
        }

        public async Task<Tarea?> actualizarTarea(Tarea tarea)
        {
            _context.Tareas.Update(tarea);
            var result = _context.SaveChanges();

            if (result > 0)
            {
                return tarea;
            }

            return null;
        }

        public async Task<Tarea?> deleteTarea(Tarea tarea)
        {
            _context.Tareas.Remove(tarea);

            var result = _context.SaveChanges();

            if (result > 0)
            {
                return tarea;
            }

            return null;
        }
    }
}
