$(function() {
  window.app = new App();

  ko.applyBindings(app, $('title')[0]);

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
  }).call(this);

  (function loadFooter() {
    $('#footer').load('partials/footer.html');
  }).call(this);

  (function restoreSession() {
    var userCookieData = Cookies.get('user');
    if(!userCookieData) return;

    var user = JSON.parse(userCookieData);
    app.authToken(user.authToken);
    app.username(user.username);
    app.roles(user.roles);
  }).call(this);

  (function createRouter() {
    window.router = new Router(renderArea, partials);

    var loginVM = new LoginViewModel();
    app.logout = loginVM.logout;
    var createVolunteerVM = new CreateVolunteerViewModel();
    window.manageVolunteersVM = new ManageVolunteersViewModel();
    var reportingVM = new ReportingViewModel();
    window.reportingVM = reportingVM;
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

      PatronCheckIn: {
        url: 'partials/patronCheckIn.html',
        name: 'Patron Check In',
        id: 'patronCheckIn',
        routeAction: function () {
          $('.ui.dropdown').dropdown();
          $('.ui.modal').modal();
        }
      },

      Reporting: {
        url: 'partials/admin/reporting.html',
        name: 'Reporting',
        id: 'reporting',
        vm: reportingVM,
        routeAction: function() {
          $('.ui.serviceSelection.dropdown')
          .dropdown({
            action: 'activate',
            onChange: function(value, text) {
              reportingVM.reportingType(value);
            }
          });
          $('.ui.timePeriod.dropdown')
          .dropdown({
            action: 'activate',
            onChange: function(value, text) {
              reportingVM.timePeriod(value);
            }
          });
          $('.ui.dropdown.component')
          .dropdown();
        }
      },

      Logout: {
        url: 'partials/mainPage.html',
        name: 'Logout',
        id: 'logout'
      },

      CreateVolunteer: {
        url: 'partials/admin/createVolunteer.html',
        name: 'Create Volunteer',
        id: 'createVolunteer',
        vm: createVolunteerVM
      },

      ManageVolunteers: {
        url: 'partials/admin/manageVolunteers.html',
        name: 'Manage Volunteers',
        id: 'manageVolunteers',
        vm: manageVolunteersVM,
        routeAction: function() {
          this.loadVolunteers();
          this.loadRoles();
        }.bind(manageVolunteersVM)
      },

      default: 'Home'
    };

    router.registerRouting(app.pageTitle, routes);
  }).call(this);
});