using ApiProvaOnline.Application.Interfaces;
using ApiProvaOnline.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiProvaOnline.Application.Services
{
    public class CorrecaoService : ICorrecaoService
    {
        private readonly IAlunoRespostaObjetivaRepository _respostaRepository;
        private readonly IPerguntaObjetivaRepository _perguntaRepository;

        public CorrecaoService(
            IAlunoRespostaObjetivaRepository respostaRepository,
            IPerguntaObjetivaRepository perguntaRepository)
        {
            _respostaRepository = respostaRepository;
            _perguntaRepository = perguntaRepository;
        }

        public async Task<double> CalcularPontuacaoAsync(int alunoId, int provaId)
        {
            return await _respostaRepository.GetPontuacaoTotalAsync(alunoId, provaId);
        }

        public async Task CorrigirProvaAsync(int alunoId, int provaId)
        {
            var respostas = await _respostaRepository.GetByAlunoIdAsync(alunoId);
            var respostasDaProva = respostas
                .Where(r => r.PerguntaObjetiva?.IdProva == provaId)
                .ToList();

            foreach (var resposta in respostasDaProva)
            {
                if (resposta.PerguntaObjetiva == null) continue;

                var acertou = char.ToUpper(resposta.Resposta) == char.ToUpper(resposta.PerguntaObjetiva.OpcaoCorreta);
                resposta.PontuacaoFinal = acertou ? resposta.PerguntaObjetiva.Pontuacao : 0;
                _respostaRepository.Update(resposta);
            }

            await _respostaRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<AlunoPontuacaoDTO>> ListarPontuacoesProvaAsync(int provaId)
        {
            var respostas = await _respostaRepository.GetByProvaIdAsync(provaId);

            return respostas
                .GroupBy(r => new { r.IdAluno, NomeAluno = r.Aluno?.Nome ?? string.Empty, Prova = r.PerguntaObjetiva?.Prova?.Titulo ?? string.Empty })
                .Select(g => new AlunoPontuacaoDTO
                {
                    aluno = g.Key.NomeAluno,
                    Titulo = g.Key.Prova,
                    PontuacaoFinal = g.Sum(r => r.PontuacaoFinal).ToString("F2")
                });
        }
    }
}
