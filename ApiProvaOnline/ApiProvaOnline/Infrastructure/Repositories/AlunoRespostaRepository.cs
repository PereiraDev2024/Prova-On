using ApiProvaOnline.Application.Interfaces;
using ApiProvaOnline.Infrastructure.Data;
using ApiProvaOnline.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiProvaOnline.Infrastructure.Repositories
{
    public class AlunoRespostaRepository : GenericRepository<AlunoResposta>, IAlunoRespostaRepository
    {
        public AlunoRespostaRepository(ProvaOnDbContext context) : base(context) { }

        public async Task<IEnumerable<AlunoResposta>> GetByAlunoIdAsync(int alunoId)
        {
            return await _context.AlunoRespostas
                .Where(ar => ar.IdAluno == alunoId)
                .Include(ar => ar.Pergunta)
                .ToListAsync();
        }
    }
}
