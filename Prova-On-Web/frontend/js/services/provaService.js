import { get, post } from '../api/api.js';

export function buscarProvaPorCodigo(codigo) {
  return get(`/Prova/BuscarProvaPorCodigo/${encodeURIComponent(codigo)}`);
}

export function buscarProva(id) {
  return get(`/Prova/BuscarProva?Id=${encodeURIComponent(id)}`);
}

export function listarProvas() {
  return get('/Prova/ListarProva');
}

export function listarProvasProfessor(professorId) {
  return get(`/Prova/ListarProvasProfessor/${encodeURIComponent(professorId)}`);
}

export function cadastrarProva(payload) {
  return post('/Prova/CadastrarProva', payload);
}