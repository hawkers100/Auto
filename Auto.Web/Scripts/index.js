var app = angular.module('autoApp', []);
app.controller('indexCtrl', function ($scope, $http) {
    $scope.cur = {};
    $scope.loadingData = false;
    $scope.loadData = function () {
        $scope.loadingData = true;
        $http.get("/api/CarRents")
            .then(function (response) {
                $scope.rents = response.data;
            }, common.showGeneralError)
            .finally(function() {
                $scope.loadingData = false;
            });
    };
    $scope.loadData();
    $scope.formatStatus = function (status) {
        return taskStatuses[status - 1];
    };
    $scope.addNew = function () {
        $scope.editing = false;
        $scope.resetForm();
        $scope.cur = { };
    };
    $scope.edit = function (rent) {
        $scope.editing = true;
        $scope.resetForm();
        $scope.cur = {
            Id: rent.Id,
            CarNumber: rent.CarNumber,
            RentLocation: rent.RentLocation,
            RentTime: new Date(rent.RentTime),
            ReturnLocation: rent.ReturnLocation
        };
        $scope.cur.RentTime = new Date(rent.RentTime);
        $scope.cur.RentTime.setSeconds(0);
        $scope.cur.RentTime.setMilliseconds(0);
        if (rent.ReturnTime) {
            $scope.cur.ReturnTime = new Date(rent.ReturnTime);
            $scope.cur.ReturnTime.setSeconds(0);
            $scope.cur.ReturnTime.setMilliseconds(0);
        }
        $("#popup-editor").modal("show");
    };
    $scope.save = function () {
        if ($scope.saving) {
            return;
        }
        if ($scope.editForm.$invalid) {
            return;
        }
        if ($scope.cur.ReturnTime && $scope.cur.ReturnTime <= $scope.cur.RentTime) {
            $scope.formError = "Время возврата автомобиля должно быть больше времени взятия";
            return;
        }
        $scope.saving = true;
        var data = angular.copy($scope.cur);
        data.RentTime = common.fixDate(data.RentTime);
        data.ReturnTime = common.fixDate(data.ReturnTime);
        if ($scope.editing) {
            $http.put("/api/CarRents/" + data.Id, data)
                .then(function (response) {
                    var index = _.findIndex($scope.rents, function (rent) { return rent.Id == response.data.Id; });
                    $scope.rents[index] = response.data;
                    $("#popup-editor").modal("hide");
                }, common.showGeneralError)
                .finally(function () {
                    $scope.saving = false;
                });
        } else {
            $http.post("/api/CarRents", data)
                .then(function (response) {
                    $scope.rents.push(response.data);
                    $("#popup-editor").modal("hide");
                }, common.showGeneralError)
                .finally(function () {
                    $scope.saving = false;
                });
        }
    };
    $scope.resetForm = function () {
        $scope.formError = null;
        $scope.editForm.$setPristine();
        $scope.editForm.$setUntouched();
    };
});