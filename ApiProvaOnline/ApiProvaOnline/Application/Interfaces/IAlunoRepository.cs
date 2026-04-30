using ApiProvaOnline.Model;

namespace ApiProvaOnline.Application.Interfaces
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        Task<IEnumerable<Aluno>> GetByProvaIdAsync(int provaId);
        Task<IEnumerable<HistoricoAlunoDTO>> GetHistoricoAsync(int alunoId);
    }
}
