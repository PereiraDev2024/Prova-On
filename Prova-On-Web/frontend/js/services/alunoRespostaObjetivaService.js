import { post } from '../api/api.js';

export function cadastrarRespostaObjetiva(payload) {
  return post('/AlunoRespostaObjetiva/CadastrarAlunoRespostaObjetiva', payload);
}