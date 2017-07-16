angular.module('poppertechCalculatorApp').directive("columnChart", function () {
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

            var deregisterWatch = scope.$watch('chartData', activate);
            return;

            function activate() {

                if (!scope.chartData) {
                    return;
                }

                deregisterWatch();

                var svg = d3.select(element[0]).append("svg").attr("width", scope.chartWidth).attr("height", scope.chartHeight);

                var margin = { top: scope.chartTopMargin, right: scope.chartRightMargin, bottom: scope.chartBottomMargin, left: scope.chartLeftMargin };
                var width = +svg.attr("width") - margin.left - margin.right;
                var height = +svg.attr("height") - margin.top - margin.bottom;

                var x = d3.scaleBand().rangeRound([0, width]).padding(0.1);
                var xAxis = d3.scaleLinear().rangeRound([0, width]);
                var y = d3.scaleLinear().rangeRound([height, 0]);

                var g = svg.append("g").attr("transform", "translate(" + margin.left + "," + margin.top + ")");

                var parsedData = JSON.parse(scope.chartData);

                x.domain(parsedData.map(function (d) { return d.interval; }));
                xAxis.domain([d3.min(parsedData, function (d) { return d.interval; }), d3.max(parsedData, function (d) { return d.interval; })]);
                y.domain([0, d3.max(parsedData, function (d) { return d.frequency; })]);

                g.append("g")
                    .attr("class", "axis axis--x")
                    .attr("transform", "translate(0," + height + ")")
                    .call(d3.axisBottom(xAxis).ticks(10))
                    .selectAll("text")
                    .attr("y", 0)
                    .attr("x", 9)
                    .attr("dy", "-.25em")
                    .attr("transform", "rotate(90)")
                    .style("text-anchor", "start");

                g.append("g")
                    .attr("class", "axis axis--y")
                    .call(d3.axisLeft(y).tickSizeOuter(0).ticks(5, "%"))
                  .append("text")
                    .attr("transform", "rotate(-90)")
                    .attr("y", 6)
                    .attr("dy", "0.71em")
                    .attr("text-anchor", "end")
                    .text("Frequency");

                g.selectAll(".bar")
                  .data(parsedData)
                  .enter().append("rect")
                    .attr("class", "bar")
                    .attr("x", function (d) { return x(d.interval); })
                    .attr("y", function (d) { return y(d.frequency); })
                    .attr("width", x.bandwidth())
                    .attr("height", function (d) { return height - y(d.frequency); });

                scope.$watch('chartData', redraw);
            }

            function redraw() {
                var svg = d3.select(element[0]).select("svg");
                var g = svg.select("g");

                var margin = { top: scope.chartTopMargin, right: scope.chartRightMargin, bottom: scope.chartBottomMargin, left: scope.chartLeftMargin };
                var width = +svg.attr("width") - margin.left - margin.right;
                var height = +svg.attr("height") - margin.top - margin.bottom;

                var parsedData = JSON.parse(scope.chartData);

                var x = d3.scaleBand().rangeRound([0, width]).padding(0.1);
                var y = d3.scaleLinear().rangeRound([height, 0]);
                var xAxis = d3.scaleLinear().rangeRound([0, width]);

                x.domain(parsedData.map(function (d) { return d.interval; }));
                xAxis.domain([d3.min(parsedData, function (d) { return d.interval; }), d3.max(parsedData, function (d) { return d.interval; })]);
                y.domain([0, d3.max(parsedData, function (d) { return d.frequency; })]);

                g.select(".axis.axis--x")
                    .call(d3.axisBottom(xAxis).ticks(10))
                .selectAll("text")
                .attr("y", 0)
                .attr("x", 9)
                .attr("dy", "-.25em")
                .attr("transform", "rotate(90)")
                .style("text-anchor", "start");

                g.select(".axis.axis--y")
                    .call(d3.axisLeft(y).tickSizeOuter(0).ticks(5, "%"))

                g.selectAll(".bar")
                    .data(parsedData)
                    .enter().append("rect")
                    .attr("class", "bar")
                    .attr("x", function (d) { return x(d.interval); })
                    .attr("y", function (d) { return y(d.frequency); })
                    .attr("width", x.bandwidth())
                    .attr("height", function (d) { return height - y(d.frequency); })
                    .exit().remove();

            }

        },
        restrict: "EA"
    }
})

