angular.module('poppertechCalculatorApp').factory('psoApiSvc', psoApiSvc)

psoApiSvc.$inject = ['$resource']

function psoApiSvc($resource) {

    var svc = {};

    svc.postPso = postPso;

    return svc;

    function postPso(input) {
        return $resource('http://localhost:43780/pso', null, { post: { method: 'POST' } }).post({}, input).$promise;
    }

};