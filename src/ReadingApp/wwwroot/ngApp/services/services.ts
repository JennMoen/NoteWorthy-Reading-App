namespace ReadingApp.Services {

    export class CommentService {

        public comments;

        constructor(private $http: ng.IHttpService, private $stateParams: ng.ui.IStateParamsService) { }


        

        public searchComments(searchTerms) {

            this.$http.get('/api/comments/search', {
                params: {
                    searchTerms: searchTerms
                }
            })
                .then((response) => {
                    this.comments = response.data;
                })
                .catch((reason) => {
                    console.log(reason)
                });
        }

    }

    angular.module('ReadingApp').service('commentService', CommentService);



    export class SearchService {
        public resources;

        constructor(public $http: ng.IHttpService) { }

        public doSearch(search) {
            this.$http.get<any>('https://www.googleapis.com/books/v1/volumes?', {
                params: {
                    q: search,
                    ApiKey: 'AIzaSyC7nCJcUIcaUOy76BVyJjMlaGcqag5mzdI',
                    maxResults: 40
                }
            })
                .then((results) => {

                    this.resources = results.data.items.map(ele => {
                        console.log(ele.volumeInfo.title);
                        console.log(ele.volumeInfo.authors);
                        return {
                            author: ele.volumeInfo.authors && ele.volumeInfo.authors[0],
                            title: ele.volumeInfo.title,
                            imageUrl: ele.volumeInfo.imageLinks && ele.volumeInfo.imageLinks.thumbnail,
                            link: ele.volumeInfo.infoLink
                        };
                    });

                    console.log(this.resources);

                });

        }
    }


    angular.module('ReadingApp').service('searchService', SearchService);
}



