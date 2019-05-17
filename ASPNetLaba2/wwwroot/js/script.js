/*jshint esversion: 6 */
const uri = "/api/zapis/";
let items = null;

document.addEventListener("DOMContentLoaded", function (event) {
    loadZapis();
    getCurrentUser();
});

function loadZapis() {
    var i, j, x = "";
    var request = new XMLHttpRequest();
    request.open("GET", uri, false);
    request.onload = function () {
        items = JSON.parse(request.responseText);
        x += "<div class='album py-5'>";
        x += "<div class='container'>";
        x += "<div class='row'>";
        for (i in items) {   
            x += "<div class='col-md-4'";
            //x += "<hr>"; 
            x += "<h6>Запись номер: " + items[i].zapisId + "</h6>";
            //x += "<h6>URL: " + items[i].url + "</h6>";
            x += "<h6>Дата приема: " + items[i].zapis_date + "</h6>";
            x += "<h6>Время приема: " + items[i].zapis_time + "</h6>";
            x += "<h6>Кабинет: " + items[i].kabinet + "</h6>";          
            

            for (j in items[i].pacient) {
                x += "<div class='col-10'>";
                x += "<h6>ФИО пациента: " + items[i].pacient[j].pacient_FIO + "<h6>";
                x += "<h6>Номер полиса: " + items[i].pacient[j].polis_number + "<h6>";
                x += "<h6>Адрес: " + items[i].pacient[j].adres + "</h6>";
                x += "</div>";

            }         
            x += "<button type='button' class='btn btn-outline-danger' onclick='deleteZapis(" + items[i].zapisId + ");'>Удалить</button>";
            x += "<a href='#scroll' class='btn btn-outline-dark' onclick='editZapis(" + items[i].zapisId + ");'>Изменить</a>";
            x += "</div>";
            x += "<hr>";
            x += "<hr>";
           
        }
        x += "</div>";
        x += "</div>";
        x += "</div>";
        document.getElementById("zapisDiv").innerHTML = x;
    };
    request.send();
}

//LogCheck
function getCurrentUser() {
    let request = new XMLHttpRequest();
    request.open("POST", "/api/Account/isAuthenticated", true);
    request.onload = function () {
        let myObj = "";
        myObj = request.responseText !== "" ?
            JSON.parse(request.responseText) : {};
        document.getElementById("msg").innerHTML = myObj.message;
    };
    request.send();
}

//scroll to destination
$(document).ready(function () {
    $("a").click(function () {
        var elementClick = $(this).attr("href");
        var destination = $(elementClick).offset().top;
        if ($.browser.safari) {
            $('body').animate({ scrollTop: destination }, 1);
            $('html').animate({ scrollTop: destination }, 1);
        }
        return false;
    });
});


function deleteZapis(id) {
    var request = new XMLHttpRequest();
    var url = uri + id;
    request.open("DELETE", url, false);
    request.onload = function () {
        // Обработка кода ответа
        var msg = "";
        if (request.status == 401) {
            alert("У вас не хватает прав для удаления");
        } else if (request.status == 204) {
            alert("Запись удалена");
            loadZapis();
        } else {
            alert("Неизвестная ошибка");
        }
        document.querySelector("#actionMsg").innerHTML = msg;
    };
    request.send();
}

//заполнение полей для изменения
function editZapis(id) {
    let elm = document.querySelector("#editDiv");
    elm.style.display = "block";
    if (items) {
        let i;
        for (i in items) {
            if (id == items[i].zapisId) {
                document.querySelector("#edit-id").value = items[i].zapisId;
                //document.querySelector("#edit-url").value = items[i].url;
                document.querySelector("#edit-date").value = items[i].zapis_date;
                document.querySelector("#edit-time").value = items[i].zapis_time;
                document.querySelector("#edit-kabinet").value = items[i].kabinet;
            }
            for (j in items[i].pacient) {
                if (id == items[i].pacient[j].zapisId) {
                    document.querySelector("#edit-fio").value = items[i].pacient[j].pacient_FIO;
                    document.querySelector("#edit-polis").value = items[i].pacient[j].polis_number;
                    document.querySelector("#edit-adres").value = items[i].pacient[j].adres;
                }
            }
        }
    }
}

function updateZapis() {
    const zapis = {
        zapisId: document.querySelector("#edit-id").value,
        //url: document.querySelector("#edit-url").value,
        zapis_date: document.querySelector("#edit-date").value,
        zapis_time: document.querySelector("#edit-time").value,
        kabinet: document.querySelector("#edit-kabinet").value,
        pacient: [{
            pacient_FIO: document.querySelector("#edit-fio").value,
            polis_number: document.querySelector("#edit-polis").value,
            adres: document.querySelector("#edit-adres").value
        }]
    };
    var request = new XMLHttpRequest();
    request.open("PUT", uri + zapis.zapisId);
    request.onload = function () {
        // Обработка кода ответа
        var msg = "";
        if (request.status == 401) {
            alert("У вас не хватает прав для изменения");
        } else  {
            alert("Запись изменена");
            loadZapis();
        } 
        document.querySelector("#actionMsg").innerHTML = msg;
    }
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    request.send(JSON.stringify(zapis));
}

function createZapis() {
    var urlText = document.getElementById("createURL").value;
    var ZapisDateText = document.getElementById("createDate").value;
    var KabinetText = document.getElementById("createKabinet").value;
    var ZapisTimeText = document.getElementById("createTime").value;
    var PacientFioText = document.getElementById("createFIO").value;
    var PolisNumberText = document.getElementById("createPolis").value;
    var AdresText = document.getElementById("createAdres").value;
    var request = new XMLHttpRequest();
    request.open("POST", uri);
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    request.onload = function () {
            alert("Запись добавлена");
            loadZapis();
    };
    request.send(JSON.stringify({
        url: urlText,
        zapis_date: ZapisDateText,
        zapis_time: ZapisTimeText,
        kabinet: KabinetText,
        pacient: [{
            pacient_FIO: PacientFioText,
            polis_number: PolisNumberText,
            adres: AdresText
        }]
    }));
}

function closeInput() {
    let elm = document.querySelector("#editDiv");
    elm.style.display = "none";
}

function logIn() {
    var email, password = "";
    // Считывание данных с формы
    email = document.getElementById("Email").value;
    password = document.getElementById("Password").value;
    var request = new XMLHttpRequest();
    request.open("POST", "/api/Account/Login");
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    request.onreadystatechange = function () {
        // Очистка контейнера вывода сообщений
        document.getElementById("msg").innerHTML = "";
        var mydiv = document.getElementById("formError");
        while (mydiv.firstChild) {
            mydiv.removeChild(mydiv.firstChild);
        }
        // Обработка ответа от сервера
        if (request.responseText !== "") {
            var msg = null;
            msg = JSON.parse(request.responseText);
            document.getElementById("msg").innerHTML = msg.message;
            // Вывод сообщений об ошибках
            if (typeof msg.error !== "undefined" && msg.error.length >0) {
                for (var i = 0; i < msg.error.length; i++) {
                    var ul = document.getElementsByTagName("ul");
                    var li = document.createElement("li");
                    li.appendChild(document.createTextNode(msg.error[i]));
                    ul[0].appendChild(li);
                }
            }
            document.getElementById("Password").value = "";
        }
        
    };
    // Запрос на сервер
    request.send(JSON.stringify({
        email: email,
        password: password
    }));

    $('#exampleModal').modal('hide');
}

function logOff() {
    var request = new XMLHttpRequest();
    request.open("POST", "api/account/logoff");
    request.onload = function () {
        var msg = JSON.parse(this.responseText);
        document.getElementById("msg").innerHTML = "";
        var mydiv = document.getElementById('formError');
        var mydiv = document.getElementById('formError');
        while (mydiv.firstChild) {
            mydiv.removeChild(mydiv.firstChild);
        }
        document.getElementById("msg").innerHTML = msg.message;
    };
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    request.send();
}
