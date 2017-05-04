angular.module('poppertechCalculatorApp').directive("areaChart", function () {
    return {
        scope: {
            chartData: "@chartData",
            chartWidth: "@chartWidth",
            chartHeight: "@chartHeight",
            chartTopMargin: "@chartTopMargin",
            chartRightMargin: "@chartRightMargin",
            chartBottomMargin: "@chartBottomMargin",
            chartLeftMargin: "@chartLeftMargin",
            chartFillColor: "@chartFillColor"
        },
        link: function (scope, element, attrs) {

            var svg = d3.select(element[0]).append("svg").attr("width", scope.chartWidth).attr("height", scope.chartHeight);

            var data = JSON.parse(scope.chartData);

            var margin = { top: scope.chartTopMargin, right: scope.chartRightMargin, bottom: scope.chartBottomMargin, left: scope.chartLeftMargin };
            var width = +svg.attr("width") - margin.left - margin.right;
            var height = +svg.attr("height") - margin.top - margin.bottom;
            var g = svg.append("g").attr("transform", "translate(" + margin.left + "," + margin.top + ")");


            var xValues = data.map(function (d) { return d.date; });
            var x = d3.scalePoint().domain(xValues).range([0, width]);

            var y = d3.scaleLinear().rangeRound([height, 0]);

            var area = d3.area()
                .x(function (d) { return x(d.date); })
                .y1(function (d) { return y(d.close); });


            y.domain([0, d3.max(data, function (d) { return d.close; })]);
            area.y0(y(0));

            var xAxis = d3.axisBottom(x).ticks(xValues.length);
            var yAxis = d3.axisLeft(y).tickSizeOuter(0);

            g.append("path")
                .datum(data)
                .attr("fill", scope.chartFillColor)
                .attr("d", area);

            g.append("g")
                .attr("class", "axis")
                .attr("transform", "translate(0," + height + ")")
                .call(xAxis)
                .selectAll("text")
                .attr("y", 0)
                .attr("x", 9)
                .attr("dy", ".35em")
                .attr("transform", "rotate(90)")
                .style("text-anchor", "start");

            g.append("g")
                .attr("class", "axis")
                .call(yAxis);

        },
        restrict: "EA"
    }
})