import { get, post, put, del } from '../api/api.js';

export function listarProfessores() {
  return get('/Professor/ListarProfessor');
}

export function buscarProfessor(id) {
  return get(`/Professor/BuscarProfessor?Id=${encodeURIComponent(id)}`);
}

export function removerProfessor(id) {
  return del(`/Professor/RemoverProfessor?Id=${encodeURIComponent(id)}`);
}

export function loginProfessor(email, senha) {
  // Call the unified auth endpoint. Backend should return { role: 'Professor'|'Administrativo', user: { ... } }
  return post('/Auth/Login', {
    Email: email,
    Senha: senha
  });
}

export function cadastrarProfessor(payload) {
  return post('/Professor/CadastrarProfessor', payload);
}

export function atualizarProfessor(payload) {
  return put('/Professor/AtualizarProfessor', payload);
}