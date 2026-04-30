using ApiProvaOnline.Application.Interfaces;
using ApiProvaOnline.Infrastructure.Data;
using ApiProvaOnline.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiProvaOnline.Infrastructure.Repositories
{
    public class AlunoRepository : GenericRepository<Aluno>, IAlunoRepository
    {
        public AlunoRepository(ProvaOnDbContext context) : base(context) { }

        public async Task<IEnumerable<Aluno>> GetByProvaIdAsync(int provaId)
        {
            return await _context.Alunos
                .Where(a => a.IdProva == provaId)
                .OrderBy(a => a.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<HistoricoAlunoDTO>> GetHistoricoAsync(int alunoId)
        {
            return await _context.AlunoRespostasObjetivas
                .Where(ar => ar.IdAluno == alunoId)
                .Include(ar => ar.Aluno)
                .Include(ar => ar.PerguntaObjetiva)
                    .ThenInclude(po => po!.Prova)
                .OrderBy(ar => ar.PerguntaObjetiva!.IdProva)
                .ThenBy(ar => ar.IdPerguntaObjetiva)
                .Select(ar => new HistoricoAlunoDTO
                {
                    id = ar.IdAluno,
                    nome = ar.Aluno!.Nome,
                    prova = ar.PerguntaObjetiva!.Prova!.Titulo,
                    pergunta = ar.PerguntaObjetiva.Titulo,
                    opcaocorreta = ar.PerguntaObjetiva.OpcaoCorreta,
                    pontuacaofinal = ar.PontuacaoFinal
                })
                .ToListAsync();
        }
    }
}
