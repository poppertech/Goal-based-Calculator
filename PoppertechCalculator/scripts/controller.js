var app = angular.module('poppertechCalculatorApp', ['ngResource']);

// TODO: ensure high test coverage of c# code
// TODO: refactor folder structure
// TODO: fill charts with api data
// TODO: user acceptance test for calculating the probability of portfolio holdings satisfying a goal
// TODO: add classes for and unit test pso
// TODO: refactor arrays, lists, and dictionaries into interfaces


angular.module('poppertechCalculatorApp')
    .controller('CalculatorController', CalculatorController)
    
CalculatorController.$inject = [
    '$scope',
    '$window',
    '$filter',
    'forecastGraphCalculationsSvc',
    'momentCalculationsSvc',
    'simulationApiSvc',
    'portfolioSimulationApiSvc',
    'forecastApiSvc'
];

function CalculatorController(
    $scope,
    $window,
    $filter,
    forecastGraphCalculationsSvc,
    momentCalculationsSvc,
    simulationApiSvc,
    portfolioSimulationApiSvc,
    forecastApiSvc) {

    vm = this;

    vm.reset = reset;
    vm.selectVariable = selectVariable;
    vm.simulateInvestments = simulateInvestments;
    vm.simulatePortfolio = simulatePortfolio;
    vm.editProperties = {};

    activate()

    function activate() {

        setEditProperties();

        $scope.$watch(function () { return vm.editProperties }, setLocalStorage, true);
        $scope.$watch(function () { return vm.selectedForecast }, calculateForecastGraph, true);
    }

    function setEditProperties() {
        var storedEditProperties = getLocalStorage();
        if (storedEditProperties) {
            vm.editProperties = storedEditProperties;
            getForecastsSuccess({ model: vm.editProperties.conditionalForecasts });
        } else {
            vm.editProperties.cashForecast = initCashForecast();
            forecastApiSvc.getForecasts().then(getForecastsSuccess, postSimulationsFailure);
        }
    }

    function initCashForecast() {
        var cashForecast = [];
        var numYears = 10;
        for (var year = 0; year < numYears + 1; year++) {
            cashForecast.push({ date: 'Year ' + year, value: 0 });
        }
        return cashForecast;
    }

    function getForecastsSuccess(response) {
        vm.editProperties.conditionalForecasts = response.model;

        vm.editProperties.investmentContexts = $filter('filter')(vm.editProperties.conditionalForecasts, function (forecast) {
            return forecast.parent;
        });

        angular.forEach(vm.editProperties.investmentContexts, function (context) {
            context.initialPrice = 100;
            context.amount = 1000;
        });

        vm.selectVariable('GDP');
        calculateForecastGraph(vm.selectedForecast);
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


    vm.onScenarioSelectionChange = function (forecastRegion) {
        var selectedRegion = $filter('filter')(vm.selectedVariable.regions, function (region) {
            return region.name === forecastRegion;
        })[0];
        vm.selectedForecast = selectedRegion.forecast;
        calculateForecastGraph(vm.selectedForecast);
    }


    function calculateForecastGraph(selectedForecast) {

        if (selectedForecast) {

            vm.conditionalForecastChartData = forecastGraphCalculationsSvc.getConditionalForecastChartData(selectedForecast);

            var momentContext = forecastGraphCalculationsSvc.getMomentCalculationsContext(selectedForecast);

            vm.conditionalStats = momentCalculationsSvc.calculateStats(momentContext);

        }

    }

    function selectVariable(variableName) {

        vm.selectedVariable = $filter('filter')(vm.editProperties.conditionalForecasts, function (variable) {
            return variable.name === variableName;
        })[0];

        selectVariableRegion(variableName);
        selectVariableClass(variableName);
        setSimulationResults();

    }

    function selectVariableRegion(variableName) {
        switch (variableName) {
            case 'GDP':
                setUnconditionalRegion();
                break;
            case 'Stocks':
                setRegionToLeftTail();
                break;
            case 'Bonds':
                setRegionToLeftTail();
                break;
        }
    }

    function selectVariableClass(variableName) {

        vm.gdpClass = "rect-normal";
        vm.stocksClass = "rect-normal";
        vm.bondsClass = "rect-normal";

        switch (variableName) {
            case 'GDP':
                vm.gdpClass = 'rect-selected';
                break;
            case 'Stocks':
                vm.stocksClass = 'rect-selected';
                break;
            case 'Bonds':
                vm.bondsClass = 'rect-selected';
                break;
        }
    }

    function setSimulationResults() {
        if (vm.simulationResults) {
            var selectedSimulationResult = getSelectedSimulationResult();
            vm.histogramChartData = selectedSimulationResult.histogramsData;
            vm.simulatedStatistics = selectedSimulationResult.statistics;
        }
    }

    function setUnconditionalRegion() {
        var selectedRegion = vm.selectedVariable.regions[0];
        vm.selectedForecast = selectedRegion.forecast;
        vm.forecastRegionOptions = [{ text: 'All', value: null }];
        vm.forecastRegion = selectedRegion.name;
    }

    function setRegionToLeftTail() {
        var selectedRegion = $filter('filter')(vm.selectedVariable.regions, function (region) {
            return region.name === 'LeftTail';
        })[0];
        vm.selectedForecast = selectedRegion.forecast;
        vm.forecastRegionOptions = getConditionalForecastRegionOptions();
        vm.forecastRegion = 'LeftTail';
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

    function simulatePortfolio(cashFlows, conditionalForecasts) {
        var cashFlowValues = cashFlows.map(function (cashFlow) { return cashFlow.value });
        var goalAttainmentContext = { cashFlows: cashFlowValues, investmentContexts: conditionalForecasts };
        portfolioSimulationApiSvc.postSimulations(goalAttainmentContext).then(postPortfolioSimulationsSuccess, postSimulationsFailure);
    }

    function postPortfolioSimulationsSuccess(response) {
        var x = response;
    }

    function postSimulationsSuccess(response) {

        angular.forEach(response.model, function (simulationResult) {
            simulationResult.statistics = changeStatNamesToUpper(simulationResult.statistics);
        });

        vm.simulationResults = response.model;
        var selectedSimulationResult = getSelectedSimulationResult();

        vm.histogramChartData = selectedSimulationResult.histogramsData;
        vm.simulatedStatistics = selectedSimulationResult.statistics;
    }

    function changeStatNamesToUpper(statistics) {
        for (var statName in statistics) {
            var upperStatName = statName.charAt(0).toUpperCase() + statName.substring(1);
            statistics[upperStatName] = statistics[statName];
            delete statistics[statName];
        }
        return statistics;
    }

    function getSelectedSimulationResult() {
        return $filter('filter')(vm.simulationResults, function (simulationResult) {
            return vm.selectedVariable.name === simulationResult.investmentName;
        })[0];
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

}
