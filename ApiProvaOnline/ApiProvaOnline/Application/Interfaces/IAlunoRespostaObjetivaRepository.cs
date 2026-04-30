using ApiProvaOnline.Model;

namespace ApiProvaOnline.Application.Interfaces
{
    public interface IAlunoRespostaObjetivaRepository : IRepository<AlunoRespostaObjetiva>
    {
        Task<IEnumerable<AlunoRespostaObjetiva>> GetByAlunoIdAsync(int alunoId);
        Task<IEnumerable<AlunoRespostaObjetiva>> GetByProvaIdAsync(int provaId);
        Task<double> GetPontuacaoTotalAsync(int alunoId, int provaId);
    }
}
