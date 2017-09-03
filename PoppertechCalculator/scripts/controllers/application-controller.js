angular.module('poppertechCalculatorApp', ['ngResource', 'ui.bootstrap']);

// TODO: user acceptance test for pso
// TODO: add form validation on ui
// TODO: add on-blur ng-model-options to inputs
// TODO: add web api validation
// TODO: loading screen
// TODO: return the probability results graph on load
// TODO: all initial data should come from the database
// TODO: deploy
// TODO: add angular animations for modal

// TODO: write narrative to correspond with calculator
// TODO: check in excel template files into source control
// TODO: write linkedin post to direct traffic to calculator

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
    'forecastApiSvc',
    'psoApiSvc',
    '$uibModal'
];

function CalculatorController(
    $scope,
    $window,
    $filter,
    forecastGraphCalculationsSvc,
    momentCalculationsSvc,
    simulationApiSvc,
    portfolioSimulationApiSvc,
    forecastApiSvc,
    psoApiSvc,
    $uibModal) {

    vm = this;

    vm.loading = true;

    vm.reset = reset;
    vm.selectVariable = selectVariable;
    vm.simulateInvestments = simulateInvestments;
    vm.simulatePortfolio = simulatePortfolio;
    vm.optimizePortfolio = optimizePortfolio;
    vm.editProperties = { optimizationParams: { lowerBound: 1000, upperBound: 3000, interval: 1000 } };

    activate()

    function activate() {

        setEditProperties();

        $scope.$watch(function () { return vm.editProperties }, setLocalStorage, true);

    }

    function setEditProperties() {
        var storedEditProperties = getLocalStorage();
        if (storedEditProperties) {
            vm.editProperties = storedEditProperties;
            getForecastsSuccess({ model: vm.editProperties.conditionalForecasts });
            vm.loading = false;
        } else {
            vm.editProperties.cashForecast = initCashForecast();
            forecastApiSvc.getForecasts().then(getForecastsSuccess, postSimulationsFailure).finally(function () { vm.loading = false });
        }
    }

    function initCashForecast() {
        var cashForecast = [{ date: 'Year ' + 0, value: 0 }];
        var numYears = 10;
        for (var year = 1; year < numYears + 1; year++) {
            cashForecast.push({ date: 'Year ' + year, value: 400 });
        }
        return cashForecast;
    }

    function getForecastsSuccess(response) {
        vm.editProperties.conditionalForecasts = response.model;

        vm.editProperties.investmentContexts = getInitialInvestmentContext(response.model);

        vm.portfolioChartData = getPortfolioChartData(vm.editProperties.investmentContexts);

        vm.selectVariable('GDP');
        calculateForecastGraph(vm.selectedForecast);
    }

    function getInitialInvestmentContext(conditionalForecasts) {
        var investmentContexts = $filter('filter')(conditionalForecasts, function (forecast) {
            return forecast.parent;
        });

        angular.forEach(investmentContexts, function (context) {
            context.initialPrice = 100;
            context.amount = 2000;

        });

        calculateInvestmentContextWeights(investmentContexts);

        return investmentContexts;
    }

    function calculateInvestmentContextWeights(investmentContexts) {
        var portfolioAmount = 0;

        angular.forEach(investmentContexts, function (context) {
            portfolioAmount += context.amount;
        });

        angular.forEach(investmentContexts, function (context) {
            context.weight = ((context.amount / portfolioAmount).toFixed(2)) * 100 + '%';
        });

    }

    function getPortfolioChartData(investmentContexts) {

        var portfolioChartData = [];

        angular.forEach(investmentContexts, function (context) {
            portfolioChartData.push({ investment: context.name, amount: context.amount });
        });

        return portfolioChartData;
    }

    vm.onScenarioSelectionChange = function (forecastRegion) {
        var selectedRegion = $filter('filter')(vm.selectedVariable.regions, function (region) {
            return region.name === forecastRegion;
        })[0];
        vm.selectedForecast = selectedRegion.forecast;
        calculateForecastGraph(vm.selectedForecast);
    }

    vm.onInvestmentAmountChange = function (investmentContexts) {
        calculateInvestmentContextWeights(investmentContexts);
        vm.portfolioChartData = getPortfolioChartData(investmentContexts);
    };

    vm.onForecastValueChange = function (selectedForecast) {
        calculateForecastGraph(selectedForecast);
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

    function optimizePortfolio(optimizationParams, cashFlows, conditionalForecasts) {
        var cashFlowValues = cashFlows.map(function (cashFlow) { return cashFlow.value });
        var psoContext = { cashFlows: cashFlowValues, investmentContexts: conditionalForecasts, OptimizationParams: optimizationParams };
        psoApiSvc.postPso(psoContext).then(postPsoSuccess, postSimulationsFailure);
    }

    function postPsoSuccess(response) {
        vm.probabilityChartData = getProbabilityChartData(response.model.chartData);
        vm.portfolioChartData = getPortfolioChartData(response.model.optimalInvestments);
        vm.editProperties.investmentContexts = getInvestmentContext(vm.editProperties.investmentContexts, response.model.optimalInvestments);
    }

    function getInvestmentContext(investmentContexts, optimalInvestments) {
        angular.forEach(investmentContexts, function (investmentContext) {
            var optimalInvestment = $filter('filter')(optimalInvestments, function (optimalInvestment) {
                return optimalInvestment.name === investmentContext.name;
            })[0];
            investmentContext.amount = optimalInvestment.amount;
            investmentContext.weight = optimalInvestment.weight;
        });
        return investmentContexts;
    }

    function postPortfolioSimulationsSuccess(response) {
        vm.probabilityChartData = getProbabilityChartData(response.model);
    }

    function getProbabilityChartData(chartData) {
        var probabilityChartData = [];
        var probabilityChartDictionary = changeKeysToUpper(chartData);
        for (var key in probabilityChartDictionary) {
            var value = probabilityChartDictionary[key];
            probabilityChartData.push({ date: key, value: value });
        }
        return probabilityChartData;
    }

    function postSimulationsSuccess(response) {

        angular.forEach(response.model, function (simulationResult) {
            simulationResult.statistics = changeKeysToUpper(simulationResult.statistics);
        });

        vm.simulationResults = response.model;
        var selectedSimulationResult = getSelectedSimulationResult();

        vm.histogramChartData = selectedSimulationResult.histogramsData;
        vm.simulatedStatistics = selectedSimulationResult.statistics;
    }

    function changeKeysToUpper(keyValue) {
        for (var key in keyValue) {
            var upperKey = key.charAt(0).toUpperCase() + key.substring(1);
            keyValue[upperKey] = keyValue[key];
            delete keyValue[key];
        }
        return keyValue;
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
