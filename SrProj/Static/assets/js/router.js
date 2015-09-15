function Router(renderElement, partialContainer) {
  //Render element should be a jquery element.
  this.contentArea = $(renderElement);
  //Partial Holder should be a jquery element.
  this.partialHolder = $(partialContainer);

  this.viewModel = {
    pageTitle: function() { throw 'NotImplementedException'; }
  };

  this.routes = {
    sampleRoute: {
      url: 'path/to/partial',
      name: 'Sample',
      id: 'sample'
    },
    default: ''
  };

  this.routeTransitionBegin = function() {};
  this.routeTransitionEnd = function() {};

  this.addPartialToPartialHolder = function(idOfPartial, partialHtml) {
    var newPartial = document.createElement('div');
    this.partialHolder.append('<div id="' + idOfPartial + '">' + 
      partialHtml +
      '</div>');
  }.bind(this);

  this.loadContent = function(route) {
    if(!this.routes[route])
      route = this.routes.default;
    
    function loadRouteContent(route) {
      this.routeTransitionBegin();

      if(this.contentArea[0] && !!ko.dataFor(this.contentArea[0])) {
        ko.cleanNode(this.contentArea[0]);
      }

      //If partial is not on page
      if(!$('#' + this.routes[route].id)[0]) {
        this.contentArea.load(this.routes[route].url, function() {
          ko.applyBindings(this.viewModel, this.contentArea[0]);
          this.addPartialToPartialHolder(this.routes[route].id, this.contentArea.html());
          this.routeTransitionEnd();
        }.bind(this));
      }
      else {
        this.contentArea.html($('#' + this.routes[route].id).html());
        ko.applyBindings(this.viewModel, this.contentArea[0]);
        this.routeTransitionEnd();
      }
    }

    this.viewModel.pageTitle(this.routes[route].name);
    loadRouteContent.call(this, route);
  }.bind(this);

  this.registerRouting = function(viewModel, routes) {
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
    
    this.viewModel = viewModel;

    this.loadDefaultRoute();
  }.bind(this);

  this.loadDefaultRoute = function() {
    var initialRoute = window.location.href.split('#')[1] || this.routes.default;
    this.loadContent(initialRoute);
  };
}