function App() {
  this.pageTitle = ko.observable('Home');

  this.apiBase = '../API';

  this.authToken = ko.observable('');
  this.username = ko.observable('');
  this.services = ko.observableArray([]);
  this.roles = ko.observableArray([]);

  this.enums = {};

  this.authToken.subscribe(cookieTracking, this);
  this.username.subscribe(cookieTracking, this);
  this.services.subscribe(cookieTracking, this);
  this.roles.subscribe(cookieTracking, this);

  function cookieTracking() {
    Cookies.set('user', JSON.stringify(this.headers()));
  }

  this.isAdmin = ko.computed(function() {
    return this.roles().map(function(role) {
      return role.roleName;
    }).indexOf('Admin') > -1;
  }, this);

  this.headers = function() {
    return {
      authToken: this.authToken(),
      username: this.username(),
      roles: this.roles(),
      services: this.services()
    };
  }.bind(this);

  this.logout = function() {
    throw 'Logout function not assigned!';
  };

  this.clearCredentials = function() {
    this.authToken('');
    this.username('');
    this.services([]);
    this.roles([]);
  }.bind(this);

  this.actionBegin = function() { };
  this.actionEnd = function() { };

  this.post = function(controller, action, data) {
    this.actionBegin();

    return $.ajax({
        type: 'POST',
        dataType: 'JSON',
        contentType: "application/json",
        headers: this.headers(),
        url: this.apiBase + '/' + controller + '/' + action,
        data: data,
        success: function(data, textStatus, request) {
          var authToken = request.getResponseHeader('authToken');
          if(authToken)
            app.authToken(authToken);
        }.bind(this),
        complete: function() {
          this.actionEnd();
        }.bind(this)
      });
  }.bind(this);

  this.get = function(controller, action) {
    this.actionBegin();

    return $.ajax({
        type: 'GET',
        dataType: 'JSON',
        contentType: "application/json",
        headers: this.headers(),
        success: function(data, textStatus, request) {
          var authToken = request.getResponseHeader('authToken');
          if(authToken)
            app.authToken(authToken);
        }.bind(this),
        url: this.apiBase + '/' + controller + '/' + action,
        complete: function() {
          this.actionEnd();
        }.bind(this)
      });
  }.bind(this);
}