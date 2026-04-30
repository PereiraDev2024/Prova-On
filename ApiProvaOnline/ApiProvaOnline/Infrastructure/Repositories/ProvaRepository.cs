using ApiProvaOnline.Application.Interfaces;
using ApiProvaOnline.Infrastructure.Data;
using ApiProvaOnline.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiProvaOnline.Infrastructure.Repositories
{
    public class ProvaRepository : GenericRepository<Prova>, IProvaRepository
    {
        public ProvaRepository(ProvaOnDbContext context) : base(context) { }

        public async Task<IEnumerable<Prova>> GetByProfessorIdAsync(int professorId)
        {
            return await _context.Provas
                .Where(p => p.IdProfessor == professorId)
                .OrderBy(p => p.Titulo)
                .ToListAsync();
        }

        public async Task<Prova?> GetByCodigoAsync(string codigo)
        {
            return await _context.Provas
                .FirstOrDefaultAsync(p => p.Codigo == codigo);
        }
    }
}
