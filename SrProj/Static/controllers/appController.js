$(function() {
  window.app = new App();

  ko.applyBindings(app, $('title')[0]);
  createRouter();

  function createRouter() {
    window.router = new Router(renderArea, partials);

    var loginVM = new LoginViewModel();
    app.loginVM = loginVM;
    var createVolunteerVM = new CreateVolunteerViewModel();
    var routes = {
      Home: {
          url: 'partials/mainPage.html',
          name: 'Home',
          id: 'mainPage'
      },

      Login: {
        url: 'partials/login.html',
        name: 'Login',
        id: 'login',
        vm: loginVM
      },

      Logout: {
        url: 'partials/mainPage.html',
        name: 'Logout',
        id: 'Logout',
        routeAction: function() {
          app.username('');
        }
      },

      CreateVolunteer: {
        url: 'partials/createVolunteer.html',
        name: 'CreateVolunteer',
        id: 'CreateVolunteer',
        vm: createVolunteerVM
      },

      default: 'Home'
    };

    router.registerRouting(app.pageTitle, routes);
  }

  (function loadHeaderAndSetupMenu(){
    $('#menu').load('partials/menu.html', function() {
      function changeActiveMenuOption() {
        var currentPage = window.location.href.split('#')[1];
        if(currentPage == 'Register') currentPage = 'Login';
        var target = currentPage ? $('.ui.menu a:contains(' +  currentPage + ')') : $('.ui.menu a:contains(Home)');

        $('.ui.menu a').removeClass('active');
        target.addClass('active');
      }

      window.addEventListener('hashchange', changeActiveMenuOption);
      changeActiveMenuOption();
      $('.ui.sidebar').sidebar();
      $('#menuOpener').click(function() {
        $('.ui.sidebar').sidebar('toggle');
      });

      (function menuToggleOnOptionSelected() {
        $('.ui.sidebar.vertical.menu').on('click', 'a.item', function() {
          $('.ui.sidebar').sidebar('hide');
        });
      })();

      ko.applyBindings(app, $('.ui.sidebar')[0]);
    });
  })();

  (function loadFooter() {
    $('#footer').load('partials/footer.html');
  })();
});