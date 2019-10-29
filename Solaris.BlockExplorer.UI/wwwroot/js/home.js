function loadMarketData() {
    $.post(_endpoints.marketData, function (result) {
        $("#marketData").html(result);
    });
}
function connectFeed() {
    const connection = new window.signalR.HubConnectionBuilder()
        .withUrl("/wsbn")
        .configureLogging(window.signalR.LogLevel.Error)
        .build();

    connection.on("wsbn",
        (message) => {
            var element = $("#lastBlockHeight");
            element.html(message.height);

            highLight(element);

            $.post(_endpoints.block, { height: message.height }, function (block) {
                var row = $.parseHTML(block);
                $("#rows").prepend(row);
                highLight(row);
            });
        });
    startFeed(connection);
}

function startFeed(connection) {
    connection.onclose(() => {
        console.log("Websocket closed, restarting...");
        setTimeout(() => startFeed(connection), 5000);
    });

    if (connection.connection.connectionState === 2) {
        connection.start().catch(err => {
            console.log(`Problem starting websocket connection ${err.toString()}`);
            setTimeout(() => this.Start(connection), 5000);
        }).then(() => {
            console.log("Websocket started");
        });
    }
}

function highLight(element) {
    $(element).addClass("text-white");
    $(element).finish().effect("highlight",
        { color: "#00AE52" },
        1000,
        function () {
            $(element).removeClass("text-white");
        });
}

$(document).ready(function () {
    loadMarketData();
    connectFeed();
});