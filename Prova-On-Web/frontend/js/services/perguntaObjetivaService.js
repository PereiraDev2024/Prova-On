import { get, post } from '../api/api.js';

export function listarPerguntasPorProva(provaId) {
  return get(`/PerguntaObjetiva/ListarPorProva/${encodeURIComponent(provaId)}`);
}

export function cadastrarPerguntaObjetiva(payload) {
  return post('/PerguntaObjetiva/CadastrarPerguntaObjetiva', payload);
}