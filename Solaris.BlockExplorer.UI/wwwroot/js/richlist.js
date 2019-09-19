function setPage(page) {
    $("#pageNumber").val(page);
    $("#pagingForm").submit();
    return false;
}
function setPageSize(obj) {
    $("#pageNumber").val(1);
    $("#pageSize").val($(obj).val());
    $("#pagingForm").submit();
    return false;
}

$(document).ready(function () {
    $("#pageSizeControl").val($("#pageSize").val());
    loadWealthDataChart();
});

function loadWealthDataChart() {
    $.post(_endpoints.wealthChartData, function (data) {

        for (let i = 1; i <= 4; i++) {
            $(`#top${i}`).html(data[`top${i}`]);
            $(`#top${i}amount`).html(data[`top${i}amount`]);
        }

        const ctx = document.getElementById("wealthChart").getContext("2d");
        const chartData = {
            labels: [
                `Top 1-25 | ${data.top1amount} ${_settings.ticker} | %`,
                `Top 26-50 | ${data.top2amount} ${_settings.ticker} | %`,
                `Top 51-75 | ${data.top3amount} ${_settings.ticker} | %`,
                `Top >76 | ${data.top4amount} ${_settings.ticker} | %`],
            datasets: [
                {
                    label: "Percentage",
                    data: [data.top1, data.top2, data.top3, data.top4],
                    backgroundColor: [
                        "rgba(255, 99, 132, 0.5)",
                        "rgba(54, 162, 235, 0.2)",
                        "rgba(255, 206, 86, 0.2)",
                        "rgba(75, 192, 192, 0.2)"
                    ],
                    borderColor: [
                        "rgba(255,99,132,1)",
                        "rgba(54, 162, 235, 1)",
                        "rgba(255, 206, 86, 1)",
                        "rgba(75, 192, 192, 1)"
                    ],
                    borderWidth: 1
                }
            ]
        };

        var wealthChart = new Chart(ctx,
            {
                type: 'doughnut',
                data: chartData,
                options: {
                    responsive: true,
                    legend: {
                        display: false
                    }
                }
            });

        $("#distribution-loading").hide("slow", function() {
            $("#distribution").collapse("show");
        });
    });
}