function carregarmenu() {

    fetch('Menu.html')
    .then(response => response.text())
    .then(html => {

        document.getElementById("menu").innerHTML = html;


    })
    .catch(error =>{
        console.log("Algo de inesperado aconteceu menu");
    })
}


function carregaroffcanvas(){

    fetch('offcanvasprofessor.html')
    .then(response => response.text())
    .then(html => {

        document.getElementById('offcanvas').innerHTML = html;

    })
    .catch(error =>{

        console.log("Algo de inesperado aconteceu offcanvasprofessor");

    })

}



function carregaroffcanvasadm(){

    fetch('offcanvasadm.html')
    .then(response => response.text())
    .then(html => {

        document.getElementById('offcanvasadm').innerHTML = html;

    })
    .catch(error =>{

        console.log("Algo de inesperado aconteceu offcanvasadm");

    })

}

function carregaroffcanvasaluno(){

    fetch('offcannvasaluo.html')
    .then(response => response.text())
    .then(html => {
        document.getElementById('offcanvasaluno').innerHTML = html;
    })
    .catch(error => {

        console.log("Algo de inesperado aconteceu offcanvasaluno");

    })
}


carregarmenu();
carregaroffcanvas();
carregaroffcanvasadm();
carregaroffcanvasaluno();