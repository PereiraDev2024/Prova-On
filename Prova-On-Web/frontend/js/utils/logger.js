function formatTimestamp() {
  const now = new Date();
  const pad = (value) => String(value).padStart(2, '0');
  return `${now.getFullYear()}-${pad(now.getMonth() + 1)}-${pad(now.getDate())} ${pad(now.getHours())}:${pad(now.getMinutes())}`;
}

function normalizeMessage(message) {
  if (message instanceof Error) return message.message;
  if (typeof message === 'string') return message;
  return JSON.stringify(message);
}

export function logInfo(origem, mensagem) {
  console.log(`[INFO] [${formatTimestamp()}] [${origem}] ${normalizeMessage(mensagem)}`);
}

export function logError(origem, mensagem, erro = null) {
  const detalhes = erro ? ` | ${normalizeMessage(erro)}` : '';
  console.error(`[ERROR] [${formatTimestamp()}] [${origem}] ${normalizeMessage(mensagem)}${detalhes}`);
}

export function logApi(metodo, endpoint, status) {
  console.log(`[API] [${formatTimestamp()}] [${String(metodo).toUpperCase()}] ${endpoint} - ${status}`);
}