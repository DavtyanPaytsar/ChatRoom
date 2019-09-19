
let currentRoomId = null;

function getMessagesByRoomId(room_id) {

    $("#currentRoom").val(room_id);
    currentRoomId = room_id;

    $.ajax({
        url: '/Message/GetByRoomId',
        type: 'GET',
        async: true,
        data: {
            roomId: room_id
        },
        success: function (data) {
            $('#msgs').html('');
            data.forEach(function (data) {

                var userName = $('#currentUser').val();

                var sender = data.userId;
                var message = data.userId + ":  " + data.content;

                if (sender == userName) {
                    $('#msgs').append('<div class="row chat_my_message wrapContainer"><b>' +
                        message + '</b>' + '<br />' + '</div>');
                }
                else {
                    $('#msgs').append('<div class="row chat_others_message wrapContainer"><b>' +
                        message + '</b>' + '<br />' + '</div>');
                }

                //$('#msgs').append(data.userId + ":  " + data.content + '<br />');
            });
        },
        error: function (data) {
            console.log(data);
        }
    });
}

$("#CreateNewRoomButton").click(function () {

    var model = {
        Name: $("#roomName").val()
    };

    $.ajax({
        url: '/Room/Create',
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(model),
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            $('#myModal').modal('hide');
            location.reload();
        },
        error: function (data) {
            console.log(data);
        }
    });

});

$("#rooms").on("click", ".room", function () {
    let room_id = $(this).attr("data-group_id");

    $('.room').css({ "border-style": "none", cursor: "pointer" });
    $(this).css({ "border-style": "inset", cursor: "default" });

    getMessagesByRoomId(room_id);
});

$("#rooms").on("click", ".roomDelete", function () {
    let room_id = $(this).attr("data-group_id");

    $.ajax({
        url: '/Room/Delete',
        type: 'DELETE',
        async: true,
        data: {
            roomId: room_id
        },
        success: function (data) {
            location.reload();
        },
        error: function (data) {
            console.log(data);
        }
    });

});

$(function () {

    var userName = $('#currentUser').val();

    if (currentRoomId === null && userName != null) {

        $.ajax({
            url: '/Room/GetAll',
            type: 'GET',
            async: true,
            success: function (data) {
                $('#rooms').html('');
                data.forEach(function (data) {

                    let room = "";
                    if (data.name === "Master Room") {
                        room = '<div class="row top-space">' +
                            '<div class="col-sm-8">' +
                            '<div class="room selectedRoom" data-group_id=' + data.id + '>' + data.name + '</div>' +
                            '</div>' +
                            '</div>';

                        getMessagesByRoomId(data.id);
                    }
                    else {
                        room = '<div class="row top-space">' +
                            '<div class="col-sm-8">' +
                            '<div class="room" data-group_id=' + data.id + '>' + data.name + '</div>' +
                            '</div>' +
                            '<div class="col-sm-2">' +
                            '<div class="roomDelete glyphicon glyphicon-remove" data-group_id=' + data.id + '>' + '</div>' +
                            '</div>' +
                            '</div>';
                    }

                    $('#rooms').append(room);
                });
            },
            error: function (data) {
                console.log(data);
            }
        });
    }

    var protocol = location.protocol === "https:" ? "wss:" : "ws:";
    var wsUri = protocol + "//" + window.location.host;
    var socket = new WebSocket(wsUri);
    socket.onopen = e => {
        console.log("socket opened", e);
    };

    socket.onclose = function (e) {
        console.log("socket closed", e);
    };

    socket.onmessage = function (e) {
        console.log(e);
        var message = e.data.split(";")[0];
        var roomId = e.data.split(";")[1];
        var sender = e.data.split(":")[0];

        if (roomId == currentRoomId) {
            if (sender == userName) {
                $('#msgs').append('<div class="row chat_my_message wrapContainer"><b>' +
                    message + '</b>' + '<br />' + '</div>');
            }
            else {
                $('#msgs').append('<div class="row chat_others_message wrapContainer"><b>' +
                    message + '</b>' + '<br />' + '</div>');
            }
        }
    };

    socket.onerror = function (e) {
        console.error(e.data);
    };

    $('#messageContent').keypress(function (e) {
        if (e.which !== 13 || !$('#messageContent').val()) {
            return;
        }

        e.preventDefault();

        var messageContent = $('#messageContent').val();
        var newMessage = {
            Content: messageContent,
            RoomId: currentRoomId
        };

        $.ajax({
            url: '/Message/Create',
            dataType: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(newMessage),
            async: true,
            processData: false,
            cache: false,
            success: function (e) {
                var message = userName + ":  " + messageContent + ";" + currentRoomId;
                socket.send(message);
                $('#messageContent').val('');
            },
            error: function (data) {
                console.log(data);
            }
        });
    });
});
