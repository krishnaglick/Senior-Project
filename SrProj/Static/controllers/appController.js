$(function() {
  window.app = new App();

  ko.applyBindings(app, $('title')[0]);
  createRouter();

  function createRouter() {
    window.router = new Router(renderArea, partials);

    var volunteerVM = new VolunteerViewModel();
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
        vm: volunteerVM
      },

      Register: {
        url: 'partials/register.html',
        name: 'Register',
        id: 'register',
        vm: volunteerVM
      },

      default: 'Home'
    };

    router.registerRouting(app.pageTitle, routes);
  }

  (function loadHeaderAndSetupMenu(){
    $('#header').load('partials/header.html', function() {
      function changeActiveMenuOption() {
        var currentPage = window.location.href.split('#')[1];
        if(currentPage == 'Register') currentPage = 'Login';
        var target = currentPage ? $('.ui.menu a:contains(' +  currentPage + ')') : $('.ui.menu a:contains(Home)');

        $('.ui.menu a').removeClass('active');
        target.addClass('active');
      }

      window.addEventListener('hashchange', changeActiveMenuOption);
      changeActiveMenuOption();
    });
  })();

  (function loadFooter() {
    $('#footer').load('partials/footer.html');
  })();
});