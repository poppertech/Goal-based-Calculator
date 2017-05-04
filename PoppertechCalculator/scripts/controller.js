var app = angular.module('poppertechCalculatorApp', []);

angular.module('poppertechCalculatorApp').controller('mockupController',['$scope', '$window', function ($scope, $window) {

    vm = this;

    activate()

    function activate() {
        var storedEditProperties = getLocalStorage();
        vm.editProperties = storedEditProperties ? storedEditProperties : { cashForecast: [] }
        if (vm.editProperties.cashForecast.length < 1) {
            initCashForecast(vm.editProperties.cashForecast);
        }
        $scope.$watch(function () { return vm.editProperties }, setLocalStorage, true);
    }

    function initCashForecast(cashForecast) {
        var numYears = 10;
        for (var year = 0; year < numYears + 1; year++) {
            cashForecast.push({ title: 'Year ' + year, data: 0 });
        }
    }

    vm.portfolioChartData = [
        { investment: "Bonds", amount: 2704659 },
        { investment: "Stocks", amount: 612463 }
    ];

    vm.probabilityChartData = [
        { date: "Year 1", close: 194.93 },
        { date: "Year 2", close: 90.75 },
        { date: "Year 3", close: 214.01 },
        { date: "Year 4", close: 329.57 },
        { date: "Year 5", close: 411.23 }
    ];

    vm.cashChartData = [
        { date: "Year 1", close: 194.93 },
        { date: "Year 2", close: 90.75 },
        { date: "Year 3", close: 214.01 },
        { date: "Year 4", close: 329.57 },
        { date: "Year 5", close: 411.23 }
    ];

    vm.investmentStats = [
        { investment: "Stocks", statistics: [{ title: "Mean", value: "5%" }, { title: "Stdev", value: "15%" }, { title: "Skew", value: "-.03" }, { title: "Kurt", value: "2" }] },
        { investment: "Bonds", statistics: [{ title: "Mean", value: "2%" }, { title: "Stdev", value: "5%" }, { title: "Skew", value: "-.05" }, { title: "Kurt", value: "3" }] },
        { investment: "Portfolio", statistics: [{ title: "Mean", value: "3%" }, { title: "Stdev", value: "10%" }, { title: "Skew", value: "-.04" }, { title: "Kurt", value: "2.5" }] },
    ];

    vm.conditionalForecast = [
        { title: "Minimum", value: "-100%" },
        { title: "Worst Case", value: "-50%" },
        { title: "Most Likely", value: "25%" },
        { title: "Best Case", value: "50%" },
        { title: "Maximum", value: "200%" }
    ];

    vm.conditionalForecastChartData = [
        { date: "Year 1", close: 194.93 },
        { date: "Year 2", close: 90.75 },
        { date: "Year 3", close: 214.01 },
        { date: "Year 4", close: 329.57 },
        { date: "Year 5", close: 411.23 }
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

    vm.showAlert = function (input) { alert(input); }

    function setLocalStorage(editProperties){
        $window.localStorage.setItem('editProperties', angular.toJson(editProperties))
    }

    function getLocalStorage(){
        return angular.fromJson($window.localStorage.getItem('editProperties'));
    }


}]);
