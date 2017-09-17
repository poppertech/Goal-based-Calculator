angular.module('poppertechCalculatorApp').factory('portfolioSimulationApiSvc', portfolioSimulationApiSvc)

portfolioSimulationApiSvc.$inject = ['$resource']
 
function portfolioSimulationApiSvc($resource) {

    var svc = {};

    svc.postSimulations = postSimulations;
    svc.getSimulations = getSimulations;

    return svc;

    function postSimulations(input){
        return $resource('http://localhost:43780/portfolio', null, { post: { method: 'POST' } }).post({}, input).$promise;
    }

    function getSimulations() {
        return $resource('http://localhost:43780/portfolio', null, { get: { method: 'GET' } }).get().$promise;
    }


};