$(document).ready(function () {
    let hubUrl = 'https://localhost:44382/archive';
    const hubConnection = new signalR.HubConnectionBuilder()
        .withUrl(hubUrl)
        .build();
    // получение сообщения от сервера
    hubConnection.on('Notify', function (message, percent) {
        document.getElementById(message).textContent = percent;
    });

    hubConnection.start();

    $('#start').click(function (e) {
        e.preventDefault();
        $.ajax({
            url: '/Archiving/StartArchive',
            type: 'post',
            data: {
                directoryName: "@Model.DirectoryName"
            },
            success: function () {
            }
        });
    });

    $('#stop').click(function (e) {
        $.ajax({
            url: '/Archiving/StopArchive',
            type: 'get',
            success: function () {
                $.getElementsByClassName("percent").textContent = "";
            }
        });
    });
});