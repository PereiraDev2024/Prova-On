function carregarmenu() {

    fetch('Menu.html')
    .then(response => response.text())
    .then(html => {

        document.getElementById("menu").innerHTML = html;


    })
    .catch(error =>{
        console.log("Algo de inesperado aconteceu");
    })
}


function carregaroffcanvas(){

    fetch('offcanvas.html')
    .then(response => response.text())
    .then(html => {

        document.getElementById('offcanvas').innerHTML = html;

    })
    .catch(error =>{

        console.log("Algo de inesperado aconteceu");

    })

}



function carregaroffcanvasadm(){

    fetch('offcanvasadm.html')
    .then(response => response.text())
    .then(html => {

        document.getElementById('offcanvasadm').innerHTML = html;

    })
    .catch(error =>{

        console.log("Algo de inesperado aconteceu");

    })

}



carregarmenu();
carregaroffcanvas();
carregaroffcanvasadm();