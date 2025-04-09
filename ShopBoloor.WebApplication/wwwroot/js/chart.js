$(function () {
    ChangeYearChart('0');
});
function ChangeYearChart(y) {
    
    var chartDiv = $("div#chartIndexDiv");
    var chartLoading = $("div#chartLoadingIndexDiv");
    var parent = $("#ParentChartDiv");
    parent.html("");
    parent.append(`<canvas id="salesChart" style="height: 300px;"></canvas>`);
    chartDiv.addClass('display-chart-none');
    chartLoading.addClass('display-chart-block');
    $.ajax({
        type: "Post",
        url: "/Admin/Home/GetChartData",
        data: {
            year : y
        }
    }).done(function (res) {
        debugger;
        var model = JSON.parse(res);
        $("strong#title-chart-year").text(`فروش سال ${model.Year}`);
        var buttonsParent = $("p#button-years-chart-year");
        buttonsParent.html("");
        model.Years.forEach(x => {
            var yearTag = `
            <a onclick="ChangeYearChart('${x}')" class="btn btn-sm btn-info">${x}</a>
                          `;
            buttonsParent.append(yearTag);
        });
        var salesChartData = {
            labels: model.Mounth,
            datasets: [
                {
                    label: 'Digital Goods',
                    fillColor: 'rgba(60,141,188,0.9)',
                    strokeColor: 'rgba(60,141,188,0.8)',
                    pointColor: '#3b8bba',
                    pointStrokeColor: 'rgba(60,141,188,1)',
                    pointHighlightFill: '#fff',
                    pointHighlightStroke: 'rgba(60,141,188,1)',
                    data: model.Prices
                }
            ]
        };

        var salesChartOptions = {
            // Boolean - If we should show the scale at all
            showScale: true,
            // Boolean - Whether grid lines are shown across the chart
            scaleShowGridLines: false,
            // String - Colour of the grid lines
            scaleGridLineColor: 'rgba(0,0,0,.05)',
            // Number - Width of the grid lines
            scaleGridLineWidth: 1,
            // Boolean - Whether to show horizontal lines (except X axis)
            scaleShowHorizontalLines: true,
            // Boolean - Whether to show vertical lines (except Y axis)
            scaleShowVerticalLines: true,
            // Boolean - Whether the line is curved between points
            bezierCurve: true,
            // Number - Tension of the bezier curve between points
            bezierCurveTension: 0.3,
            // Boolean - Whether to show a dot for each point
            pointDot: false,
            // Number - Radius of each point dot in pixels
            pointDotRadius: 4,
            // Number - Pixel width of point dot stroke
            pointDotStrokeWidth: 1,
            // Number - amount extra to add to the radius to cater for hit detection outside the drawn point
            pointHitDetectionRadius: 20,
            // Boolean - Whether to show a stroke for datasets
            datasetStroke: true,
            // Number - Pixel width of dataset stroke
            datasetStrokeWidth: 2,
            // Boolean - Whether to fill the dataset with a color
            datasetFill: true,
            // String - A legend template
            legendTemplate: '<ul class=\'<%=name.toLowerCase()%>-legend\'><% for (var i=0; i<datasets.length; i++){%><li><span style=\'background-color:<%=datasets[i].lineColor%>\'></span><%=datasets[i].label%></li><%}%></ul>',
            // Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
            maintainAspectRatio: true,
            // Boolean - whether to make the chart responsive to window resizing
            responsive: true
        };
        

        chartDiv.removeClass('display-chart-none');
        chartLoading.removeClass('display-chart-block');
        chartDiv.addClass('display-chart-block');
        chartLoading.addClass('display-chart-none');
        var salesChartCanvas = $('#salesChart').get(0).getContext('2d');
        var salesChart = new Chart(salesChartCanvas);

        salesChart.Line(salesChartData, salesChartOptions);
    });
   
}
//function ChangeYearChart(year) {

    
//    var salesChartCanvas = $('#salesChart').get(0).getContext('2d');
//    var salesChart = new Chart(salesChartCanvas);

//    var salesChartData = {
//        labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'test'],
//        datasets: [
//            {
//                label: 'Electronics',
//                fillColor: 'rgb(210, 214, 222)',
//                strokeColor: 'rgb(210, 214, 222)',
//                pointColor: 'rgb(210, 214, 222)',
//                pointStrokeColor: '#c1c7d1',
//                pointHighlightFill: '#fff',
//                pointHighlightStroke: 'rgb(220,220,220)',
//                data: [65, 59, 80, 81, 56, 55, 40, 12]
//            },
//            {
//                label: 'Digital Goods',
//                fillColor: 'rgba(60,141,188,0.9)',
//                strokeColor: 'rgba(60,141,188,0.8)',
//                pointColor: '#3b8bba',
//                pointStrokeColor: 'rgba(60,141,188,1)',
//                pointHighlightFill: '#fff',
//                pointHighlightStroke: 'rgba(60,141,188,1)',
//                data: [28, 48, 40, 19, 86, 27, 90, 124]
//            },
//            {
//                label: 'Test',
//                fillColor: 'rgba(100,200,10)',
//                strokeColor: 'rgba(160,25,210)',
//                pointColor: '#352bba',
//                pointStrokeColor: 'rgba(160,25,210)',
//                pointHighlightFill: '#fff',
//                pointHighlightStroke: 'rgba(100,200,10)',
//                data: [28, 10, 40, 74, 86, 100, 90, 124]
//            }
//        ]
//    };

//    var salesChartOptions = {
//        // Boolean - If we should show the scale at all
//        showScale: true,
//        // Boolean - Whether grid lines are shown across the chart
//        scaleShowGridLines: false,
//        // String - Colour of the grid lines
//        scaleGridLineColor: 'rgba(0,0,0,.05)',
//        // Number - Width of the grid lines
//        scaleGridLineWidth: 1,
//        // Boolean - Whether to show horizontal lines (except X axis)
//        scaleShowHorizontalLines: true,
//        // Boolean - Whether to show vertical lines (except Y axis)
//        scaleShowVerticalLines: true,
//        // Boolean - Whether the line is curved between points
//        bezierCurve: true,
//        // Number - Tension of the bezier curve between points
//        bezierCurveTension: 0.3,
//        // Boolean - Whether to show a dot for each point
//        pointDot: false,
//        // Number - Radius of each point dot in pixels
//        pointDotRadius: 4,
//        // Number - Pixel width of point dot stroke
//        pointDotStrokeWidth: 1,
//        // Number - amount extra to add to the radius to cater for hit detection outside the drawn point
//        pointHitDetectionRadius: 20,
//        // Boolean - Whether to show a stroke for datasets
//        datasetStroke: true,
//        // Number - Pixel width of dataset stroke
//        datasetStrokeWidth: 2,
//        // Boolean - Whether to fill the dataset with a color
//        datasetFill: true,
//        // String - A legend template
//        legendTemplate: '<ul class=\'<%=name.toLowerCase()%>-legend\'><% for (var i=0; i<datasets.length; i++){%><li><span style=\'background-color:<%=datasets[i].lineColor%>\'></span><%=datasets[i].label%></li><%}%></ul>',
//        // Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
//        maintainAspectRatio: true,
//        // Boolean - whether to make the chart responsive to window resizing
//        responsive: true
//    };

//    salesChart.Line(salesChartData, salesChartOptions);
//    //setTimeout(function () {

//    //}, 3000);
//}