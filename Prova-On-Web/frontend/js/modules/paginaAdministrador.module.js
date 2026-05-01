import { logInfo, logError } from '../utils/logger.js';
import { listarProfessores, removerProfessor } from '../services/professorService.js';
import { listarProvasProfessor } from '../services/provaService.js';
import { cadastrarAluno } from '../services/alunoService.js';

export function initPaginaAdministrador() {
  const inputProfessorId = document.getElementById('professorId');
  const btnBuscarProvas = document.getElementById('btnBuscarProvas');
  const listaProvas = document.getElementById('listaProvas');
  const listaProfessores = document.getElementById('listaProfessores');
  const formCadastrarAluno = document.getElementById('formCadastrarAluno');
  const selectProvaAluno = document.getElementById('provaAluno');
  const inputRemoverProfessorId = document.getElementById('removerProfessorId');
  const btnRemoverProfessor = document.getElementById('btnRemoverProfessor');
  const btnListarProfessores = document.getElementById('btnListarProfessores');

  if (!inputProfessorId && !formCadastrarAluno && !inputRemoverProfessorId && !btnListarProfessores) return;

  const renderizarProvas = (provas) => {
    listaProvas.innerHTML = '';
    selectProvaAluno.innerHTML = '<option value="">Selecione uma prova</option>';

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
          <small>Código: ${prova.codigo ?? '-'}</small>
        </div>
      `;
      listaProvas.appendChild(li);

      const option = document.createElement('option');
      option.value = prova.id;
      option.textContent = prova.titulo ?? `Prova ${prova.id}`;
      selectProvaAluno.appendChild(option);
    });
  };

  const carregarProfessores = async () => {
    try {
      logInfo('UI', 'Listagem de professores iniciada');
      const professores = await listarProfessores();

      listaProfessores.innerHTML = '';

      if (!professores || professores.length === 0) {
        listaProfessores.innerHTML = '<li class="list-group-item text-muted">Nenhum professor encontrado.</li>';
        return;
      }

      professores.forEach((professor) => {
        const li = document.createElement('li');
        li.className = 'list-group-item d-flex justify-content-between align-items-center';
        li.innerHTML = `
          <div>
            <strong>ID:</strong> ${professor.id}<br>
            <strong>Nome:</strong> ${professor.nome || 'Sem nome'}
          </div>
          <button class="btn btn-sm btn-danger" type="button" data-professor-id="${professor.id}">Remover</button>
        `;
        listaProfessores.appendChild(li);
      });

      listaProfessores.querySelectorAll('button[data-professor-id]').forEach((button) => {
        button.addEventListener('click', async () => {
          const id = button.getAttribute('data-professor-id');

          try {
            if (!confirm(`Deseja remover o professor ID ${id}?`)) return;

            logInfo('UI', `Remoção de professor iniciada: ${id}`);
            await removerProfessor(id);
            logInfo('UI', `Professor removido: ${id}`);
            await carregarProfessores();
          } catch (error) {
            logError('UI', 'Erro ao remover professor', error);
            alert('Erro ao remover professor.');
          }
        });
      });
    } catch (error) {
      logError('UI', 'Erro ao listar professores', error);
      alert('Erro ao listar professores.');
    }
  };

  const carregarProvasDoProfessor = async () => {
    const professorId = inputProfessorId.value.trim();

    if (!professorId) {
      alert('Informe o ID do professor.');
      return;
    }

    try {
      logInfo('UI', `Busca de provas do professor iniciada: ${professorId}`);
      const provas = await listarProvasProfessor(professorId);
      renderizarProvas(provas);
    } catch (error) {
      logError('UI', 'Erro ao buscar provas do professor', error);
      alert('Erro ao buscar provas do professor.');
    }
  };

  if (btnBuscarProvas) {
    btnBuscarProvas.addEventListener('click', carregarProvasDoProfessor);
  }

  if (btnListarProfessores) {
    btnListarProfessores.addEventListener('click', carregarProfessores);
  }

  if (btnRemoverProfessor) {
    btnRemoverProfessor.addEventListener('click', async () => {
      const id = inputRemoverProfessorId.value.trim();

      if (!id) {
        alert('Informe o ID do professor.');
        return;
      }

      if (!confirm(`Deseja remover o professor ID ${id}?`)) return;

      try {
        logInfo('UI', `Remoção manual de professor iniciada: ${id}`);
        await removerProfessor(id);
        logInfo('UI', `Professor removido: ${id}`);
        inputRemoverProfessorId.value = '';
        await carregarProfessores();
      } catch (error) {
        logError('UI', 'Erro ao remover professor', error);
        alert('Erro ao remover professor.');
      }
    });
  }

  if (formCadastrarAluno) {
    formCadastrarAluno.addEventListener('submit', async (event) => {
      event.preventDefault();

      const nome = document.getElementById('nomeAluno')?.value.trim();
      const dataNascimento = document.getElementById('dataNascimentoAluno')?.value;
      const idProva = selectProvaAluno?.value;

      if (!nome || !dataNascimento || !idProva) {
        alert('Preencha todos os campos.');
        return;
      }

      const payload = {
        id: 0,
        nome,
        dataNascimento: new Date(dataNascimento).toISOString(),
        dataHoraRegistro: new Date().toISOString(),
        idProva: Number(idProva)
      };

      try {
        logInfo('UI', 'Cadastro de aluno iniciado');
        const alunoCriado = await cadastrarAluno(payload);
        const alunoId = alunoCriado?.id ?? alunoCriado?.aluno?.id ?? alunoCriado?.data?.id;

        if (alunoId) {
          localStorage.setItem('alunoId', String(alunoId));
          sessionStorage.setItem('alunoId', String(alunoId));
        }

        logInfo('UI', 'Aluno cadastrado com sucesso');
        formCadastrarAluno.reset();
      } catch (error) {
        logError('UI', 'Erro ao cadastrar aluno', error);
        alert('Erro ao cadastrar aluno.');
      }
    });
  }

  carregarProfessores();
}