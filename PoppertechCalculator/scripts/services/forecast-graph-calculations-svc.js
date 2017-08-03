angular.module('poppertechCalculatorApp').factory('forecastGraphCalculationsSvc',forecastGraphCalculationsSvc)

function forecastGraphCalculationsSvc() {

    var svc = {};

    var LEFT_TAIL = 10;
    var RIGHT_TAIL = 10;
    var NORMAL = 100 - LEFT_TAIL - RIGHT_TAIL;

    var xMin, xWorst, xLikely, xBest, xMax;
    var hWorst, hLikely, hBest;
    var hMin = 0; var hMax = 0;

    svc.calculateHeights = calculateHeights;

    svc.getConditionalForecastChartData = getConditionalForecastChartData;
    svc.getMomentCalculationsContext = getMomentCalculationsContext;

    return svc;

    function calculateHeights(selectedForecast) {
        setParameters(selectedForecast);
        hWorst = calculateWorstCaseHeight();
        hBest = calculateBestCaseHeight();
        hLikely = calculateMostLikelyHeight();
    }

    function getConditionalForecastChartData(selectedForecast) {
        calculateHeights(selectedForecast);
        return [
                { x: xMin, y: hMin },
                { x: xWorst, y: hWorst },
                { x: xLikely, y: hLikely },
                { x: xBest, y: hBest },
                { x: xMax, y: hMax }
        ];
    }

    function getMomentCalculationsContext(selectedForecast) {
        calculateHeights(selectedForecast);

        return {
            xMin: xMin,
            xWorst: xWorst,
            xLikely: xLikely,
            xBest: xBest,
            xMax: xMax,
            hMin: hMin,
            hWorst: hWorst,
            hLikely: hLikely,
            hBest: hBest,
            hMax: hMax
        };

    }

    function setParameters(forecast) {

        xMin = forecast.minimum;
        xWorst = forecast.worst;
        xLikely = forecast.likely;
        xBest = forecast.best;
        xMax = forecast.maximum;

    }

    function calculateWorstCaseHeight() {
        return 2 * LEFT_TAIL / (xWorst - xMin)
    }

    function calculateBestCaseHeight() {
        return 2 * RIGHT_TAIL / (xMax - xBest);
    }

    function calculateMostLikelyHeight() {
        return (2 * NORMAL - hWorst * (xLikely - xWorst) - hBest * (xBest - xLikely)) / (xBest - xWorst);
    }
}