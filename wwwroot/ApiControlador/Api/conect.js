/* 
$(document).ready(function () {
    $('#formulario_activacion').on('submit', function (evt) {
        evt.preventDefault();
        alert('Prevenido');
        var dataToSend = '{login: "admin",password: "admin"}';
        var xhr = new XMLHttpRequest();
        xhr.open('POST', 'http://192.168.88.129/login.fcgi');
        xhr.setRequestHeader('Content-type', 'application/json');
        xhr.setRequestHeader("Access-Control-Allow-Origin", "*");
        xhr.setRequestHeader("Access-Control-Allow-Credentials", "true");
        xhr.setRequestHeader("Access-Control-Allow-Methods", "GET,HEAD,OPTIONS,POST,PUT");
        xhr.setRequestHeader("Access-Control-Allow-Headers", "Access-Control-Allow-Headers, Origin,Accept, X-Requested-With, Content-Type, Access-Control-Request-Method, Access-Control-Request-Headers");
        xhr.send(dataToSend);
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                console.log(this.responseText);
            }
        };
    });
}); */
$(document).on('click', '.guardar', function (evt) {
    alert('Prevenido');
    var dataToSend = '{"login":"admin","password":"admin"}';
    var xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://192.168.88.129/login.fcgi');
    xhr.setRequestHeader('Content-Security-Policy', 'upgrade-insecure-requests');
    xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    xhr.send(JSON.stringify({ "login": "admin", "password": "admin" }));
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            console.log(this.responseText);
        }
    };
});