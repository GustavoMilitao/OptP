angular
    .module('app.optp', ['ngAnimate','ngAria','ngMaterial', 'ngMessages', 'material.svgAssetsCache'])
    .controller('HomeController', HomeController);

function HomeController($scope, $http, $mdDialog) {
    $scope.modeloMatematico = {
        NomeModeloMatematico: "",
        Variaveis: {},
        Restricoes: [],
        Direcao: 0,
    };
    $scope.variavelAAdicionar = {};
    $scope.restricaoAAdicionar = {
        NomeRestricao: "",
        Variaveis: {},
        Operador: 0,
        Expressao: 0.0
    };
    $scope.operadores = {
        0: "<=",
        1: ">=",
        2: "<",
        3: ">",
        4: "="
    }

    $scope.direcaoOptimizacao = {
        0: "Maximização",
        1: "Minimização",
    }
    $scope.variavelDaRestricaoAAdicionar = [];
    $scope.solucao = null;

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

    $scope.adicionarRestricao = function (ev) {
        if ($scope.temAlgumCoeficiente()) {
            $scope.modeloMatematico.Restricoes.push($scope.restricaoAAdicionar);
            $scope.restricaoAAdicionar = {
                NomeRestricao: "",
                Variaveis: {},
                Operador: 0,
                Expressao: 0.0
            };
        } else {
            $scope.showAlert(ev, 'Atenção', 'Adicione ao menos um coeficiente para alguma das variáveis');
        }
    }

    $scope.removerRestricao = function (index) {
        $scope.modeloMatematico.Restricoes.splice(index, 1);
    }

    $scope.numeroMenorQueLimite = function(numero) {
        return numero < Object.keys($scope.modeloMatematico.Variaveis).length -1 ? '+' : '';
    }

    $scope.temAlgumCoeficiente = function () {
        var tem = false;
        Object.keys($scope.restricaoAAdicionar.Variaveis).forEach(function (elemento) {
            if ($scope.restricaoAAdicionar.Variaveis[elemento]
                && $scope.restricaoAAdicionar.Variaveis[elemento] != "") {
                tem = true;
            }
        });
        return tem;
    }

    $scope.haSolucao = function () {
        return Object.keys($scope.modeloMatematico.Variaveis).every(function (variavelModelo) {
            $scope.modeloMatematico.Restricoes.some(function (restricaoModelo) {
                return Object.keys(restricaoModelo.Variaveis).some(function (variavelRestricao) {
                    return variavelModelo === variavelRestricao;
                });
            });
        });
    }

    $scope.showAlert = function (ev, titulo, descricao) {
        // Appending dialog to document.body to cover sidenav in docs app
        // Modal dialogs should fully cover application
        // to prevent interaction outside of dialog
        $mdDialog.show(
            $mdDialog.alert()
                .parent(angular.element(document.querySelector('#appoptp')))
                .clickOutsideToClose(true)
                .title(titulo)
                .textContent(descricao)
                .ariaLabel('Alert Dialog Demo')
                .ok('OK')
                .targetEvent(ev)
        );
    }


    $scope.temVariaveis = function(){
        return (Object.keys($scope.modeloMatematico.Variaveis).length > 0);
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

    $scope.solucionarProblema = function (ev) {
        if ($scope.haSolucao()) {
            $http({
                method: 'POST',
                url: '/Home/ResolverProblema',
                data: $scope.modeloMatematico
            }).then(function successCallback(response) {
                if (response.data.success) {
                    $scope.solucao = response.data.solucao;
                } else {
                    $scope.showAlert(ev, 'Erro', response.data.message)
                }
            }, function errorCallback(response) {
                $scope.showAlert(ev, 'Erro', 'Falha na comunicação com o servidor.')
            });
        }else{
            $scope.showAlert(ev, 'Erro', 'O modelo matemático não possui solução pois não há restrição para uma ou mais variáveis.')
        }
    }

    $scope.logout = function () {
        setCookie('loggedUser', "", -1);
        window.location.href = "/";
    }

}