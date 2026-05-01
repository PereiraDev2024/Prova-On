import { logInfo, logError } from '../utils/logger.js';
import { cadastrarAluno } from '../services/alunoService.js';

export function initCadastroAlunoPage() {
  const form = document.getElementById('formAluno');
  if (!form) return;

  const provaId = sessionStorage.getItem('provaId');
  if (!provaId) {
    window.location.href = 'CodigoAcessoProva.html';
    return;
  }

  const inputData = document.getElementById('dataNascimento');
  if (window.IMask && inputData) {
    try { IMask(inputData, { mask: '00/00/0000' }); } catch (e) { /* ignore */ }
  }

  form.addEventListener('submit', async (e) => {
    e.preventDefault();

    const nome = document.getElementById('nome')?.value.trim();
    const data = document.getElementById('dataNascimento')?.value.trim();

    if (!nome || !data) {
      alert('Preencha todos os campos.');
      return;
    }

    const partes = data.split('/');
    if (partes.length !== 3 || partes[2].length !== 4) {
      alert('Data inválida. Use dd/mm/aaaa');
      return;
    }

    const dataISO = `${partes[2]}-${partes[1]}-${partes[0]}`;

    const payload = {
      id: 0,
      nome,
      dataNascimento: new Date(dataISO).toISOString(),
      idProva: Number(provaId),
      dataHoraRegistro: new Date().toISOString()
    };

    try {
      logInfo('UI', 'Cadastro de aluno iniciado');
      const alunoCriado = await cadastrarAluno(payload);
      const alunoId = alunoCriado?.id ?? alunoCriado?.aluno?.id ?? alunoCriado?.data?.id ?? null;
      if (alunoId) {
        localStorage.setItem('alunoId', String(alunoId));
        sessionStorage.setItem('alunoId', String(alunoId));
      }

      alert('Cadastro realizado. Iniciando a prova...');
      window.location.href = 'Prova.html';
    } catch (error) {
      logError('UI', 'Erro ao cadastrar aluno', error);
      alert('Erro ao cadastrar aluno. Verifique a API.');
    }
  });
}
