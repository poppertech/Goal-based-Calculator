angular.module('poppertechCalculatorApp').directive("multiColoredAreaChart", multiColoredAreaChartDirective)

function multiColoredAreaChartDirective() {

    return {
        scope: {
            chartData: "@chartData",
            chartWidth: "@chartWidth",
            chartHeight: "@chartHeight",
            chartTopMargin: "@chartTopMargin",
            chartRightMargin: "@chartRightMargin",
            chartBottomMargin: "@chartBottomMargin",
            chartLeftMargin: "@chartLeftMargin"
        },
        link: function (scope, element, attrs) {

            var deregister = scope.$watch('chartData', activate);

            function activate(newVal, oldVal) {

                if (!newVal) { return; }

                var svg = d3.select(element[0]).append("svg").attr("width", scope.chartWidth).attr("height", scope.chartHeight);

                var data = JSON.parse(scope.chartData);

                var margin = { top: scope.chartTopMargin, right: scope.chartRightMargin, bottom: scope.chartBottomMargin, left: scope.chartLeftMargin };
                var width = +svg.attr("width") - margin.left - margin.right;
                var height = +svg.attr("height") - margin.top - margin.bottom;
                var g = svg.append("g").attr("transform", "translate(" + margin.left + "," + margin.top + ")");


                var xValues = data.map(function (d) { return d.x; });

                var x = d3.scaleLinear()
                    .domain([d3.min(xValues), d3.max(xValues)])
                    .range([0, width]);

                var y = d3.scaleLinear().rangeRound([height, 0]);

                var area = d3.area()
                    .x(function (d) { return x(d.x); })
                    .y1(function (d) { return y(d.y); });


                y.domain([0, d3.max(data, function (d) { return d.y; })]);
                area.y0(y(0));

                var xAxis = d3.axisBottom(x).ticks(xValues.length);
                var yAxis = d3.axisLeft(y).ticks(5).tickSizeOuter(0);;

                for (var cnt = 0; cnt < data.length - 1; cnt++) {
                    var chartData = [data[cnt], data[cnt + 1]];
                    g.append("path")
                     .datum(chartData)
                     .attr("class", "path" + cnt)
                    .attr("fill", getColor(cnt))
                    .attr("d", area);

                    function getColor(colorCnt) {
                        switch (colorCnt) {
                            case 0:
                                return 'darkred';
                            case 1:
                                return 'salmon';
                            case 2:
                                return 'lightgreen';
                            case 3:
                                return 'darkgreen';
                        }
                    }
                }

                g.append("g")
                    .attr("class", "xaxis")
                    .attr("transform", "translate(0," + height + ")")
                    .call(xAxis)
                    .selectAll("text")
                    .attr("y", 0)
                    .attr("x", 9)
                    .attr("dy", "-.25em")
                    .attr("transform", "rotate(90)")
                    .style("text-anchor", "start");

                g.append("g")
                    .attr("class", "yaxis")
                    .call(yAxis);

                deregister();
                scope.$watch('chartData', redraw);

            }

            function redraw() {

                var svg = d3.select(element[0]).select("svg")
                var g = svg.select("g")

                var margin = { top: scope.chartTopMargin, right: scope.chartRightMargin, bottom: scope.chartBottomMargin, left: scope.chartLeftMargin };
                var width = +svg.attr("width") - margin.left - margin.right;
                var height = +svg.attr("height") - margin.top - margin.bottom;

                var data = JSON.parse(scope.chartData);

                var xValues = data.map(function (d) { return d.x; });

                var x = d3.scaleLinear()
                        .domain([d3.min(xValues), d3.max(xValues)])
                        .range([0, width]);

                var y = d3.scaleLinear().rangeRound([height, 0]);

                var area = d3.area()
                    .x(function (d) { return x(d.x); })
                    .y1(function (d) { return y(d.y); });


                y.domain([0, d3.max(data, function (d) { return d.y; })]);
                area.y0(y(0));

                var xAxis = d3.axisBottom(x).ticks(xValues.length);
                var yAxis = d3.axisLeft(y).ticks(5).tickSizeOuter(0);

                for (var cnt = 0; cnt < data.length - 1; cnt++) {
                    var chartData = [data[cnt], data[cnt + 1]];
                    g.select(".path" + cnt)
                     .datum(chartData)
                    .attr("d", area);
                }

                g.select(".xaxis")
                .call(xAxis);

                g.select(".yaxis")
                    .call(yAxis);

            }

        },
        restrict: "EA"
    }
}





