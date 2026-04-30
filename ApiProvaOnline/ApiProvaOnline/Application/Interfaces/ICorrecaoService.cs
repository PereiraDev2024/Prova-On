using ApiProvaOnline.Model;

namespace ApiProvaOnline.Application.Interfaces
{
    public interface ICorrecaoService
    {
        Task<double> CalcularPontuacaoAsync(int alunoId, int provaId);
        Task CorrigirProvaAsync(int alunoId, int provaId);
        Task<IEnumerable<AlunoPontuacaoDTO>> ListarPontuacoesProvaAsync(int provaId);
    }
}
