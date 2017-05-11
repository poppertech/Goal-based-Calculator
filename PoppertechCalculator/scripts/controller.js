var app = angular.module('poppertechCalculatorApp', []);

angular.module('poppertechCalculatorApp').controller('mockupController', ['$scope', '$window', function ($scope, $window) {

    vm = this;

    vm.reset = reset;

    activate()

    function activate() {
        var storedEditProperties = getLocalStorage();
        vm.editProperties = storedEditProperties ? storedEditProperties : { cashForecast: []}
        if (vm.editProperties.cashForecast.length < 1) {
            initCashForecast(vm.editProperties.cashForecast);
        }
        if (!vm.editProperties.conditionalForecasts) {
            initConditionalForecasts();
        }
        $scope.$watch(function () { return vm.editProperties }, setLocalStorage, true);
    }

    function initCashForecast(cashForecast) {
        var numYears = 10;
        for (var year = 0; year < numYears + 1; year++) {
            cashForecast.push({ date: 'Year ' + year, value: 0 });
        }
    }

    function initConditionalForecasts() {
        vm.editProperties.conditionalForecasts = {
            gdp: {
                leftTail: [
                { title: "Minimum", value: "-100%" },
                { title: "Worst Case", value: "-50%" },
                { title: "Most Likely", value: "25%" },
                { title: "Best Case", value: "50%" },
                { title: "Maximum", value: "200%" }
                ],
                leftNormal: [
                { title: "Minimum", value: "-50%" },
                { title: "Worst Case", value: "-25%" },
                { title: "Most Likely", value: "15%" },
                { title: "Best Case", value: "60%" },
                { title: "Maximum", value: "200%" }
                ],
                rightNormal: [
                { title: "Minimum", value: "-30%" },
                { title: "Worst Case", value: "-15%" },
                { title: "Most Likely", value: "25%" },
                { title: "Best Case", value: "70%" },
                { title: "Maximum", value: "200%" }
                ],
                rightTail: [
                { title: "Minimum", value: "-10%" },
                { title: "Worst Case", value: "-10%" },
                { title: "Most Likely", value: "30%" },
                { title: "Best Case", value: "80%" },
                { title: "Maximum", value: "200%" }
                ]
            },
            stocks: {
                leftTail: [
                { title: "Minimum", value: "-100%" },
                { title: "Worst Case", value: "-50%" },
                { title: "Most Likely", value: "25%" },
                { title: "Best Case", value: "50%" },
                { title: "Maximum", value: "200%" }
                ],
                leftNormal: [
                { title: "Minimum", value: "-50%" },
                { title: "Worst Case", value: "-25%" },
                { title: "Most Likely", value: "15%" },
                { title: "Best Case", value: "60%" },
                { title: "Maximum", value: "200%" }
                ],
                rightNormal: [
                { title: "Minimum", value: "-30%" },
                { title: "Worst Case", value: "-15%" },
                { title: "Most Likely", value: "25%" },
                { title: "Best Case", value: "70%" },
                { title: "Maximum", value: "200%" }
                ],
                rightTail: [
                { title: "Minimum", value: "-10%" },
                { title: "Worst Case", value: "-10%" },
                { title: "Most Likely", value: "30%" },
                { title: "Best Case", value: "80%" },
                { title: "Maximum", value: "200%" }
                ]
            },
            bonds: {
                leftTail: [
                { title: "Minimum", value: "-100%" },
                { title: "Worst Case", value: "-50%" },
                { title: "Most Likely", value: "25%" },
                { title: "Best Case", value: "50%" },
                { title: "Maximum", value: "200%" }
                ],
                leftNormal: [
                { title: "Minimum", value: "-50%" },
                { title: "Worst Case", value: "-25%" },
                { title: "Most Likely", value: "15%" },
                { title: "Best Case", value: "60%" },
                { title: "Maximum", value: "200%" }
                ],
                rightNormal: [
                { title: "Minimum", value: "-30%" },
                { title: "Worst Case", value: "-15%" },
                { title: "Most Likely", value: "25%" },
                { title: "Best Case", value: "70%" },
                { title: "Maximum", value: "200%" }
                ],
                rightTail: [
                { title: "Minimum", value: "-10%" },
                { title: "Worst Case", value: "-10%" },
                { title: "Most Likely", value: "30%" },
                { title: "Best Case", value: "80%" },
                { title: "Maximum", value: "200%" }
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

    vm.investmentStats = [
        { investment: "Stocks", statistics: [{ title: "Mean", value: "5%" }, { title: "Stdev", value: "15%" }, { title: "Skew", value: "-.03" }, { title: "Kurt", value: "2" }] },
        { investment: "Bonds", statistics: [{ title: "Mean", value: "2%" }, { title: "Stdev", value: "5%" }, { title: "Skew", value: "-.05" }, { title: "Kurt", value: "3" }] },
        { investment: "Portfolio", statistics: [{ title: "Mean", value: "3%" }, { title: "Stdev", value: "10%" }, { title: "Skew", value: "-.04" }, { title: "Kurt", value: "2.5" }] },
    ];

    vm.forecastRegionOptions = [
        { text: "Left Tail", value: "leftTail" },
        { text: "Left Normal", value: "leftNormal" },
        { text: "Right Normal", value: "rightNormal" },
        { text: "Right Tail", value: "rightTail" }
    ];

    vm.onScenarioSelectionChange = function (forecastRegion) {
        vm.selectedForecast = vm.selectedVariable[forecastRegion];
    }

    vm.conditionalForecastChartData = [
        { x: 40, y: 0 },
        { x: 75, y: .57 },
        { x: 100, y: 2.10 },
        { x: 130, y: 1 },
        { x: 150, y: 0 }
    ];

    vm.conditionalStats = [
    { title: "Mean", value: "5%" },
    { title: "Stdev", value: "15%" },
    { title: "Skew", value: "-.05" },
    { title: "Kurt", value: "1" },
    ];

    vm.histogramChartData = [
    { letter: "A", frequency: 0.08167 },
    { letter: "B", frequency: 0.01492 },
    { letter: "C", frequency: 0.02782 },
    { letter: "D", frequency: 0.04253 },
    { letter: "E", frequency: 0.12702 },
    ];

    vm.selectVariable = function (variable) {
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
