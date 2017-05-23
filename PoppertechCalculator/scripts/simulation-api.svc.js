﻿angular.module('poppertechCalculatorApp').factory('simulationApiSvc', ['$resource', function ($resource) {

    var svc = {};

    svc.postSimulations = postSimulations;
    svc.getSimulations = getSimulations;

    return svc;

    function postSimulations(input){
        return $resource('http://localhost:43780/simulation', null, { post: { method: 'POST' } }).post({}, input).$promise;
    }

    function getSimulations() {
        return $resource('http://localhost:43780/simulation').get({}, { test: "this is a test" }).$promise;
    }

}]);