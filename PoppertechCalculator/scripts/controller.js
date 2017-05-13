﻿var app = angular.module('poppertechCalculatorApp', []);

angular.module('poppertechCalculatorApp').controller('mockupController', ['$scope', '$window', 'forecastGraphCalculationsSvc', 'momentCalculationsSvc', function ($scope, $window, forecastGraphCalculationsSvc, momentCalculationsSvc) {

    vm = this;

    vm.reset = reset;
    vm.selectVariable = selectVariable;

    activate()

    function activate() {
        var storedEditProperties = getLocalStorage();
        vm.editProperties = storedEditProperties ? storedEditProperties : { cashForecast: [] }
        if (vm.editProperties.cashForecast.length < 1) {
            initCashForecast(vm.editProperties.cashForecast);
        }
        if (!vm.editProperties.conditionalForecasts) {
            initConditionalForecasts();
        }
        initSelectedConditionalForecast();
        initConditionalStats(vm.selectedForecast);

        $scope.$watch(function () { return vm.editProperties }, setLocalStorage, true);
        //$scope.$watch(function () { return vm.selectedForecast }, calculateForecastGraph, true);
    }

    function initCashForecast(cashForecast) {
        var numYears = 10;
        for (var year = 0; year < numYears + 1; year++) {
            cashForecast.push({ date: 'Year ' + year, value: 0 });
        }
    }

    function initSelectedConditionalForecast() {
        vm.selectVariable('gdp');
        vm.selectedForecast = vm.selectedVariable['leftTail'];
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

        vm.conditionalStats = [
            { title: "Mean", value: momentCalculationsSvc.getMean() },
            { title: "Stdev", value: momentCalculationsSvc.getStdev() },
            { title: "Skew", value: momentCalculationsSvc.getSkew() },
            { title: "Kurt", value: momentCalculationsSvc.getKurt() },
        ];

    }


    function initConditionalForecasts() {
        vm.editProperties.conditionalForecasts = {
            gdp: {
                leftTail: [
                { title: "Minimum", value: 40 },
                { title: "Worst Case", value: 75 },
                { title: "Most Likely", value: 100 },
                { title: "Best Case", value: 130 },
                { title: "Maximum", value: 150 }
                ],
                leftNormal: [
                { title: "Minimum", value: 40 },
                { title: "Worst Case", value: 75 },
                { title: "Most Likely", value: 100 },
                { title: "Best Case", value: 130 },
                { title: "Maximum", value: 150 }
                ],
                rightNormal: [
                { title: "Minimum", value: 40 },
                { title: "Worst Case", value: 75 },
                { title: "Most Likely", value: 100 },
                { title: "Best Case", value: 130 },
                { title: "Maximum", value: 150 }
                ],
                rightTail: [
                { title: "Minimum", value: 40 },
                { title: "Worst Case", value: 75 },
                { title: "Most Likely", value: 100 },
                { title: "Best Case", value: 130 },
                { title: "Maximum", value: 150 }
                ]
            },
            stocks: {
                leftTail: [
                { title: "Minimum", value: 40 },
                { title: "Worst Case", value: 75 },
                { title: "Most Likely", value: 100 },
                { title: "Best Case", value: 130 },
                { title: "Maximum", value: 150 }
                ],
                leftNormal: [
                { title: "Minimum", value: 40 },
                { title: "Worst Case", value: 75 },
                { title: "Most Likely", value: 100 },
                { title: "Best Case", value: 130 },
                { title: "Maximum", value: 150 }
                ],
                rightNormal: [
                { title: "Minimum", value: 40 },
                { title: "Worst Case", value: 75 },
                { title: "Most Likely", value: 100 },
                { title: "Best Case", value: 130 },
                { title: "Maximum", value: 150 }
                ],
                rightTail: [
                { title: "Minimum", value: 40 },
                { title: "Worst Case", value: 75 },
                { title: "Most Likely", value: 100 },
                { title: "Best Case", value: 130 },
                { title: "Maximum", value: 150 }
                ]
            },
            bonds: {
                leftTail: [
                { title: "Minimum", value: 40 },
                { title: "Worst Case", value: 75 },
                { title: "Most Likely", value: 100 },
                { title: "Best Case", value: 130 },
                { title: "Maximum", value: 150 }
                ],
                leftNormal: [
                { title: "Minimum", value: 40 },
                { title: "Worst Case", value: 75 },
                { title: "Most Likely", value: 100 },
                { title: "Best Case", value: 130 },
                { title: "Maximum", value: 150 }
                ],
                rightNormal: [
                { title: "Minimum", value: 40 },
                { title: "Worst Case", value: 75 },
                { title: "Most Likely", value: 100 },
                { title: "Best Case", value: 130 },
                { title: "Maximum", value: 150 }
                ],
                rightTail: [
                { title: "Minimum", value: 40 },
                { title: "Worst Case", value: 75 },
                { title: "Most Likely", value: 100 },
                { title: "Best Case", value: 130 },
                { title: "Maximum", value: 150 }
                ]
            }
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

    vm.forecastRegionOptions = [
        { text: "Left Tail", value: "leftTail" },
        { text: "Left Normal", value: "leftNormal" },
        { text: "Right Normal", value: "rightNormal" },
        { text: "Right Tail", value: "rightTail" }
    ];

    vm.onScenarioSelectionChange = function (forecastRegion) {
        vm.selectedForecast = vm.selectedVariable[forecastRegion];
        calculateForecastGraph(vm.selectedForecast);
    }


    function calculateForecastGraph(selectedForecast) {

        if (selectedForecast && selectedForecast.length > 0) {
            forecastGraphCalculationsSvc.calculateHeights(selectedForecast);

            vm.conditionalForecastChartData = [
                { x: forecastGraphCalculationsSvc.getXMin(), y: forecastGraphCalculationsSvc.getMinHeight() },
                { x: forecastGraphCalculationsSvc.getXWorstCase(), y: forecastGraphCalculationsSvc.getWorstCaseHeight() },
                { x: forecastGraphCalculationsSvc.getXMostLikely(), y: forecastGraphCalculationsSvc.getMostLikelyHeight() },
                { x: forecastGraphCalculationsSvc.getXBestCase(), y: forecastGraphCalculationsSvc.getBestCaseHeight() },
                { x: forecastGraphCalculationsSvc.getXMax(), y: forecastGraphCalculationsSvc.getMaxHeight() }
            ];
        }

    }

    vm.investmentStats = [
    { investment: "Stocks", statistics: [{ title: "Mean", value: "5%" }, { title: "Stdev", value: "15%" }, { title: "Skew", value: "-.03" }, { title: "Kurt", value: "2" }] },
    { investment: "Bonds", statistics: [{ title: "Mean", value: "2%" }, { title: "Stdev", value: "5%" }, { title: "Skew", value: "-.05" }, { title: "Kurt", value: "3" }] },
    { investment: "Portfolio", statistics: [{ title: "Mean", value: "3%" }, { title: "Stdev", value: "10%" }, { title: "Skew", value: "-.04" }, { title: "Kurt", value: "2.5" }] }
    ];

    vm.histogramChartData = [
    { letter: "A", frequency: 0.08167 },
    { letter: "B", frequency: 0.01492 },
    { letter: "C", frequency: 0.02782 },
    { letter: "D", frequency: 0.04253 },
    { letter: "E", frequency: 0.12702 },
    ];

    function selectVariable(variable) {
        vm.selectedVariable = vm.editProperties.conditionalForecasts[variable];
        vm.gdpClass = "rect-normal";
        vm.stocksClass = "rect-normal";
        vm.bondsClass = "rect-normal";
        switch (variable) {
            case 'gdp':
                vm.gdpClass = 'rect-selected';
                break;
            case 'stocks':
                vm.stocksClass = 'rect-selected';
                break;
            case 'bonds':
                vm.bondsClass = 'rect-selected';
                break;
        }
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
