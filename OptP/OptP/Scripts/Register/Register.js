angular
    .module('app.optp', ['$window'])
    .controller('RegisterController', RegisterController);

function RegisterController($scope, $http, $window) {

    angular.element(document).ready(function () {
        var cookie = getCookie('loggedUser');
        if (cookie) {
            window.location.href = "/Home";
        }
    });


    $scope.login = "";
    $scope.senha = "";
    $scope.senha1 = "";

    $scope.registrar = function () {
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
        } else if (!$scope.senha1 || $scope.senha1 == null ||
            $scope.senha1 == undefined || $scope.senha1 == "") {
            alert('Senha inválida');
        } else if (senha1 != senha) {
            alert('As senhas diferem');
        } else {
            $scope.registrar();
        }
    }
}

