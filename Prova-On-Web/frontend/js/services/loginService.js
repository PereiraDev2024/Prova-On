import { logInfo, logError } from '../utils/logger.js';
import { loginProfessor } from '../services/professorService.js';

export function initLoginPage() {
  const form = document.getElementById('loginForm');
  if (!form) return;

  form.addEventListener('submit', async (event) => {
    event.preventDefault();

    const email = document.getElementById('email')?.value.trim();
    const senha = document.getElementById('password')?.value;

    if (!email || !senha) {
      logInfo('UI', 'Validação falhou: campos obrigatórios');
      alert('Preencha todos os campos.');
      return;
    }

    try {
      logInfo('UI', 'Tentativa de login iniciada');

      const response = await loginProfessor(email, senha);

      // Expected response: { role: 'Professor'|'Administrativo', user: { ... } }
      const role = response?.role ?? response?.roleName ?? null;
      const user = response?.user ?? response;

      if (!role) {
        throw new Error('Resposta de autenticação inesperada. Backend deve retornar o papel do usuário.');
      }

      // Persist user and redirect based on role
      if (role.toLowerCase() === 'professor') {
        localStorage.setItem('professorLogado', JSON.stringify(user));
        sessionStorage.setItem('professorLogado', JSON.stringify(user));
        logInfo('UI', 'Login de professor realizado com sucesso');
        window.location.href = 'DashboardProfessor.html';
      } else if (role.toLowerCase() === 'administrativo' || role.toLowerCase() === 'administrador') {
        localStorage.setItem('professorLogado', JSON.stringify(user));
        sessionStorage.setItem('professorLogado', JSON.stringify(user));
        logInfo('UI', 'Login de administrador realizado com sucesso');
        window.location.href = 'PaginaDoadministrador.html';
      } else {
        throw new Error('Papel de usuário desconhecido: ' + role);
      }
    } catch (error) {
      const status = error?.response?.status ?? null;
      if (status === 404) {
        logError('UI', 'Endpoint de login não encontrado (404)');
        alert('Endpoint de login não implementado no backend (POST /Auth/Login). Verifique o servidor ou implemente o endpoint.');
      } else if (status === 401) {
        logError('UI', 'Credenciais inválidas (401)');
        alert('Email ou senha inválidos. Verifique suas credenciais.');
      } else {
        logError('UI', 'Erro ao realizar login', error);
        alert(error.message || 'Ocorreu um erro ao tentar realizar o login.');
      }
    }
  });

  form.addEventListener('reset', () => {
    logInfo('UI', 'Formulário de login limpo');
  });
}