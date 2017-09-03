angular.module('poppertechCalculatorApp').controller('ModalDemoCtrl', ModalDemoCtrl)

ModalDemoCtrl.$inject = ['$scope', '$uibModal', '$log'];

function ModalDemoCtrl($scope, 
    $uibModal, 
    $log) {

        var modalInstance = $uibModal.open({
            animation: false,
            templateUrl: 'myModalContent.html',
            controller: 'ModalInstanceCtrl',
            size: 'lg',
        });

};

angular.module('poppertechCalculatorApp').controller('ModalInstanceCtrl', function ($scope, $uibModalInstance) {

    $scope.close = function () {
        $uibModalInstance.close();
    };

});