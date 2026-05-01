import { logInfo } from './utils/logger.js';
import { initLoginPage } from './modules/login.module.js';
import { initCodigoAcessoPage } from './modules/codigoAcesso.module.js';
import { initPaginaAdministrador } from './modules/paginaAdministrador.module.js';
import { initProvaPage } from './modules/prova.module.js';
import { initCadastrarProvaPage } from './modules/cadastrarProva.module.js';
import { initCorrigirProvaPage } from './modules/corrigirProva.module.js';
import { initResultadoPage } from './modules/resultado.module.js';
import { initCadastroAlunoPage } from './modules/cadastroAluno.module.js';
import { initDashboardProfessorPage } from './modules/dashboardProfessor.module.js';
import { initAdminForms } from './modules/adminForms.module.js';
import { initListarProvasPage } from './modules/listarProvas.module.js';
import { initLayout } from './modules/layout.module.js';

document.addEventListener('DOMContentLoaded', () => {
  logInfo('UI', 'Aplica��o carregada');
  initLayout();
  initLoginPage();
  initCodigoAcessoPage();
  initPaginaAdministrador();
  initProvaPage();
  initCadastrarProvaPage();
  initCorrigirProvaPage();
  initResultadoPage();
  initCadastroAlunoPage();
  initDashboardProfessorPage();
  initAdminForms();
  initListarProvasPage();
});