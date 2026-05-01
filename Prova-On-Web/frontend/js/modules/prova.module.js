import { logInfo, logError } from '../utils/logger.js';
import { listarPerguntasPorProva } from '../services/perguntaObjetivaService.js';
import { cadastrarRespostaObjetiva } from '../services/alunoRespostaObjetivaService.js';

export function initProvaPage() {
  const tituloProva = document.getElementById('tituloProva');
  const perguntasContainer = document.getElementById('perguntasContainer');
  const botoesContainer = document.getElementById('botoesContainer');
  const btnEnviar = document.getElementById('btnEnviar');
  const timerElemento = document.getElementById('timer');

  // Só inicializa se os elementos da página Prova.html existirem
  if (!perguntasContainer || !timerElemento) {
    return;
  }

  const provaId = sessionStorage.getItem('provaId');
  const provaTitulo = sessionStorage.getItem('provaTitulo');
  const alunoIdPersistido = sessionStorage.getItem('alunoId') || localStorage.getItem('alunoId');

  // Se não houver provaId, redireciona para inserir código de acesso
  if (!provaId) {
    logInfo('UI', 'Redirecionando para código de acesso - provaId não encontrado');
    window.location.href = 'CodigoAcessoProva.html';
    return;
  }

  if (alunoIdPersistido) {
    sessionStorage.setItem('alunoId', alunoIdPersistido);
  }

  let perguntas = [];
  let totalSegundos = 60 * 60;
  let intervalo = null;
  const alunoId = alunoIdPersistido;

  const renderizarPerguntas = (lista) => {
    perguntasContainer.innerHTML = '';

    lista.forEach((p, index) => {
      const opcaoA = p.opcaoA ?? p.opcaoa ?? '';
      const opcaoB = p.opcaoB ?? p.opcaob ?? '';
      const opcaoC = p.opcaoC ?? p.opcaoc ?? '';
      const opcaoD = p.opcaoD ?? p.opcaod ?? '';

      const div = document.createElement('div');
      div.className = 'mb-4';
      div.innerHTML = `
        <p class="fw-medium">${index + 1}. ${p.titulo ?? ''}</p>
        ${p.descricao ? `<p class="text-muted small">${p.descricao}</p>` : ''}
        <div class="form-check">
          <input class="form-check-input" type="radio" name="q${p.id}" id="q${p.id}_a" value="A" required>
          <label class="form-check-label" for="q${p.id}_a">A) ${opcaoA}</label>
        </div>
        <div class="form-check">
          <input class="form-check-input" type="radio" name="q${p.id}" id="q${p.id}_b" value="B">
          <label class="form-check-label" for="q${p.id}_b">B) ${opcaoB}</label>
        </div>
        <div class="form-check">
          <input class="form-check-input" type="radio" name="q${p.id}" id="q${p.id}_c" value="C">
          <label class="form-check-label" for="q${p.id}_c">C) ${opcaoC}</label>
        </div>
        <div class="form-check">
          <input class="form-check-input" type="radio" name="q${p.id}" id="q${p.id}_d" value="D">
          <label class="form-check-label" for="q${p.id}_d">D) ${opcaoD}</label>
        </div>
        <hr>
      `;
      perguntasContainer.appendChild(div);
    });
  };

  const iniciarTimer = () => {
    intervalo = setInterval(() => {
      const minutos = Math.floor(totalSegundos / 60);
      const segundos = totalSegundos % 60;
      timerElemento.textContent = `${String(minutos).padStart(2, '0')}:${String(segundos).padStart(2, '0')}`;

      if (totalSegundos <= 0) {
        clearInterval(intervalo);
        enviarProva();
        return;
      }

      totalSegundos -= 1;
    }, 1000);
  };

  const desabilitarFormulario = () => {
    document.querySelectorAll('input[type=radio]').forEach((el) => {
      el.disabled = true;
    });

    if (btnEnviar) {
      btnEnviar.disabled = true;
    }
  };

  const reabilitarFormulario = () => {
    document.querySelectorAll('input[type=radio]').forEach((el) => {
      el.disabled = false;
    });

    if (btnEnviar) {
      btnEnviar.disabled = false;
    }
  };

  const enviarProva = async () => {
    desabilitarFormulario();
    if (intervalo) clearInterval(intervalo);

    const respostasParaEnviar = [];

    for (const pergunta of perguntas) {
      const selecionado = document.querySelector(`input[name="q${pergunta.id}"]:checked`);
      if (selecionado) {
        respostasParaEnviar.push({
          idAluno: Number(alunoId),
          idPerguntaObjetiva: pergunta.id,
          resposta: selecionado.value
        });
      }
    }

    if (respostasParaEnviar.length === 0) {
      alert('Voc� n�o respondeu nenhuma quest�o.');
      reabilitarFormulario();
      iniciarTimer();
      return;
    }

    try {
      logInfo('UI', 'Envio da prova iniciado');

      for (const resposta of respostasParaEnviar) {
        await cadastrarRespostaObjetiva(resposta);
      }

      logInfo('UI', 'Prova enviada com sucesso');
      window.location.href = 'Resultado.html';
    } catch (error) {
      logError('UI', 'Erro ao enviar prova', error);
      alert('Ocorreu um erro ao enviar a prova.');
      reabilitarFormulario();

      if (totalSegundos > 0) {
        iniciarTimer();
      }
    }
  };

  const carregarPerguntas = async () => {
    try {
      logInfo('UI', `Carregando perguntas da prova ${provaId}`);
      perguntas = await listarPerguntasPorProva(provaId);

      if (provaTitulo) {
        tituloProva.textContent = provaTitulo;
      }

      if (!perguntas || perguntas.length === 0) {
        perguntasContainer.innerHTML = '<div class="alert alert-warning">Esta prova n�o possui perguntas cadastradas.</div>';
        return;
      }

      renderizarPerguntas(perguntas);
      botoesContainer.classList.remove('d-none');
      iniciarTimer();
    } catch (error) {
      logError('UI', 'Erro ao carregar perguntas', error);
      perguntasContainer.innerHTML = '<div class="alert alert-danger">Erro ao carregar as perguntas. Verifique se a API est� ativa.</div>';
    }
  };

  if (btnEnviar) {
    btnEnviar.addEventListener('click', async () => {
      const naoRespondidas = perguntas.filter((p) => !document.querySelector(`input[name="q${p.id}"]:checked`));

      if (naoRespondidas.length > 0) {
        const confirmar = confirm(`Voc� deixou ${naoRespondidas.length} quest�o(�es) sem resposta. Deseja enviar mesmo assim?`);
        if (!confirmar) return;
      }

      await enviarProva();
    });
  }

  if (tituloProva && provaTitulo) {
    tituloProva.textContent = provaTitulo;
  }

  carregarPerguntas();
}