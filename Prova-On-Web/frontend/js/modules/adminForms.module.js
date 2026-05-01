import { logInfo, logError } from '../utils/logger.js';
import { listarProfessores, buscarProfessor, cadastrarProfessor, atualizarProfessor, removerProfessor } from '../services/professorService.js';

function element(id) { return document.getElementById(id); }

export function initAdminForms() {
  try {
    // Buscar professor page
    const buscarInput = element('professorIdBuscar');
    if (buscarInput && document.querySelector('#tabelaProfessor')) {
      const btn = buscarInput.nextElementSibling || document.querySelector('button[onclick="buscarProfessor()"]');
      const tbody = document.querySelector('#tabelaProfessor tbody');

      async function handleBuscar() {
        const id = buscarInput.value?.trim();
        if (!id) return window.Swal ? Swal.fire('Atenção', 'Informe o ID do professor para buscar.', 'warning') : alert('Informe o ID do professor');
        try {
          const professor = await buscarProfessor(id);
          if (!professor) {
            if (window.Swal) Swal.fire('Nenhum resultado', 'Nenhum professor encontrado com esse ID.', 'info');
            tbody.innerHTML = `<tr><td colspan="3" class="text-center text-muted">Nenhum professor carregado.</td></tr>`;
            return;
          }
          const dataRegistro = professor.dataRegistro || professor.dataHoraRegistro || professor.dataRegistro;
          tbody.innerHTML = `
            <tr>
              <td>${professor.nome || '-'}</td>
              <td>${professor.email || '-'}</td>
              <td>${dataRegistro ? new Date(dataRegistro).toLocaleString('pt-BR') : '-'}</td>
            </tr>
          `;
          if (window.Swal) Swal.fire('Professor encontrado', `Mostrando dados de ${professor.nome || 'Professor'}.`, 'success');
        } catch (err) {
          logError('UI','Erro ao buscar professor', err);
          if (window.Swal) Swal.fire('Erro', 'Erro ao buscar professor. Verifique sua conexão ou o ID.', 'error');
          tbody.innerHTML = `<tr><td colspan="3" class="text-center text-muted">Nenhum professor carregado.</td></tr>`;
        }
      }

      if (btn) btn.addEventListener('click', handleBuscar);
    }

    // Cadastrar professor page
    const formCadastro = element('formCadastro');
    if (formCadastro) {
      formCadastro.addEventListener('submit', async (e) => {
        e.preventDefault();
        const nome = element('nome')?.value.trim();
        const email = element('email')?.value.trim();
        const senha = element('senha')?.value || '';
        if (!nome || !email || !senha) return window.Swal ? Swal.fire('Atenção','Preencha todos os campos.','warning') : alert('Preencha todos os campos');
        try {
          const resp = await cadastrarProfessor({ id:0, nome, email, senha });
          if (window.Swal) Swal.fire('Professor cadastrado!', `ID: <b>${resp?.id}</b><br>Nome: <b>${resp?.nome}</b>`, 'success');
          formCadastro.reset();
        } catch (err) {
          logError('UI','Erro ao cadastrar professor', err);
          if (window.Swal) Swal.fire('Erro', 'Erro ao cadastrar professor. Verifique os dados.', 'error');
        }
      });
    }

    // Atualizar professor page
    const formAtualizar = element('formAtualizarProfessor');
    const buscarAtualizar = element('professorIdBuscar');
    if (buscarAtualizar && formAtualizar) {
      let idSelecionado = null;
      const nomeEl = element('nomeProfessor');
      const emailEl = element('emailProfessor');
      const senhaEl = element('senhaProfessor');

      async function buscarParaAtualizar() {
        const id = buscarAtualizar.value?.trim();
        if (!id) return window.Swal ? Swal.fire('Atenção','Informe o ID do professor para buscar.','warning') : alert('Informe o ID');
        try {
          const professor = await buscarProfessor(id);
          if (!professor) { if (window.Swal) Swal.fire('Nenhum resultado','Nenhum professor encontrado','info'); formAtualizar.style.display='none'; return; }
          idSelecionado = id;
          nomeEl.value = professor.nome||''; emailEl.value = professor.email||''; senhaEl.value=''; formAtualizar.style.display='block';
          if (window.Swal) Swal.fire('Professor encontrado','Pronto para editar','success');
        } catch (err) { logError('UI','Erro buscar p atualizacao',err); if (window.Swal) Swal.fire('Erro','Erro ao buscar professor','error'); }
      }

      const btnBuscar = document.querySelector('#professorIdBuscar + button') || document.querySelector('button[onclick="buscarProfessor()"]');
      if (btnBuscar) btnBuscar.addEventListener('click', buscarParaAtualizar);

      formAtualizar.addEventListener('submit', async (e) => {
        e.preventDefault(); if (!idSelecionado) return;
        const nome = nomeEl.value.trim(); const email = emailEl.value.trim(); const senha = senhaEl.value;
        if (!nome || !email) return window.Swal ? Swal.fire('Atenção','Nome e email são obrigatórios','warning') : alert('Nome e email obrigatórios');
        const payload = { id: idSelecionado, nome, email };
        if (senha) payload.senha = senha;
        try { await atualizarProfessor(payload); if (window.Swal) Swal.fire('Atualizado','Professor atualizado com sucesso','success'); formAtualizar.style.display='none'; buscarAtualizar.value=''; } catch (err) { logError('UI','Erro atualizar',err); if (window.Swal) Swal.fire('Erro','Erro ao atualizar professor','error'); }
      });
    }

    // Remover professor page
    const buscarRemover = element('professorIdBuscar');
    const btnRemover = element('btnRemover');
    if (buscarRemover && element('tabelaProfessor')) {
      let idSelecionado = null;
      async function handleBuscarRemover() {
        const id = buscarRemover.value?.trim(); if (!id) return window.Swal ? Swal.fire('Atenção','Informe o ID do professor para buscar.','warning') : alert('Informe o ID');
        try {
          const professor = await buscarProfessor(id);
          if (!professor) { if (window.Swal) Swal.fire('Nenhum resultado','Nenhum professor encontrado','info'); element('tabelaProfessor').querySelector('tbody').innerHTML = `<tr><td colspan="3" class="text-center text-muted">Nenhum professor carregado.</td></tr>`; if (btnRemover) btnRemover.style.display='none'; return; }
          idSelecionado = id; const tbody = element('tabelaProfessor').querySelector('tbody'); const dataRegistro = professor.dataHoraRegistro || professor.dataRegistro; tbody.innerHTML = `<tr><td>${professor.nome||'-'}</td><td>${professor.email||'-'}</td><td>${dataRegistro?new Date(dataRegistro).toLocaleString('pt-BR'):'-'}</td></tr>`; if (btnRemover) btnRemover.style.display='inline-block'; if (window.Swal) Swal.fire('Professor encontrado','Pronto para remoção','success');
        } catch (err) { logError('UI','Erro buscar p remover',err); if (window.Swal) Swal.fire('Erro','Erro ao buscar professor','error'); }
      }
      const btnBuscar = document.querySelector('#professorIdBuscar + button') || document.querySelector('button[onclick="buscarProfessor()"]');
      if (btnBuscar) btnBuscar.addEventListener('click', handleBuscarRemover);
      if (btnRemover) btnRemover.addEventListener('click', async () => {
        if (!idSelecionado) return;
        const conf = await (window.Swal ? Swal.fire({title:'Tem certeza?', text:`Remover professor ${idSelecionado}?`, icon:'warning', showCancelButton:true, confirmButtonText:'Sim, remover'}) : Promise.resolve({isConfirmed: confirm('Remover?')}));
        if (!conf.isConfirmed) return;
        try { await removerProfessor(idSelecionado); if (window.Swal) Swal.fire('Removido!','Professor removido com sucesso.','success'); element('tabelaProfessor').querySelector('tbody').innerHTML = `<tr><td colspan="3" class="text-center text-muted">Nenhum professor carregado.</td></tr>`; if (btnRemover) btnRemover.style.display='none'; buscarRemover.value=''; } catch (err) { logError('UI','Erro remover',err); if (window.Swal) Swal.fire('Erro','Erro ao remover professor','error'); }
      });
    }

    // Listar professores page
    const tabelaProfessores = element('tabelaProfessores');
    if (tabelaProfessores) {
      async function carregar() {
        try {
          const professores = await listarProfessores();
          tabelaProfessores.innerHTML = '';
          if (!professores || professores.length === 0) { tabelaProfessores.innerHTML = `<tr><td colspan="4" class="text-center text-muted">Nenhum professor encontrado.</td></tr>`; return; }
          professores.forEach(p => {
            const dataRegistro = p.dataRegistro || p.dataHoraRegistro;
            const tr = document.createElement('tr');
            tr.innerHTML = `
              <td style="vertical-align: middle;">${p.id||'-'}</td>
              <td style="vertical-align: middle;">${p.nome||'-'}</td>
              <td style="vertical-align: middle;">${p.email||'-'}</td>
              <td style="vertical-align: middle;">${dataRegistro?new Date(dataRegistro).toLocaleString('pt-BR'):'-'}</td>
              <td style="vertical-align: middle;">
                <button class="btn btn-warning btn-sm me-2 btn-edit" data-id="${p.id}">Editar</button>
                <button class="btn btn-danger btn-sm btn-delete" data-id="${p.id}" data-nome="${(p.nome||'').replace(/'/g,"\'")}">Deletar</button>
              </td>
            `;
            tabelaProfessores.appendChild(tr);
          });

          tabelaProfessores.querySelectorAll('.btn-delete').forEach(btn => btn.addEventListener('click', async (e) => {
            const id = btn.getAttribute('data-id'); const nome = btn.getAttribute('data-nome');
            const res = await (window.Swal ? Swal.fire({title:`Tem certeza que deseja deletar o professor "${nome}"?`, text:'Esta ação não pode ser desfeita!', icon:'warning', showCancelButton:true, confirmButtonText:'Sim, deletar!'}) : Promise.resolve({isConfirmed: confirm('Confirma?')}));
            if (!res.isConfirmed) return;
            try { await removerProfessor(id); if (window.Swal) Swal.fire('Deletado!','Professor excluído com sucesso.','success'); carregar(); } catch (err) { logError('UI','Erro deletar',err); if (window.Swal) Swal.fire('Erro','Erro ao excluir professor.','error'); }
          }));

        } catch (err) { logError('UI','Erro listar professores',err); if (window.Swal) Swal.fire('Erro','Não foi possível carregar os professores.','error'); tabelaProfessores.innerHTML = `<tr><td colspan="4" class="text-center text-danger">Erro ao carregar dados.</td></tr>`; }
      }
      carregar();
    }

  } catch (err) {
    logError('UI','initAdminForms failed', err);
  }
}
