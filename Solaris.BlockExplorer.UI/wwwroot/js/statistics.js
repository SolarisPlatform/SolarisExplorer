function loadDifficultyData() {
    $.post(_endpoints.difficultyChartData,
        function (data) {
            const quotes = $.map(data,
                function (quote) {
                    return quote.difficulty;
                });
            const labels = $.map(data,
                function (quote) {
                    return `Block ${quote.height} - ${new moment(quote.time).format("MMM Do, HH:mm:ss")}`;
                });
            const chart = new Chart(document.getElementById("difficultyChart"),
                {
                    type: "line",
                    data: {
                        labels: labels,
                        datasets: [
                            {
                                data: quotes,
                                backgroundColor: [
                                    "rgba(56, 127, 225, 0.15)"
                                ],
                                borderColor: [
                                    "rgba(56, 127, 225, 1)"
                                ],
                                borderWidth: 1
                            }
                        ]
                    },
                    options: {
                        scales: {
                            yAxes: [
                                {
                                    ticks: {
                                        beginAtZero: false
                                    },
                                    display: true
                                }
                            ],
                            xAxes: [
                                {
                                    ticks: {
                                        display: true
                                    },
                                    display: false
                                }
                            ]
                        },
                        legend: {
                            display: false
                        },
                        responsive: true
                    }
                });
            $("#difficultyChartLoading").hide("slow", function() {
                $("#difficultyChart").collapse("show");
            });
        });
}
function loadTransactionCountData() {
    $.post(_endpoints.transactionCountChartData,
        function (data) {
            const quotes = $.map(data,
                function (quote) {
                    return quote.transactionCount;
                });
            const labels = $.map(data,
                function (quote) {
                    return `Block ${quote.height}`;
                });
            const chart = new Chart(document.getElementById("transactionCountChart"),
                {
                    type: "line",
                    data: {
                        labels: labels,
                        datasets: [
                            {
                                data: quotes,
                                backgroundColor: [
                                    "rgba(56, 127, 225, 0.15)"
                                ],
                                borderColor: [
                                    "rgba(56, 127, 225, 1)"
                                ],
                                borderWidth: 1
                            }
                        ]
                    },
                    options: {
                        scales: {
                            yAxes: [
                                {
                                    ticks: {
                                        beginAtZero: false
                                    },
                                    display: true
                                }
                            ],
                            xAxes: [
                                {
                                    ticks: {
                                        display: true
                                    },
                                    display: false
                                }
                            ]
                        },
                        legend: {
                            display: false
                        },
                        responsive: true
                    }
                });
            $("#transactionCountChartLoading").hide("slow", function() {
                $("#transactionCountChart").collapse("show");
            });
        });
}
function loadBlockSizeData() {
    $.post(_endpoints.blockSizeChartData,
        function (data) {
            const quotes = $.map(data,
                function (quote) {
                    return quote.size;
                });
            const labels = $.map(data,
                function (quote) {
                    return `Block ${quote.height} - ${quote.size} KB`;
                });
            const chart = new Chart(document.getElementById("blockSizeChart"),
                {
                    type: "line",
                    data: {
                        labels: labels,
                        datasets: [
                            {
                                data: quotes,
                                backgroundColor: [
                                    "rgba(56, 127, 225, 0.15)"
                                ],
                                borderColor: [
                                    "rgba(56, 127, 225, 1)"
                                ],
                                borderWidth: 1
                            }
                        ]
                    },
                    options: {
                        scales: {
                            yAxes: [
                                {
                                    ticks: {
                                        beginAtZero: false
                                    },
                                    display: true
                                }
                            ],
                            xAxes: [
                                {
                                    ticks: {
                                        display: true
                                    },
                                    display: false
                                }
                            ]
                        },
                        legend: {
                            display: false
                        },
                        responsive: true
                    }
                });

            $("#blockSizeChartLoading").hide("slow", function() {
                $("#blockSizeChart").collapse("show");
            });
        });
}
$(document).ready(function () {
    loadDifficultyData();
    loadTransactionCountData();
    loadBlockSizeData();
});