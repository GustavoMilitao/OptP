angular
    .module('app.optp', [])
    .controller('HomeController', HomeController);

function HomeController($scope, $http) {
    $scope.modeloMatematico = {
        NomeModeloMatematico: "",
        Variaveis: {},
        Restricoes: [],
        Direcao: "",
    };
    $scope.variavelAAdicionar = {};
    $scope.restricaoAAdicionar = {
        NomeRestricao: "",
        Variaveis: {},
        Operador: "",
        Expressao: 0.0
    };
    $scope.operadores = {
        0: "<=",
        1: ">=",
        2: "<",
        3: ">"
    }

    $scope.direcaoOptimizacao = {
        0: "Maximização",
        1: "Minimização",
    }
    $scope.variavelDaRestricaoAAdicionar = {};
    $scope.solucao = {};

    $scope.adicionarVariavelRestricao = function () {
        if ($scope.variavelDaRestricaoAAdicionar.nomeVariavel
            && $scope.variavelDaRestricaoAAdicionar.nomeVariavel != ""
            && $scope.variavelDaRestricaoAAdicionar.coeficiente
            && $scope.variavelDaRestricaoAAdicionar.coeficiente != "") {
            $scope.restricaoAAdicionar
                .Variaveis[$scope.capitalize($scope.variavelDaRestricaoAAdicionar.nomeVariavel)] = parseFloat($scope.variavelDaRestricaoAAdicionar.coeficiente);
            $scope.variavelDaRestricaoAAdicionar = {};
        }
    }

    $scope.adicionarRestricao = function () {
        if (Object.keys($scope.restricaoAAdicionar.Variaveis).length > 0) {
            $scope.modeloMatematico.Restricoes.push($scope.restricaoAAdicionar);
            $scope.restricaoAAdicionar = {
                NomeRestricao: "",
                Variaveis: {},
                Operador: "",
                Expressao: 0.0
            };
        }
    }

    $scope.submitVariavel = function () {
        $scope.modeloMatematico.Variaveis[$scope.capitalize($scope.variavelAAdicionar.nomeVariavel)] =
            $scope.variavelAAdicionar.coeficiente;
        $scope.variavelAAdicionar = {};
    }

    $scope.capitalize = function (str) {
        var strSplit = str.split(' ');
        for (var i = 0; i < strSplit.length; i++) {
            strSplit[i] = strSplit[i][0].toUpperCase() + strSplit[i].slice(1);
        }
        return strSplit.join(' ');
    }

    $scope.solucionarProblema = function () {
        if (Object.keys($scope.modeloMatematico.Variaveis).length > 0
            && $scope.modeloMatematico.Restricoes.length > 0
            && $scope.modeloMatematico.Direcao != "") {
            $http({
                method: 'POST',
                url: '/Home/ResolverProblema',
                data: $scope.modeloMatematico
            }).then(function successCallback(response) {
                if (response.data.success) {
                    alert('Sucesso!');
                } else {
                    alert(response.data.message);
                }
            }, function errorCallback(response) {
                alert('Falha na comunicação com o servidor');
            });
        }
    }

    $scope.logout = function () {
        setCookie('loggedUser', "", -1);
        window.location.href = "/";
    }

}