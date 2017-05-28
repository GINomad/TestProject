$(document).ready(function () {
    var chat = $.connection.userHub;
    chat.client.pageReload = function () {
        $.ajax({
            type: "POST",
            url: "/Account/LogOff",
            datatype: "html",            
        }).done(function () { location.reload()});
    }
});