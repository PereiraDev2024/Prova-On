import { logInfo, logError } from '../utils/logger.js';
import { cadastrarProva } from '../services/provaService.js';
import { cadastrarPerguntaObjetiva } from '../services/perguntaObjetivaService.js';

export function initCadastrarProvaPage() {
  const formProva = document.getElementById('formProva');
  const formObjetiva = document.getElementById('formObjetiva');
  const formDiscursiva = document.getElementById('formDiscursiva');
  const listaPerguntas = document.getElementById('listaPerguntas');
  const btnEnviarProva = document.getElementById('button_enviarProva');

  if (!formProva || !formObjetiva || !formDiscursiva || !listaPerguntas || !btnEnviarProva) return;

  const perguntas = [];
  const professor = JSON.parse(localStorage.getItem('professorLogado') || 'null');

  if (!professor?.id) {
    alert('Professor n�o autenticado. Fa�a login novamente.');
    return;
  }

  const adicionarPerguntaNaTela = (tipo, dados) => {
    perguntas.push({ tipo, ...dados });

    const item = document.createElement('li');
    item.className = 'list-group-item';

    if (tipo === 'objetiva') {
      item.innerHTML = `
        <strong>Objetiva:</strong> ${dados.enunciado}<br>
        A) ${dados.altA}<br>
        B) ${dados.altB}<br>
        C) ${dados.altC}<br>
        D) ${dados.altD}<br>
        <strong>Correta:</strong> ${dados.correta}
      `;
    } else {
      item.innerHTML = `
        <strong>Discursiva:</strong> ${dados.enunciado}<br>
        ${dados.respostaModelo ? `<em>Resposta Modelo:</em> ${dados.respostaModelo}` : ''}
      `;
    }

    listaPerguntas.appendChild(item);
  };

  formObjetiva.addEventListener('submit', (event) => {
    event.preventDefault();

    const enunciado = document.getElementById('enunciadoObjetiva')?.value.trim();
    const altA = document.getElementById('altA')?.value.trim();
    const altB = document.getElementById('altB')?.value.trim();
    const altC = document.getElementById('altC')?.value.trim();
    const altD = document.getElementById('altD')?.value.trim();
    const correta = document.getElementById('respostaCorreta')?.value;

    if (!enunciado || !altA || !altB || !altC || !altD || !correta) {
      alert('Preencha todos os campos.');
      return;
    }

    adicionarPerguntaNaTela('objetiva', { enunciado, altA, altB, altC, altD, correta });
    formObjetiva.reset();
  });

  formDiscursiva.addEventListener('submit', (event) => {
    event.preventDefault();

    const enunciado = document.getElementById('enunciadoDiscursiva')?.value.trim();
    const respostaModelo = document.getElementById('respostaModelo')?.value.trim();

    if (!enunciado) {
      alert('Preencha o enunciado.');
      return;
    }

    adicionarPerguntaNaTela('discursiva', { enunciado, respostaModelo });
    formDiscursiva.reset();
  });

  btnEnviarProva.addEventListener('click', async (event) => {
    event.preventDefault();

    const nomeProva = document.getElementById('nomeProva')?.value.trim();
    const dataProva = document.getElementById('dataProva')?.value;
    const disciplina = document.getElementById('disciplina')?.value;

    if (!nomeProva || !dataProva || !disciplina) {
      alert('Preencha todos os dados da prova.');
      return;
    }

    const dataHora = new Date().toISOString();

    const provaPayload = {
      id: 0,
      titulo: nomeProva,
      dataHoraInicio: dataHora,
      dataHoraFim: dataHora,
      dataHoraRegistro: dataHora,
      codigo: Math.random().toString(36).substring(2, 8).toUpperCase(),
      idProfessor: professor.id
    };

    try {
      logInfo('UI', 'Cadastro de prova iniciado');
      const provaResposta = await cadastrarProva(provaPayload);
      const idProva = provaResposta?.id || 0;

      for (const pergunta of perguntas) {
        if (pergunta.tipo === 'objetiva') {
          const objetivaPayload = {
            id: 0,
            titulo: pergunta.enunciado,
            descricao: 'Pergunta objetiva',
            opcaoA: pergunta.altA,
            opcaoB: pergunta.altB,
            opcaoC: pergunta.altC,
            opcaoD: pergunta.altD,
            opcaoCorreta: pergunta.correta,
            pontuacao: 1,
            dataHoraRegistro: dataHora,
            idProva
          };

          await cadastrarPerguntaObjetiva(objetivaPayload);
        }
      }

      logInfo('UI', 'Prova cadastrada com sucesso');
      alert('Prova cadastrada com sucesso!');
      formProva.reset();
      listaPerguntas.innerHTML = '';
      perguntas.length = 0;
    } catch (error) {
      logError('UI', 'Erro ao cadastrar prova', error);
      alert('Erro ao cadastrar prova.');
    }
  });
}