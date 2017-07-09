var app = angular.module('poppertechCalculatorApp', ['ngResource']);

// TODO: build the simulation
// TODO: add inputs for portfolio holdings

angular.module('poppertechCalculatorApp').controller('mockupController', ['$scope', '$window', '$filter', 'forecastGraphCalculationsSvc', 'momentCalculationsSvc', 'simulationApiSvc', 'forecastApiSvc', function ($scope, $window, $filter, forecastGraphCalculationsSvc, momentCalculationsSvc, simulationApiSvc, forecastApiSvc) {

    vm = this;

    vm.reset = reset;
    vm.selectVariable = selectVariable;
    vm.simulateInvestments = simulateInvestments;


    activate()

    function activate() {
        var storedEditProperties = getLocalStorage();
        vm.editProperties = storedEditProperties ? storedEditProperties : { cashForecast: [] }
        if (vm.editProperties.cashForecast.length < 1) {
            initCashForecast(vm.editProperties.cashForecast);
        }
        if (!vm.editProperties.conditionalForecasts) {
            forecastApiSvc.getForecasts().then(getForecastsSuccess, postSimulationsFailure);
        } else {
            getForecastsSuccess({ model: vm.editProperties.conditionalForecasts });
        }

        $scope.$watch(function () { return vm.editProperties }, setLocalStorage, true);
        $scope.$watch(function () { return vm.selectedForecast }, calculateForecastGraph, true);
    }

    function initCashForecast(cashForecast) {
        var numYears = 10;
        for (var year = 0; year < numYears + 1; year++) {
            cashForecast.push({ date: 'Year ' + year, value: 0 });
        }
    }

    function getForecastsSuccess(response) {
        vm.editProperties.conditionalForecasts = response.model;
        initSelectedConditionalForecast();
        initConditionalStats(vm.selectedForecast);
    }

    function initSelectedConditionalForecast() {
        vm.selectVariable('GDP');
        calculateForecastGraph(vm.selectedForecast);
    }

    function initConditionalStats() {

        var context = {
            xMin: forecastGraphCalculationsSvc.getXMin(),
            xWorst: forecastGraphCalculationsSvc.getXWorstCase(),
            xLikely: forecastGraphCalculationsSvc.getXMostLikely(),
            xBest: forecastGraphCalculationsSvc.getXBestCase(),
            xMax: forecastGraphCalculationsSvc.getXMax(),
            hMin: forecastGraphCalculationsSvc.getMinHeight(),
            hWorst: forecastGraphCalculationsSvc.getWorstCaseHeight(),
            hLikely: forecastGraphCalculationsSvc.getMostLikelyHeight(),
            hBest: forecastGraphCalculationsSvc.getBestCaseHeight(),
            hMax: forecastGraphCalculationsSvc.getMaxHeight()
        };

        momentCalculationsSvc.calculateStats(context);

        vm.conditionalStats = {
            Mean: momentCalculationsSvc.getMean(),
            Stdev: momentCalculationsSvc.getStdev(),
            Skew: momentCalculationsSvc.getSkew(),
            Kurt: momentCalculationsSvc.getKurt(),
        };

    }



    vm.portfolioChartData = [
        { investment: "Bonds", amount: 2704659 },
        { investment: "Stocks", amount: 612463 }
    ];

    vm.probabilityChartData = [
        { date: "Year 1", value: 194.93 },
        { date: "Year 2", value: 90.75 },
        { date: "Year 3", value: 214.01 },
        { date: "Year 4", value: 329.57 },
        { date: "Year 5", value: 411.23 }
    ];

    vm.cashChartData = [
        { date: "Year 1", value: 194.93 },
        { date: "Year 2", value: 90.75 },
        { date: "Year 3", value: 214.01 },
        { date: "Year 4", value: 329.57 },
        { date: "Year 5", value: 411.23 }
    ];

    vm.onScenarioSelectionChange = function (forecastRegion) {
        var selectedRegion = $filter('filter')(vm.selectedVariable.regions, function (region) {
            return region.name === forecastRegion;
        })[0];
        vm.selectedForecast = selectedRegion.forecast;
        calculateForecastGraph(vm.selectedForecast);
    }


    function calculateForecastGraph(selectedForecast) {

        if (selectedForecast) {
            forecastGraphCalculationsSvc.calculateHeights(selectedForecast);

            vm.conditionalForecastChartData = [
                { x: forecastGraphCalculationsSvc.getXMin(), y: forecastGraphCalculationsSvc.getMinHeight() },
                { x: forecastGraphCalculationsSvc.getXWorstCase(), y: forecastGraphCalculationsSvc.getWorstCaseHeight() },
                { x: forecastGraphCalculationsSvc.getXMostLikely(), y: forecastGraphCalculationsSvc.getMostLikelyHeight() },
                { x: forecastGraphCalculationsSvc.getXBestCase(), y: forecastGraphCalculationsSvc.getBestCaseHeight() },
                { x: forecastGraphCalculationsSvc.getXMax(), y: forecastGraphCalculationsSvc.getMaxHeight() }
            ];

            var context = {
                xMin: forecastGraphCalculationsSvc.getXMin(),
                xWorst: forecastGraphCalculationsSvc.getXWorstCase(),
                xLikely: forecastGraphCalculationsSvc.getXMostLikely(),
                xBest: forecastGraphCalculationsSvc.getXBestCase(),
                xMax: forecastGraphCalculationsSvc.getXMax(),
                hMin: forecastGraphCalculationsSvc.getMinHeight(),
                hWorst: forecastGraphCalculationsSvc.getWorstCaseHeight(),
                hLikely: forecastGraphCalculationsSvc.getMostLikelyHeight(),
                hBest: forecastGraphCalculationsSvc.getBestCaseHeight(),
                hMax: forecastGraphCalculationsSvc.getMaxHeight()
            };

            momentCalculationsSvc.calculateStats(context);

            vm.conditionalStats =
                {
                    Mean: momentCalculationsSvc.getMean(),
                    Stdev: momentCalculationsSvc.getStdev(),
                    Skew: momentCalculationsSvc.getSkew(),
                    Kurt: momentCalculationsSvc.getKurt(),
                };

        }

    }

    vm.histogramChartData = [
    { letter: "A", frequency: 0.08167 },
    { letter: "B", frequency: 0.01492 },
    { letter: "C", frequency: 0.02782 },
    { letter: "D", frequency: 0.04253 },
    { letter: "E", frequency: 0.12702 },
    ];

    function selectVariable(variableName) {
        vm.selectedVariable = $filter('filter')(vm.editProperties.conditionalForecasts, function (variable) {
            return variable.name === variableName;
        })[0];

        vm.gdpClass = "rect-normal";
        vm.stocksClass = "rect-normal";
        vm.bondsClass = "rect-normal";

        switch (variableName) {
            case 'GDP':
                var selectedRegion = $filter('filter')(vm.selectedVariable.regions, function (region) {
                    return !region.name;
                })[0];
                vm.selectedForecast = selectedRegion.forecast;
                vm.forecastRegionOptions = [{ text: 'All', value: null }];
                vm.forecastRegion = selectedRegion.name;
                vm.gdpClass = 'rect-selected';
                break;
            case 'Stocks':
                var selectedRegion = $filter('filter')(vm.selectedVariable.regions, function (region) {
                    return region.name === 'LeftTail';
                })[0];
                vm.selectedForecast = selectedRegion.forecast;
                vm.forecastRegionOptions = getConditionalForecastRegionOptions();
                vm.forecastRegion = 'LeftTail';
                vm.stocksClass = 'rect-selected';
                break;
            case 'Bonds':
                var selectedRegion = $filter('filter')(vm.selectedVariable.regions, function (region) {
                    return region.name === 'LeftTail';
                })[0];
                vm.selectedForecast = selectedRegion.forecast;
                vm.forecastRegionOptions = getConditionalForecastRegionOptions();
                vm.forecastRegion = 'LeftTail';
                vm.bondsClass = 'rect-selected';
                break;
        }
    }

    function getConditionalForecastRegionOptions() {
        return [
    { text: "Left Tail", value: "LeftTail" },
    { text: "Left Normal", value: "LeftNormal" },
    { text: "Right Normal", value: "RightNormal" },
    { text: "Right Tail", value: "RightTail" }
        ];
    }

    function simulateInvestments(conditionalForecasts) {
        simulationApiSvc.postSimulations(conditionalForecasts).then(postSimulationsSuccess, postSimulationsFailure)
    }

    function postSimulationsSuccess(response) {
        vm.simulationResults = response.model;

    }

    function postSimulationsFailure(err) {
        var x = err;
    }

    function setLocalStorage(editProperties) {
        $window.localStorage.setItem('editProperties', angular.toJson(editProperties))
    }

    function getLocalStorage() {
        return angular.fromJson($window.localStorage.getItem('editProperties'));
    }

    function reset() {
        $window.localStorage.removeItem('editProperties');
        activate();
    }

}]);
