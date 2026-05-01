import { get } from '../api/api.js';

export function obterPontuacaoAluno(alunoId, provaId) {
  return get(`/AlunoRespostaObjetiva/PontuacaoAluno?alunoId=${encodeURIComponent(alunoId)}&provaId=${encodeURIComponent(provaId)}`);
}

export function obterHistoricoAluno(alunoId) {
  return get(`/Aluno/Historico?id=${encodeURIComponent(alunoId)}`);
}