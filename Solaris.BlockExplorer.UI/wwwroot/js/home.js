function loadMarketData() {
    $.post(_endpoints.marketData, function(result) {
        $("#marketData").html(result);
    });
}

$(document).ready(function() {
    loadMarketData();
});