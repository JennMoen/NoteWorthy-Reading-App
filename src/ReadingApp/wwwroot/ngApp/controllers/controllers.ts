namespace ReadingApp.Controllers {

    export class HomeController {                   //homepage images
        public message1 = "NoteWorthy Reading App";
        public message2 = "A Handy Source for Storing and Searching your Personal Reading Notes and Comments";

        public images = [
            { id: 1, image: "http://www.thelitwitch.com/wp-content/uploads/2011/05/For_Whom_Tolls.jpg", text: "Books" },
            { id: 2, image: "http://cdn.slashgear.com/wp-content/uploads/2012/10/Jobs_Newsweek.jpeg", text: "Magazines" },
            { id: 3, image: "https://lh5.ggpht.com/s5DuS8_GWnjvGd-Ypdxd9-5S3H3ul_82CFMomN7OgTYBM223Sxnf-qOZLxPk0owqUAw=w300", text: "E-Sources" }
        ]

    }


    export class SecretController {
        public secrets;

        constructor($http: ng.IHttpService) {
            $http.get('/api/secrets').then((results) => {
                this.secrets = results.data;
            });
        }
    }


    export class ResourceController {       //for booksview state

        public resources;

        constructor(private $http: ng.IHttpService) {
            $http.get('/api/resource')
                .then((results) => {
                    this.resources = results.data;
                });

        }

    }


    export class ResourceDetailsController {      //view a book, view comments, add comment

        public resource;
        public comments;

        constructor(private $http: ng.IHttpService, private $stateParams: ng.ui.IStateParamsService, private $state: ng.ui.IStateService) {
            $http.get(`/api/resource/${$stateParams['id']}`)
                .then((response) => {

                    this.resource = response.data;

                });

        }

        public postComment(comment) {
            this.$http.post(`/api/resource/${this.$stateParams['id']}/comments`, comment)
                .then((response) => {
                    this.$state.reload;
                })
                .catch((reason) => {
                    console.log(reason);
                });

        }

        public deleteResource(resource) {
            this.$http.delete(`/api/resource/${this.$stateParams['id']}`, resource)
                .then((response) => {
                    this.$state.go('booksview');
                })
                .catch((reason) => {
                    console.log(reason);
                });

        }

        public deleteComment(comment) {
            this.$http.delete(`/api/resource/${this.$stateParams['id']}/comments`, comment).then((results) => {
                this.$state.go('bookdetail');
            })
                .catch((reason) => {
                    console.log(reason);
                });
        }

    }

    export class SearchController {      //search books to add, add from list or manually

        public doSearch;

        constructor(public searchService: ReadingApp.Services.SearchService, private $http: ng.IHttpService, private $state: ng.ui.IStateService) {
            this.doSearch = searchService.doSearch.bind(searchService);
        }

        public get resources() {
            return this.searchService.resources;

        }


        public addResource(resource) {
            this.$http.post('/api/resource', resource)

                .then((response) => {
                    this.$state.go('booksview');
                })
                .catch((reason) => {
                    console.log(reason);
                });

        }

    }

    export class CommentController {        //searches through comments for keywords

        public searchComments;

        constructor(public commentService: ReadingApp.Services.CommentService, private $http: ng.IHttpService, private $stateParams: ng.ui.IStateParamsService) {
            this.searchComments = commentService.searchComments.bind(commentService);
        }

        public get comments() {
            return this.commentService.comments;

        }

    }


}