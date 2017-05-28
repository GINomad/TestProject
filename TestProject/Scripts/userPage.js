$(document).ready(function () {
    var chat = $.connection.userHub;
    $.connection.hub.start().done(function () {
        var name = $("#userName").text();
        chat.server.connect();
    });
})