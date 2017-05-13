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

    function setParameters(selectedForecast) {
        for (var cnt = 0; cnt < selectedForecast.length; cnt++) {
            var forecast = selectedForecast[cnt];
            switch (forecast.title) {
                case "Minimum":
                    xMin = forecast.value;
                    break;
                case "Worst Case":
                    xWorst = forecast.value;
                    break;
                case "Most Likely":
                    xLikely = forecast.value;
                    break;
                case "Best Case":
                    xBest = forecast.value;
                    break;
                case "Maximum":
                    xMax = forecast.value;
                    break;
            }
        }
    }

    function calculateWorstCaseHeight() {
        return 2*LEFT_TAIL/(xWorst - xMin)
    }

    function calculateBestCaseHeight() {
        return 2 * RIGHT_TAIL / (xMax - xBest);
    }

    function calculateMostLikelyHeight() {
        return (2*NORMAL - hWorst * (xLikely - xWorst) - hBest * (xBest - xLikely)) / (xBest - xWorst);
    }
})