function Router(renderElement, partialContainer) {

  this.contentArea = $(renderElement);
  this.partialHolder = $(partialContainer);

  this.pageTitle = function() { throw 'NotImplementedException'; };

  this.routes = {
    sampleRoute: {
      url: 'path/to/partial', //The html file to load.
      name: 'Sample', //This becomes the page title
      id: 'sample', //The partial is cached under this id, needs to be unique
      vm: function() { throw 'NotImplementedException'; }, //The viewmodel associated with the route
      routeAction: function() { throw 'NotImplementedException'; } //An action to take upon loading the route
    },
    default: ''
  };

  this.routeTransitionBegin = function() {};
  this.routeTransitionEnd = function() {};

  this.addPartialToPartialHolder = function(idOfPartial, partialHtml) {
    this.partialHolder.append('<span id="' + idOfPartial + '">' +
      partialHtml +
      '</span>');
  }.bind(this);

  this.loadContent = function(route) {
    if(!this.routes[route])
      route = this.routes.default;

    function loadRouteContent(route) {
      this.routeTransitionBegin();

      if(this.contentArea[0] && !!ko.dataFor(this.contentArea[0])) {
        ko.cleanNode(this.contentArea[0]);
      }

      function applyBinding() {
        if(this.routes[route].vm) {
          if(typeof route === 'function'){
            ko.applyBindings(this.routes[route].vm(), this.contentArea[0]);
          }
          else {
            ko.applyBindings(this.routes[route].vm, this.contentArea[0]);
          }
        }
      }

      var routeAction = this.routes[route].routeAction;

      //If partial is not on page
      if(true || !window[this.routes[route].id]) {
        this.contentArea.load(this.routes[route].url, function() {
          applyBinding.call(this);
          if(routeAction) routeAction();
          //this.addPartialToPartialHolder(this.routes[route].id, this.contentArea.html());
          this.routeTransitionEnd();
        }.bind(this));
      }
      else {
        /*this.contentArea.html($(window[this.routes[route].id]).html());
        applyBinding.call(this);
        if(routeAction) routeAction();
        this.routeTransitionEnd();*/
      }
    }

    this.pageTitle(this.routes[route].name);
    loadRouteContent.call(this, route);
  }.bind(this);

  this.registerRouting = function(pageTitle, routes) {
    this.routes = routes;

    window.addEventListener('hashchange', function(e) {
      var route = window.location.href.split('#')[1];
      if(route) {
        this.loadContent(route);
      }
      else {
        this.loadContent(this.routes.default);
      }
    }.bind(this));
    
    this.pageTitle = pageTitle;

    this.loadDefaultRoute();
  }.bind(this);

  this.loadDefaultRoute = function() {
    var initialRoute = window.location.href.split('#')[1] || this.routes.default;
    this.loadContent(initialRoute);
  };
}