using System;
using System.Linq;
using System.Threading.Tasks;
using CursoAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace CursoAPI.Repository
{
    public class CursoAPIRepository : ICursoAPI
    {

        public readonly CursoAPIContext _context;

        public CursoAPIRepository(CursoAPIContext context)
        {
            _context = context;
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Evento[]> GetAllEventosAsync(int pageNumber, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedeSociais);

            if (includePalestrantes)
            {
                query = query.Include(pe => pe.PalestranteEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            int pageSize = 5;

            query = query.AsNoTracking()
                .OrderBy(e => e.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTheme(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
               .Include(e => e.Lotes)
               .Include(e => e.RedeSociais);

            if (includePalestrantes)
            {
                query = query.Include(pe => pe.PalestranteEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            query = query.AsNoTracking()
                .OrderByDescending(e => e.DataEvento).Where(e => e.Tema.Contains(tema));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsyncByName(string nome, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedesSociais);

            if (includeEventos)
            {
                query = query.Include(pe => pe.PalestranteEventos)
                    .ThenInclude(e => e.Evento);
            }

            query = query.AsNoTracking()
                .OrderBy(p => p.Nome).Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoAsyncById(int id, bool includePalestrantes)
        {

            IQueryable<Evento> query = _context.Eventos
               .Include(e => e.Lotes)
               .Include(e => e.RedeSociais);

            if (includePalestrantes)
            {
                query = query.Include(pe => pe.PalestranteEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            query = query.AsNoTracking()
                .OrderByDescending(e => e.DataEvento).Where(e => e.Id == id);

            return await query.FirstOrDefaultAsync();

        }

        public async Task<Palestrante> GetPalestranteAsyncById(int PalestranteId, bool includeEventos)
        {

            IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedesSociais);

            if (includeEventos)
            {
                query = query.Include(pe => pe.PalestranteEventos)
                    .ThenInclude(p => p.Evento);
            }

            query = query.AsNoTracking()
                .OrderBy(p => p.Nome).Where(p => p.Id == PalestranteId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
