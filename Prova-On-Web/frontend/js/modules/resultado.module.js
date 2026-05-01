import { logInfo, logError } from '../utils/logger.js';
import { obterPontuacaoAluno, obterHistoricoAluno } from '../services/resultadoService.js';

export function initResultadoPage() {
  const container = document.getElementById('resultadoContainer');
  if (!container) return;

  const alunoIdPersistido = sessionStorage.getItem('alunoId') || localStorage.getItem('alunoId');
  const provaId = sessionStorage.getItem('provaId');
  const provaTitulo = sessionStorage.getItem('provaTitulo') || 'Prova';

  if (alunoIdPersistido) {
    sessionStorage.setItem('alunoId', alunoIdPersistido);
  }

  if (!alunoIdPersistido || !provaId) {
    container.innerHTML = `
      <div class="alert alert-warning text-center" role="alert">
        <h4 class="alert-heading">Sessão expirada</h4>
        <p>Não foi possível identificar o aluno ou a prova. Por favor, acesse novamente.</p>
      </div>
    `;
    return;
  }

  const renderHistorico = (historico) => {
    if (!historico || historico.length === 0) return '';

    const linhas = historico.map((item, index) => {
      const pergunta = item.pergunta ?? item.titulo ?? '-';
      const gabarito = item.opcaocorreta ?? item.opcaoCorreta ?? '-';
      const pontuacaoFinal = Number(item.pontuacaofinal ?? item.pontuacaoFinal ?? 0);
      const acertou = pontuacaoFinal > 0;

      return `
        <tr>
          <td>${index + 1}</td>
          <td>${pergunta}</td>
          <td class="text-center"><strong>${gabarito}</strong></td>
          <td class="text-center ${acertou ? 'text-success' : 'text-danger'}">
            ${acertou ? '✔ Acerto' : '✘ Erro'}
          </td>
          <td class="text-end">${pontuacaoFinal.toFixed(2)}</td>
        </tr>
      `;
    }).join('');

    return `
      <h5 class="mt-4">Detalhamento por questão</h5>
      <table class="table table-bordered table-sm">
        <thead class="table-light">
          <tr>
            <th>#</th>
            <th>Pergunta</th>
            <th class="text-center">Gabarito</th>
            <th class="text-center">Resultado</th>
            <th class="text-end">Pontos</th>
          </tr>
        </thead>
        <tbody>${linhas}</tbody>
      </table>
    `;
  };

  async function carregarResultado() {
    try {
      logInfo('UI', `Carregando resultado da prova ${provaId}`);

      const pontuacaoResp = await obterPontuacaoAluno(alunoIdPersistido, provaId);
      const pontuacao = typeof pontuacaoResp === 'number'
        ? pontuacaoResp
        : Number(pontuacaoResp?.pontuacao ?? pontuacaoResp?.data?.pontuacao ?? 0);

      let historicoHTML = '';
      try {
        const historicoResp = await obterHistoricoAluno(alunoIdPersistido);
        const historico = Array.isArray(historicoResp)
          ? historicoResp
          : (historicoResp?.historico ?? historicoResp?.data ?? []);

        historicoHTML = renderHistorico(historico);
      } catch (error) {
        logError('UI', 'Erro ao buscar histórico detalhado', error);
      }

      container.innerHTML = `
        <div class="alert alert-success text-center mt-4" role="alert">
          <h4 class="alert-heading">Prova concluída!</h4>
          <p>Prova: <strong>${provaTitulo}</strong></p>
          <hr>
          <p class="mb-0 fs-4">Sua pontuação: <strong>${pontuacao.toFixed(2)}</strong></p>
        </div>
        ${historicoHTML}
      `;

      sessionStorage.removeItem('provaId');
      sessionStorage.removeItem('provaTitulo');
    } catch (error) {
      logError('UI', 'Erro ao carregar resultado', error);
      container.innerHTML = `
        <div class="alert alert-danger text-center mt-4" role="alert">
          <h4 class="alert-heading">Erro ao carregar resultado</h4>
          <p>Não foi possível buscar sua pontuação. Tente novamente em instantes.</p>
        </div>
      `;
    }
  }

  carregarResultado();
}