import { logInfo, logError } from '../utils/logger.js';
import { listarProvasProfessor } from '../services/provaService.js';
import { obterPontuacaoAluno } from '../services/resultadoService.js';

export function initDashboardProfessorPage() {
  const listaProvas = document.getElementById('listaProvas');
  if (!listaProvas) return;

  const inputProfessorId = document.getElementById('professorId');
  const btnBuscar = document.querySelector('button[onclick="buscarProvasDoProfessor()"]') || document.querySelector('#professorId + button');

  async function carregarProvas() {
    const professorId = inputProfessorId.value?.trim();
    if (!professorId) return alert('Informe o ID do professor.');

    try {
      logInfo('UI', `Listando provas do professor ${professorId}`);
      const provas = await listarProvasProfessor(professorId);
      renderizarProvas(provas);
    } catch (error) {
      logError('UI', 'Erro ao listar provas', error);
      alert('Erro ao buscar provas.');
    }
  }

  function renderizarProvas(provas) {
    listaProvas.innerHTML = '';
    if (!provas || provas.length === 0) {
      listaProvas.innerHTML = '<li class="list-group-item text-muted">Nenhuma prova encontrada.</li>';
      return;
    }

    provas.forEach((prova) => {
      const li = document.createElement('li');
      li.className = 'list-group-item d-flex justify-content-between align-items-center';
      li.innerHTML = `
        <div>
          <strong>${prova.titulo ?? 'Sem título'}</strong><br>
          Código: ${prova.codigo ?? '-'} | Cadastro: ${new Date(prova.dataHoraRegistro).toLocaleString()}
        </div>
        <div>
          <button class="btn btn-sm btn-info btn-result" data-id="${prova.id}" data-titulo="${(prova.titulo||'')}">Resultados</button>
          <button class="btn btn-sm btn-warning btn-edit" data-id="${prova.id}">Editar</button>
          <button class="btn btn-sm btn-danger btn-delete" data-id="${prova.id}">Apagar</button>
        </div>
      `;
      listaProvas.appendChild(li);
    });

    listaProvas.querySelectorAll('.btn-result').forEach((btn) => {
      btn.addEventListener('click', async () => {
        const provaId = btn.getAttribute('data-id');
        const titulo = btn.getAttribute('data-titulo');
        // carregar pontuações
        try {
          const resp = await fetch(`${document.querySelector('meta[name="api-base-url"]').content.replace(/\/$/, '')}/AlunoRespostaObjetiva/ListarPontuacoesProva/${encodeURIComponent(provaId)}`);
          const data = await resp.json();
          // simples render em tabela se existir elemento
          const tbody = document.getElementById('corpoTabelaAlunos');
          const sem = document.getElementById('semResultados');
          const container = document.getElementById('alunosContainer');
          if (container) container.style.display = 'block';
          if (document.getElementById('tituloProvaSelecionada')) document.getElementById('tituloProvaSelecionada').textContent = titulo;
          if (!tbody) return;
          tbody.innerHTML = '';
          if (!data || data.length === 0) {
            if (sem) sem.style.display = 'block';
            return;
          }
          data.forEach(p => {
            const tr = document.createElement('tr');
            tr.innerHTML = `<td>${p.nomeAluno ?? p.alunoNome ?? 'Aluno'}</td><td class="text-end">${Number(p.pontuacao ?? p.pontos ?? 0).toFixed(2)}</td>`;
            tbody.appendChild(tr);
          });
        } catch (err) {
          logError('UI', 'Erro ao carregar pontuações', err);
          alert('Erro ao carregar pontuações');
        }
      });
    });
  }

  if (btnBuscar) btnBuscar.addEventListener('click', carregarProvas);
}
