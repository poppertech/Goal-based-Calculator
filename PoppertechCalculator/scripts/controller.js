﻿var app = angular.module('poppertechCalculatorApp', ['ngResource']);

// TODO: add investment name and region name properties to conditional forecasts and make it an array
// TODO: build the simulation
// TODO: add inputs for portfolio holdings

angular.module('poppertechCalculatorApp').controller('mockupController', ['$scope', '$window', '$filter', 'forecastGraphCalculationsSvc', 'momentCalculationsSvc', 'simulationApiSvc', 'forecastApiSvc', function ($scope, $window, $filter, forecastGraphCalculationsSvc, momentCalculationsSvc, simulationApiSvc, forecastApiSvc) {

    vm = this;

    vm.reset = reset;
    vm.selectVariable = selectVariable;

    simulationApiSvc.postSimulations().then(postSimulationsSuccess, postSimulationsFailure)
    forecastApiSvc.getForecasts().then(getForecastsSuccess, postSimulationsFailure);

    function postSimulationsSuccess(response) {
        vm.investmentStats = response.model;
    }

    function postSimulationsFailure(err) {
        var x = err;
    }

    function getForecastsSuccess(response) {
        vm.editProperties.conditionalForecasts = response.model;
        initSelectedConditionalForecast();
        initConditionalStats(vm.selectedForecast);
        $scope.$watch(function () { return vm.editProperties }, setLocalStorage, true);
        $scope.$watch(function () { return vm.selectedForecast }, calculateForecastGraph, true);
    //    vm.editProperties.conditionalForecasts = [
    //{
    //    name: 'gdp',
    //    regions: [
    //        {
    //            name: 'all',
    //            forecast: [
    //                { title: "Minimum", value: 40 },
    //                { title: "Worst Case", value: 75 },
    //                { title: "Most Likely", value: 100 },
    //                { title: "Best Case", value: 130 },
    //                { title: "Maximum", value: 150 }
    //            ]
    //        }
    //    ]
    //},
    //{
    //    name: 'stocks',
    //    regions: [
    //            {
    //                name: 'leftTail',
    //                forecast: [
    //                    { title: "Minimum", value: 20 },
    //                    { title: "Worst Case", value: 40 },
    //                    { title: "Most Likely", value: 80 },
    //                    { title: "Best Case", value: 100 },
    //                    { title: "Maximum", value: 120 }
    //                ]
    //            },
    //            {
    //                name: 'leftNormal',
    //                forecast: [
    //                    { title: "Minimum", value: 30 },
    //                    { title: "Worst Case", value: 60 },
    //                    { title: "Most Likely", value: 90 },
    //                    { title: "Best Case", value: 110 },
    //                    { title: "Maximum", value: 130 }
    //                ]
    //            },
    //            {
    //                name: 'rightNormal',
    //                forecast: [
    //                    { title: "Minimum", value: 50 },
    //                    { title: "Worst Case", value: 90 },
    //                    { title: "Most Likely", value: 110 },
    //                    { title: "Best Case", value: 140 },
    //                    { title: "Maximum", value: 160 }
    //                ]
    //            },
    //            {
    //                name: 'rightTail',
    //                forecast: [
    //                    { title: "Minimum", value: 60 },
    //                    { title: "Worst Case", value: 100 },
    //                    { title: "Most Likely", value: 120 },
    //                    { title: "Best Case", value: 150 },
    //                    { title: "Maximum", value: 170 }
    //                ]
    //            }
    //    ]
    //},
    //            {
    //                name: 'bonds',
    //                regions: [
    //                        {
    //                            name: 'leftTail',
    //                            forecast: [
    //                                { title: "Minimum", value: 20 },
    //                                { title: "Worst Case", value: 40 },
    //                                { title: "Most Likely", value: 80 },
    //                                { title: "Best Case", value: 100 },
    //                                { title: "Maximum", value: 120 }
    //                            ]
    //                        },
    //                        {
    //                            name: 'leftNormal',
    //                            forecast: [
    //                                { title: "Minimum", value: 30 },
    //                                { title: "Worst Case", value: 60 },
    //                                { title: "Most Likely", value: 90 },
    //                                { title: "Best Case", value: 110 },
    //                                { title: "Maximum", value: 130 }
    //                            ]
    //                        },
    //                        {
    //                            name: 'rightNormal',
    //                            forecast: [
    //                                { title: "Minimum", value: 50 },
    //                                { title: "Worst Case", value: 90 },
    //                                { title: "Most Likely", value: 110 },
    //                                { title: "Best Case", value: 140 },
    //                                { title: "Maximum", value: 160 }
    //                            ]
    //                        },
    //                        {
    //                            name: 'rightTail',
    //                            forecast: [
    //                                { title: "Minimum", value: 60 },
    //                                { title: "Worst Case", value: 100 },
    //                                { title: "Most Likely", value: 120 },
    //                                { title: "Best Case", value: 150 },
    //                                { title: "Maximum", value: 170 }
    //                            ]
    //                        }
    //                ]
    //            }
    //    ];
    }

    activate()

    function activate() {
        var storedEditProperties = getLocalStorage();
        vm.editProperties = storedEditProperties ? storedEditProperties : { cashForecast: [] }
        if (vm.editProperties.cashForecast.length < 1) {
            initCashForecast(vm.editProperties.cashForecast);
        }
        if (!vm.editProperties.conditionalForecasts) {
            //initConditionalForecasts();
        }
        //initSelectedConditionalForecast();
        //initConditionalStats(vm.selectedForecast);

        //$scope.$watch(function () { return vm.editProperties }, setLocalStorage, true);
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
            { text: "Mean", value: momentCalculationsSvc.getMean() },
            { text: "Stdev", value: momentCalculationsSvc.getStdev() },
            { text: "Skew", value: momentCalculationsSvc.getSkew() },
            { text: "Kurt", value: momentCalculationsSvc.getKurt() },
        ];

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

        if (selectedForecast && selectedForecast.length > 0) {
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

            vm.conditionalStats = [
                { text: "Mean", value: momentCalculationsSvc.getMean() },
                { text: "Stdev", value: momentCalculationsSvc.getStdev() },
                { text: "Skew", value: momentCalculationsSvc.getSkew() },
                { text: "Kurt", value: momentCalculationsSvc.getKurt() },
            ];

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
            case 'gdp':
                var selectedRegion = $filter('filter')(vm.selectedVariable.regions, function (region) {
                    return region.name === 'all';
                })[0];
                vm.selectedForecast = selectedRegion.forecast;
                vm.forecastRegionOptions = [{ text: 'All', value: 'all' }];
                vm.forecastRegion = selectedRegion.name;
                vm.gdpClass = 'rect-selected';
                break;
            case 'stocks':
                var selectedRegion = $filter('filter')(vm.selectedVariable.regions, function (region) {
                    return region.name === 'leftTail';
                })[0];
                vm.selectedForecast = selectedRegion.forecast;
                vm.forecastRegionOptions = getConditionalForecastRegionOptions();
                vm.forecastRegion = 'leftTail';
                vm.stocksClass = 'rect-selected';
                break;
            case 'bonds':
                var selectedRegion = $filter('filter')(vm.selectedVariable.regions, function (region) {
                    return region.name === 'leftTail';
                })[0];
                vm.selectedForecast = selectedRegion.forecast;
                vm.forecastRegionOptions = getConditionalForecastRegionOptions();
                vm.forecastRegion = 'leftTail';
                vm.bondsClass = 'rect-selected';
                break;
        }
    }

    function getConditionalForecastRegionOptions() {
        return [
    { text: "Left Tail", value: "leftTail" },
    { text: "Left Normal", value: "leftNormal" },
    { text: "Right Normal", value: "rightNormal" },
    { text: "Right Tail", value: "rightTail" }
        ];
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
