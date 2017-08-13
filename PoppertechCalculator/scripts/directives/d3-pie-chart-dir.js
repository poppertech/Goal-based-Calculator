angular.module('poppertechCalculatorApp').directive("pieChart", pieChartDirective)

function pieChartDirective() {

    return {
        scope: {
            chartData: "@chartData",
            chartWidth: "@chartWidth",
            chartHeight: "@chartHeight",
            chartPathOffset: "@chartPathOffset",
            chartLabelOffset: "@chartLabelOffset",
            chartTopMargin: "@chartTopMargin",
            chartLeftMargin: "@chartLeftMargin"
        },
        link: function (scope, element, attrs) {

            activate();
            scope.$watch('chartData', activate);

            function activate() {

                if (!scope.chartData) {
                    return;
                }

                d3.select(element[0]).select("svg").remove();

                var svg = d3.select(element[0]).append("svg").attr("width", scope.chartWidth).attr("height", scope.chartHeight);

                var margin = { top: +scope.chartTopMargin, left: +scope.chartLeftMargin };
                var width = +svg.attr("width");
                var height = +svg.attr("height");
                var radius = Math.min(width, height) / 2;
                var g = svg.append("g").attr("transform", "translate(" + (margin.left + width / 2) + "," + (margin.top + height / 2) + ")");


                var ordinalScaleRange = ["#98abc5", "#ff8c00"];

                var parsedOrdinalScaleRange = ordinalScaleRange;
                var color = d3.scaleOrdinal(parsedOrdinalScaleRange);

                var pie = d3.pie()
                    .sort(null)
                    .value(function (d) { return d.amount; });

                var path = d3.arc()
                    .outerRadius(radius - scope.chartPathOffset)
                    .innerRadius(0);

                var label = d3.arc()
                    .outerRadius(radius - scope.chartLabelOffset)
                    .innerRadius(radius - scope.chartLabelOffset);

                var parsedData = JSON.parse(scope.chartData);

                var arc = g.selectAll(".arc")
                  .data(pie(parsedData))
                  .enter().append("g")
                    .attr("class", "arc");

                arc.append("path")
                    .attr("d", path)
                    .attr("fill", function (d) {
                        return color(d.data.investment);
                    });

                arc.append("text")
                    .attr("transform", function (d) { return "translate(" + label.centroid(d) + ")"; })
                    .attr("dy", "0.5em")
                    .attr("dx", "-2em")
                    .attr("class", "pie-chart-label")
                    .text(function (d) { return d.data.investment; });

                
            }

        },
        restrict: "EA"
    }
}