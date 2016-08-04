namespace ReadingApp {

    angular.module('ReadingApp', ['ui.router', 'ngResource', 'ui.bootstrap']).config((
        $stateProvider: ng.ui.IStateProvider,
        $urlRouterProvider: ng.ui.IUrlRouterProvider,
        $locationProvider: ng.ILocationProvider
    ) => {
        // Define routes
        $stateProvider
            .state('home', {
                url: '/',
                templateUrl: '/ngApp/views/home.html',
                controller: ReadingApp.Controllers.HomeController,
                controllerAs: 'controller'
            })
            .state('addbook', {
                url: '/addbook',
                templateUrl: '/ngApp/views/addbook.html',
                controller: ReadingApp.Controllers.SearchController,
                controllerAs: 'controller'
            })
            .state('secret', {
                url: '/secret',
                templateUrl: '/ngApp/views/secret.html',
                controller: ReadingApp.Controllers.SecretController,
                controllerAs: 'controller'
            })
            .state('login', {
                url: '/login',
                templateUrl: '/ngApp/views/login.html',
                controller: ReadingApp.Controllers.LoginController,
                controllerAs: 'controller'
            })
            .state('register', {
                url: '/register',
                templateUrl: '/ngApp/views/register.html',
                controller: ReadingApp.Controllers.RegisterController,
                controllerAs: 'controller'
            })
            .state('externalRegister', {
                url: '/externalRegister',
                templateUrl: '/ngApp/views/externalRegister.html',
                controller: ReadingApp.Controllers.ExternalRegisterController,
                controllerAs: 'controller'
            })
            .state('booksview', {
                url: '/booksview',
                templateUrl: '/ngApp/views/booksview.html',
                controller: ReadingApp.Controllers.ResourceController,
                controllerAs: 'controller'
            })
            .state('search', {
                url: '/search',
                templateUrl: '/ngApp/views/search.html',
                controller: ReadingApp.Controllers.CommentController,
                controllerAs: 'controller'
            })
            .state('bookdetail', {
                url: '/bookdetail/:id',
                templateUrl: '/ngApp/views/bookdetail.html',
                controller: ReadingApp.Controllers.ResourceDetailsController,
                controllerAs: 'controller'
            })
            .state('searchdetail', {
                url: '/searchdetail/:id',
                templateUrl: '/ngApp/views/searchdetail.html',
                controller: ReadingApp.Controllers.CommentController,
                controllerAs: 'controller'
            })
            .state('notFound', {
                url: '/notFound',
                templateUrl: '/ngApp/views/notFound.html'
            });

        // Handle request for non-existent route
        $urlRouterProvider.otherwise('/notFound');

        // Enable HTML5 navigation
        $locationProvider.html5Mode(true);
    });

    
    angular.module('ReadingApp').factory('authInterceptor', (
        $q: ng.IQService,
        $window: ng.IWindowService,
        $location: ng.ILocationService
    ) =>
        ({
            request: function (config) {
                config.headers = config.headers || {};
                config.headers['X-Requested-With'] = 'XMLHttpRequest';
                return config;
            },
            responseError: function (rejection) {
                if (rejection.status === 401 || rejection.status === 403) {
                    $location.path('/login');
                }
                return $q.reject(rejection);
            }
        })
    );

    angular.module('ReadingApp').config(function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptor');
    });

    

}
