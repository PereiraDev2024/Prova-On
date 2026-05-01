export function initLayout() {
  carregarmenu();
  carregaroffcanvas();
  carregaroffcanvasadm();
  carregaroffcanvasaluno();
}

function carregarmenu() {
  fetch('Menu.html')
    .then(response => response.text())
    .then(html => {
      const el = document.getElementById('menu');
      if (el) el.innerHTML = html;
    })
    .catch(() => {
      console.log('Erro ao carregar Menu.html');
    });
}

function carregaroffcanvas() {
  fetch('offcanvasprofessor.html')
    .then(response => response.text())
    .then(html => {
      const el = document.getElementById('offcanvas');
      if (el) el.innerHTML = html;
    })
    .catch(() => {
      console.log('Erro ao carregar offcanvasprofessor.html');
    });
}

function carregaroffcanvasadm() {
  fetch('offcanvasadm.html')
    .then(response => response.text())
    .then(html => {
      const el = document.getElementById('offcanvasadm');
      if (el) el.innerHTML = html;
    })
    .catch(() => {
      console.log('Erro ao carregar offcanvasadm.html');
    });
}

function carregaroffcanvasaluno() {
  fetch('offcanvasaluno.html')
    .then(response => response.text())
    .then(html => {
      const el = document.getElementById('offcanvasaluno');
      if (el) el.innerHTML = html;
    })
    .catch(() => {
      console.log('Erro ao carregar offcanvasaluno.html');
    });
}
