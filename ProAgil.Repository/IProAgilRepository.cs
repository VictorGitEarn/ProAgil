using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        //Eventos

        Task<Evento[]> GetAllEventosByTema(string tema, bool includePalestrantes = false);
        Task<Evento[]> GetAllEventoAsync(bool includePalestrantes = false);
        Task<Evento> GetAllEventosById(int eventoId, bool includePalestrantes = false);

        //Palestrante
        Task<Palestrante[]> GetAllPalestrantesAsyncByName(string name, bool includeEventos = false);
        Task<Palestrante> GetPalestranteAsync(int palestranteId, bool includeEventos = false);

    }
}