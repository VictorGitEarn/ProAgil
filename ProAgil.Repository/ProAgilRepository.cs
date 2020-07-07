using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilContext _context;

        public ProAgilRepository(ProAgilContext context)
        {
            this._context = context;
        }
        //Gerais
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        //Evento
        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                                            .Include(c => c.Lote)
                                            .Include(c => c.RedeSociais);

            if (includePalestrantes)
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(p => p.Palestrante);
            
            query = query.OrderByDescending(c => c.DataEvento);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTema(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                                            .Include(c => c.Lote)
                                            .Include(c => c.RedeSociais);

            if (includePalestrantes)
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(p => p.Palestrante);
            
            query = query.OrderByDescending(c => c.DataEvento)
                        .Where(c => c.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetAllEventosById(int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                                            .Include(c => c.Lote)
                                            .Include(c => c.RedeSociais);

            if (includePalestrantes)
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(p => p.Palestrante);
            
            query = query.OrderByDescending(c => c.DataEvento)
                        .Where(c => c.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }

        //Palestrante
        public async Task<Palestrante[]> GetaLLPalestrantesAsyncByName(string name, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                                            .Include(c => c.RedeSociais);

            if (includeEventos)
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(e => e.Evento);
            
            query = query.Where(p => p.Nome.ToLower().Contains(name.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteAsync(int palestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                                            .Include(c => c.RedeSociais);

            if (includeEventos)
                query = query
                    .Include(pe => pe.PalestranteEventos)
                    .ThenInclude(e => e.Evento);
            
            query = query.OrderBy(p => p.Nome)
                        .Where(p => p.Id == palestranteId);

            return await query.FirstOrDefaultAsync();
        }
    }
}