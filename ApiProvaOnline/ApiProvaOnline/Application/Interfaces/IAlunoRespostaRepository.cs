using ApiProvaOnline.Model;

namespace ApiProvaOnline.Application.Interfaces
{
    public interface IAlunoRespostaRepository : IRepository<AlunoResposta>
    {
        Task<IEnumerable<AlunoResposta>> GetByAlunoIdAsync(int alunoId);
    }
}
