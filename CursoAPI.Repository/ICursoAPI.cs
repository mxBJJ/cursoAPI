using System;
using System.Threading.Tasks;
using CursoAPI.Domain;

namespace CursoAPI.Repository
{
    public interface ICursoAPI
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        //EVENTOS
        Task<Evento[]> GetAllEventosByTheme(string tema, bool includePalestrantes);
        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes);
        Task<Evento> GetEventoAsyncById(int EventoId, bool includePalestrantes);


        //PALESTRANTES
        Task<Palestrante[]> GetAllPalestrantesAsyncByName(string nome, bool includeEventos);
        Task<Palestrante> GetPalestranteAsyncById(int PalestranteId, bool includeEventos);
    }
}
