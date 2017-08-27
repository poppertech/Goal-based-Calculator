angular.module('poppertechCalculatorApp').factory('simulationApiSvc', simulationApiSvc)
    
simulationApiSvc.$inject = ['$resource'] 

function simulationApiSvc($resource) {

    var svc = {};

    svc.postSimulations = postSimulations;

    return svc;

    function postSimulations(input){
        return $resource('http://localhost:43780/investment', null, { post: { method: 'POST' } }).post({}, input).$promise;
    }


};