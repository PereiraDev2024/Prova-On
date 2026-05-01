import { logInfo, logError } from '../utils/logger.js';
import { buscarProva } from '../services/provaService.js';

export function initCorrigirProvaPage() {
  const formCorrecao = document.getElementById('formCorrecao');
  const provaInfo = document.getElementById('provaInfo');
  const perguntasContainer = document.getElementById('perguntasContainer');

  if (!formCorrecao || !provaInfo || !perguntasContainer) return;

  const params = new URLSearchParams(window.location.search);
  const provaId = params.get('id');

  if (!provaId) {
    alert('ID da prova não fornecido.');
    return;
  }

  function mostrarProva(prova) {
    provaInfo.innerHTML = `
      <h4>${prova.titulo ?? 'Prova'}</h4>
      <p><strong>Data de Registro:</strong> ${
        prova.dataHoraRegistro ? new Date(prova.dataHoraRegistro).toLocaleString('pt-BR') : '-'
      }</p>
    `;
  }

  function mostrarPerguntas(prova) {
    perguntasContainer.innerHTML = '';

    if (!prova?.perguntas || prova.perguntas.length === 0) {
      perguntasContainer.innerHTML = '<div class="alert alert-warning">Nenhuma pergunta encontrada para correção.</div>';
      return;
    }

    prova.perguntas.forEach((pergunta, index) => {
      const campoNota = `
        <div class="mt-2">
          <label for="nota-${index}" class="form-label">Nota</label>
          <input type="number" class="form-control" id="nota-${index}" min="0" max="10" step="0.5">
        </div>
      `;

      const card = document.createElement('div');
      card.className = 'card mb-3';

      if (pergunta.tipo === 'objetiva') {
        card.innerHTML = `
          <div class="card-header"><strong>Pergunta ${index + 1} (Objetiva)</strong></div>
          <div class="card-body">
            <p><strong>Enunciado:</strong> ${pergunta.titulo ?? ''}</p>
            <ul>
              <li><strong>A:</strong> ${pergunta.opcaoA ?? ''}</li>
              <li><strong>B:</strong> ${pergunta.opcaoB ?? ''}</li>
              <li><strong>C:</strong> ${pergunta.opcaoC ?? ''}</li>
              <li><strong>D:</strong> ${pergunta.opcaoD ?? ''}</li>
            </ul>
            <p><strong>Resposta Correta:</strong> ${pergunta.respostaCorreta ?? '-'}</p>
            ${campoNota}
          </div>
        `;
      } else {
        card.innerHTML = `
          <div class="card-header"><strong>Pergunta ${index + 1} (Discursiva)</strong></div>
          <div class="card-body">
            <p><strong>Enunciado:</strong> ${pergunta.titulo ?? ''}</p>
            ${campoNota}
          </div>
        `;
      }

      perguntasContainer.appendChild(card);
    });
  }

  async function carregarDados() {
    try {
      logInfo('UI', `Carregando prova para correção: ${provaId}`);
      const prova = await buscarProva(provaId);

      mostrarProva(prova);
      mostrarPerguntas(prova);
    } catch (error) {
      logError('UI', 'Erro ao carregar dados da prova', error);
      alert('Falha ao carregar dados.');
    }
  }

  formCorrecao.addEventListener('submit', (event) => {
    event.preventDefault();
    logInfo('UI', 'Correção enviada (modo simulado)');
    alert('Correção salva (simulado).');
  });

  carregarDados();
}