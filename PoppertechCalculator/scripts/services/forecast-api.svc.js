angular.module('poppertechCalculatorApp').factory('forecastApiSvc', ['$resource', function ($resource) {

    var svc = {};

    svc.getForecasts = getForecasts;

    return svc;

    function getForecasts() {
        return $resource('http://localhost:43780/forecast').get({}, {}).$promise;
    }

}]);