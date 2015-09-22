$(function() {
  window.app_view_model = new AppViewModel();

  ko.applyBindings(app_view_model, $('title')[0]);
  createRouter();

  function createRouter() {
    window.router = new Router(renderArea, partials);

    var routes = {
      Home: {
          url: 'partials/mainPage.html',
          name: 'Home',
          id: 'mainPage'
      },

      Login: {
        url: 'partials/login.html',
        name: 'Login',
        id: 'login'
      },

      default: 'Home'
    };

    router.registerRouting(app_view_model, routes);
  }
});