angular.module('poppertechCalculatorApp').factory('portfolioSimulationApiSvc', ['$resource', function ($resource) {

    var svc = {};

    svc.postSimulations = postSimulations;

    return svc;

    function postSimulations(input){
        return $resource('http://localhost:43780/portfolio', null, { post: { method: 'POST' } }).post({}, input).$promise;
    }


}]);