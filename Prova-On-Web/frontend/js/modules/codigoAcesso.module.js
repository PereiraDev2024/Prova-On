import { logInfo, logError } from '../utils/logger.js';
import { buscarProvaPorCodigo } from '../services/provaService.js';

export function initCodigoAcessoPage() {
  const form = document.getElementById('formCodigo');
  if (!form) return;

  const inputCodigo = document.getElementById('input_codigo');

  form.addEventListener('submit', async (event) => {
    event.preventDefault();

    const codigo = inputCodigo?.value.trim();

    if (!codigo) {
      alert('Por favor, insira um c�digo.');
      return;
    }

    try {
      logInfo('UI', `Busca de prova iniciada: ${codigo}`);

      const prova = await buscarProvaPorCodigo(codigo);

      // Criar ou reutilizar alunoId
      let alunoId = localStorage.getItem('alunoId') || sessionStorage.getItem('alunoId');
      
      if (!alunoId) {
        // Gerar novo alunoId único (usando timestamp + random)
        alunoId = String(Date.now() + Math.floor(Math.random() * 10000));
        logInfo('UI', `Novo alunoId gerado: ${alunoId}`);
      }

      // Persistir alunoId
      localStorage.setItem('alunoId', alunoId);
      sessionStorage.setItem('alunoId', alunoId);

      sessionStorage.setItem('provaId', String(prova?.id ?? ''));
      sessionStorage.setItem('provaTitulo', prova?.titulo ?? 'Prova');

      logInfo('UI', 'C�digo validado com sucesso');
      window.location.href = 'Prova.html';
    } catch (error) {
      logError('UI', 'Erro ao validar c�digo da prova', error);

      const status = error?.response?.status;
      if (status === 404) {
        alert('Nenhuma prova encontrada com esse c�digo.');
      } else {
        alert('N�o foi poss�vel conectar � API. Verifique se o servidor est� ativo.');
      }
    }
  });
}