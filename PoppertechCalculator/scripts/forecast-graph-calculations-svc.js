angular.module('poppertechCalculatorApp').factory('forecastGraphCalculationsSvc', function () {

    var svc = {};

    var LEFT_TAIL = 10;
    var RIGHT_TAIL = 10;
    var NORMAL = 100 - LEFT_TAIL - RIGHT_TAIL;

    var xMin, xWorst, xLikely, xBest, xMax;
    var hWorst, hLikely, hBest;
    var hMin = 0; var hMax = 0;

    svc.calculateHeights = calculateHeights;

    svc.getXMin = getXMin;
    svc.getXWorstCase = getXWorstCase;
    svc.getXBestCase = getXBestCase;
    svc.getXMostLikely = getXMostLikely;
    svc.getXMax = getXMax;

    svc.getMinHeight = getMinHeight;
    svc.getWorstCaseHeight = getWorstCaseHeight;
    svc.getBestCaseHeight = getBestCaseHeight;
    svc.getMostLikelyHeight = getMostLikelyHeight;
    svc.getMaxHeight = getMaxHeight;

    return svc;

    function calculateHeights(selectedForecast) {
        setParameters(selectedForecast);
        hWorst = calculateWorstCaseHeight();
        hBest = calculateBestCaseHeight();
        hLikely = calculateMostLikelyHeight();
    }


    function getXMin() {
        return xMin;
    }

    function getXWorstCase() {
        return xWorst;
    }

    function getXMostLikely() {
        return xLikely;
    }

    function getXBestCase() {
        return xBest;
    }

    function getXMax() {
        return xMax;
    }

    function getMinHeight() {
        return hMin;
    }

    function getWorstCaseHeight() {
        return hWorst;
    }

    function getMostLikelyHeight() {
        return hLikely;
    }

    function getBestCaseHeight() {
        return hBest;
    }

    function getMaxHeight() {
        return hMax;
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
})