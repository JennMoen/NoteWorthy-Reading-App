namespace ReadingApp.Controllers {

    export class HomeController {                   //homepage images
        public message1 = "NoteWorthy Reading App";
        public message2 = "A Handy Source for Storing and Searching your Personal Reading Notes and Comments";

        public images = [
            { id: 1, image: "http://www.thelitwitch.com/wp-content/uploads/2011/05/For_Whom_Tolls.jpg" },
            { id: 2, image: "http://1.bp.blogspot.com/-XuQSpzI4B3U/Tea6PhfeDLI/AAAAAAAACgQ/Mire05ZL4EU/s1600/Rooftop%2BThe%2BGood%2BEarth0001.jpg" },
            { id: 3, image: "http://upload.wikimedia.org/wikipedia/en/thumb/4/4b/Crimeandpunishmentcover.png/200px-Crimeandpunishmentcover.png" }
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

        public editComment = false;

        constructor(private $http: ng.IHttpService, private $stateParams: ng.ui.IStateParamsService, private $state: ng.ui.IStateService) {
            $http.get(`/api/resource/${$stateParams['resourceid']}`)
                .then((response) => {

                    this.resource = response.data;

                });

        }

        public postComment(comment) {
            this.$http.post(`/api/resource/${this.$stateParams['resourceid']}/comments`, comment)
                .then((response) => {
                    this.$state.reload();
                })
                .catch((reason) => {
                    console.log(reason);
                });

        }
        //this.$stateParams['id']
        //${resource.id }
        public deleteResource(resource) {
            this.$http.delete(`/api/resource/${this.$stateParams['resourceid']}`)
                .then((results) => {
                    this.$state.go('booksview');
                })
                .catch((reason) => {
                    console.log(reason);
                });

        }

        public deleteComment(comment) {
            this.$http.delete(`/api/comments/${comment.id}`).then((results) => {
                // edit the array of comments on the page
                this.$state.reload();
            })
                .catch((reason) => {
                    console.log(reason);
                });

        }
        //comment.id
        public updateComment(comment) {
            this.$http.put(`/api/comments/${this.$stateParams['commentId']}`, comment ).then((results) => {
                this.$state.reload();
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


    export class CommentDetailController {

        public comment;

        constructor(private $http: ng.IHttpService, private $stateParams: ng.ui.IStateParamsService, private $state: ng.ui.IStateService) {
            $http.get(`/api/comments/${$stateParams['id']}`)
                .then((response) => {

                    this.comment = response.data;

                });
        }

        public deleteComment(comment) {
            this.$http.delete(`/api/comments/${comment.id}`, comment).then((results) => {
                this.$state.reload();
            })
                .catch((reason) => {
                    console.log(reason);
                });
        }

        public editComment(comment) {
            this.$http.patch(`/api/comments/${this.$stateParams['id']}`, comment).then((results) => {
                this.$state.go('home');
            })
                .catch((reason) => {
                    console.log(reason);
                });



        }

    }

}