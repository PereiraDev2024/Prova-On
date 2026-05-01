import { logApi, logError } from '../utils/logger.js';

function getApiBaseUrl() {
  const meta = document.querySelector('meta[name="api-base-url"]');
  const baseUrl = meta?.content?.trim();

  if (!baseUrl) {
    throw new Error('Meta api-base-url não configurada.');
  }

  return baseUrl.replace(/\/$/, '');
}

async function request(method, endpoint, data = null) {
  const url = `${getApiBaseUrl()}${endpoint}`;

  logApi(method, endpoint, 'INICIADA');

  const options = {
    method,
    headers: {
      'Accept': 'application/json'
    },
    credentials: 'include'
  };

  if (data != null) {
    options.headers['Content-Type'] = 'application/json';
    options.body = JSON.stringify(data);
  }

  try {
    const res = await fetch(url, options);
    const contentType = res.headers.get('content-type') || '';
    let body = null;
    if (contentType.includes('application/json')) {
      body = await res.json();
    } else {
      body = await res.text();
    }

    logApi(method, endpoint, res.status);

    if (!res.ok) {
      const err = new Error('HTTP error ' + res.status);
      err.response = { status: res.status, data: body };
      logError('API', `Falha na requisição ${method} ${endpoint}`, err);
      throw err;
    }

    return body;
  } catch (error) {
    const status = error?.response?.status ?? 'ERRO';
    logApi(method, endpoint, status);
    logError('API', `Falha na requisição ${method} ${endpoint}`, error);
    throw error;
  }
}

export const get = (endpoint) => request('GET', endpoint);
export const post = (endpoint, data) => request('POST', endpoint, data);
export const put = (endpoint, data) => request('PUT', endpoint, data);
export const del = (endpoint) => request('DELETE', endpoint);