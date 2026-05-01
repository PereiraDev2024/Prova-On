import { logInfo, logError } from '../utils/logger.js';

export function initListarProvasPage() {
  const tabelaProvas = document.getElementById('tabelaProvas');
  if (!tabelaProvas) return;

  async function carregarProvas() {
    try {
      const base = document.querySelector('meta[name="api-base-url"]').content.replace(/\/$/, '');
      const resposta = await fetch(`${base}/ListarProva`);
      const provas = await resposta.json();

      tabelaProvas.innerHTML = '';

      provas.forEach(prova => {
        const tr = document.createElement('tr');
        tr.innerHTML = `
          <td style="vertical-align: middle;">${prova.titulo}</td>
          <td style="vertical-align: middle;">${new Date(prova.dataHoraRegistro).toLocaleDateString()}</td>
          <td style="vertical-align: middle;">${prova.codigo}</td>
          <td style="vertical-align: middle;">
            <button style="margin-left:10px;" class="btn btn-warning btn-sm btn-edit" data-id="${prova.id}">
              <i class="bi bi-pencil-square"></i> Editar
            </button>
            <button style="margin-left:10px;" class="btn btn-danger btn-sm btn-delete" data-id="${prova.id}">
              <i class="bi bi-trash"></i> Deletar
            </button>
          </td>
        `;
        tabelaProvas.appendChild(tr);
      });

      tabelaProvas.querySelectorAll('.btn-delete').forEach(btn => btn.addEventListener('click', async () => {
        const id = btn.getAttribute('data-id');
        const result = await (window.Swal ? Swal.fire({title:'Tem certeza?', text:'Essa ação não pode ser revertida!', icon:'warning', showCancelButton:true, confirmButtonText:'Sim, deletar!'}) : Promise.resolve({isConfirmed: confirm('Deletar?')}));
        if (!result.isConfirmed) return;
        try {
          const base = document.querySelector('meta[name="api-base-url"]').content.replace(/\/$/, '');
          const resp = await fetch(`${base}/RemoverProva${id}`, { method: 'DELETE' });
          if (resp.ok) { if (window.Swal) Swal.fire('Deletado!','Prova excluída com sucesso.','success'); carregarProvas(); }
          else { if (window.Swal) Swal.fire('Erro','Não foi possível excluir a prova.','error'); }
        } catch (err) { logError('UI','Erro ao excluir prova', err); if (window.Swal) Swal.fire('Erro','Erro na requisição ao excluir prova.','error'); }
      }));

      tabelaProvas.querySelectorAll('.btn-edit').forEach(btn => btn.addEventListener('click', () => {
        const id = btn.getAttribute('data-id');
        if (window.Swal) Swal.fire('Editar Prova', `Editar prova com ID: ${id}`, 'info');
      }));

    } catch (error) {
      logError('UI','Erro ao carregar provas', error);
      if (window.Swal) Swal.fire('Erro', 'Não foi possível carregar as provas.', 'error');
    }
  }

  carregarProvas();
}
