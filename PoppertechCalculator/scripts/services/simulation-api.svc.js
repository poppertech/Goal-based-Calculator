angular.module('poppertechCalculatorApp').factory('simulationApiSvc', simulationApiSvc)
    
simulationApiSvc.$inject = ['$resource'] 

function simulationApiSvc($resource) {

    var svc = {};

    svc.postSimulations = postSimulations;
    svc.getSimulations = getSimulations;

    return svc;

    function postSimulations(input){
        return $resource('http://localhost:43780/investment', null, { post: { method: 'POST' } }).post({}, input).$promise;
    }

    function getSimulations() {
        return $resource('http://localhost:43780/investment').get({}, { test: "this is a test" }).$promise;
    }

};