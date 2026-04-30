using ApiProvaOnline.Application.Interfaces;
using ApiProvaOnline.Infrastructure.Data;
using ApiProvaOnline.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiProvaOnline.Infrastructure.Repositories
{
    public class PerguntaObjetivaRepository : GenericRepository<PerguntaObjetiva>, IPerguntaObjetivaRepository
    {
        public PerguntaObjetivaRepository(ProvaOnDbContext context) : base(context) { }

        public async Task<IEnumerable<PerguntaObjetiva>> GetByProvaIdAsync(int provaId)
        {
            return await _context.PerguntasObjetivas
                .Where(p => p.IdProva == provaId)
                .OrderBy(p => p.Titulo)
                .ToListAsync();
        }
    }
}
