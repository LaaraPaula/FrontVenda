//function fetchApiData() {
//    fetch('http://172.19.99.124:5000/controller/exibeclientes')
//        .then(response => response.json())
//        .then(data => {
//            const list = document.querySelector(".lista")

//            data.map((item) => {
//                const li = document.createElement('li');
//                li.setAttribute('id', item.Id);
//                li.innerHTML = item.Nome;
//                list.appendChild(li);
//            })
//        })
        
//}

//function fazGet(url) {
//    var request = new XMLHttpRequest();
//    request.open("GET", url, false);
//    request.send();
//    return request.responseText;
//}
//function criaLinha(usuario) {
//    linha = document.createElement("tr");

//    tdId = document.createElement("td");
//    tdNome = document.createElement("td");
//    tdCpf = document.createElement("td");
//    tdTelefone = document.createElement("td");
//    tdEndereço = document.createElement("td");

//    tdId.innerHtml = usuario.Id;
//    tdNome.innerHtml = usuario.Nome;
//    tdCpf.innerHtml = usuario.CPF;
//    tdTelefone.innerHtml = usuario.CPF;
//    tdEndereço.innerHtml = usuario.CPF;

//    linha.appendChild(tdId);
//    linha.appendChild(tdNome);
//    linha.appendChild(tdCpf);
//    linha.appendChild(tdTelefone);
//    linha.appendChild(tdEndereço);

//    return linha;
//}
//function main() {
//    let data = fazGet("http://172.19.99.124:5000/controller/exibeclientes");
//    console.log(data);
//    let usuarios = JSON.parse(data);
//    let tabela = document.getElementsByClassName("lista");
//    usuarios.forEach(element => {
//        let linha = criaLinha(element);
//        tabela.appendChild(linha);
//    });
//}

fetchApiData();



//function main() {
//    (async function () {
//        try {
//            const response = await fetch('http://172.19.99.124:5000/controller/exibeclientes');
//            const jsonData = await response.json();
//        }
//        catch (e) {
//            console.log(e);
//        }

//        let tabela = document.getElementsByClassName("lista");

//        jsonData.forEach(element => {
//            let linha = criaLinha(element);
//            tabela.appendChild(linha);
//        });
//    });
//}