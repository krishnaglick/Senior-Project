$(function() {
  window.app = new App();

  ko.applyBindings(app, $('title')[0]);
  createRouter();

  function createRouter() {
    window.router = new Router(renderArea, partials);

    var routes = {
      Home: {
          url: 'partials/mainPage.html',
          name: 'Home',
          id: 'mainPage'
      },

      //TODO: Merge login and register areas.
      Login: {
        url: 'partials/login.html',
        name: 'Login',
        id: 'login',
        vm: function() { return new VolunteerViewModel(); }
      },

      Register: {
        url: 'partials/register.html',
        name: 'Register',
        id: 'register',
        vm: function() { return new VolunteerViewModel(); }
      },

      default: 'Home'
    };

    router.registerRouting(app.pageTitle, routes);
  }
});