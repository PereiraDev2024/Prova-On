import { post } from '../api/api.js';

export function cadastrarAluno(payload) {
  return post('/Aluno/Cadastrar', payload);
}