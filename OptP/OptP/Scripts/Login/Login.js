angular
    .module('app.optp', ['$window'])
    .controller('LoginController', LoginController);

function LoginController($scope, $http, $window) {
    $scope.login = "";
    $scope.senha = "";

    $scope.logar = function () {
        $http({
            method: 'POST',
            url: '/Login',
            data: {
                Usuario: $scope.login,
                Senha: $scope.senha
            }
        }).then(function successCallback(response) {
            if (response.data.sucesso) {
                $window.location.href = '/Home'
            } else {
                alert(response.data.message);
            }
        }, function errorCallback(response) {
            alert('Falha na comunicação com o servidor');
        });
    }

    $scope.submit = function () {
        if (!$scope.login || $scope.login == null ||
            $scope.login == undefined || $scope.login == "") {
            alert('Login inválido');
        } else if (!$scope.senha || $scope.senha == null ||
            $scope.senha == undefined || $scope.senha == "") {
            alert('Senha inválida');
        } else {
            $scope.logar();
    }
}