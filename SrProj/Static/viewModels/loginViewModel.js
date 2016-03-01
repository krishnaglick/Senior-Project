function LoginViewModel() {
  this.username = ko.observable();
  this.password = ko.observable();

  this.controller = 'Login';

  this.clearCredentials = function() {
    this.username('');
    this.password('');
  }.bind(this);

  this.enterLogin = function(data, event) {
      var enterKeyCode = 13;
      if(event && event.keyCode === enterKeyCode) {
        this.login();
        return false;
      }
      return true;
  };

  this.login = function(data, event) {
    var action = 'login';

    app.post(this.controller, action, ko.toJSON(this))
      .success(function(data, textStatus, request) {
        var authToken = request.getResponseHeader('authToken');
        if(authToken) {
          app.authToken(authToken);
          app.username(this.username());
          app.services(data.allowedServices);
          app.roles(data.roles);
          this.clearCredentials();
          //TODO: Let user know they logged in.
          alert('You\'re logged in!');
          window.location = window.location.href.split('#')[0] + '#PatronCheckIn';
        }
        else {
          //Bad things
          alert('There was a problem logging you in, please try again!');
        }
      }.bind(this))
      .error(function(data) {
        if(data.responseJSON){
          if(Array.isArray(data.responseJSON) && data.responseJSON.length > 1) {
            //Aggregate errors

            return;
          }
          else if(Array.isArray(data.responseJSON) && data.responseJSON.length == 1) {
            data.responseJSON = data.responseJSON[0];
          }

          //Handle single error.
          alert('Invalid username or password.');
        }
        //debugger;
      }.bind(this));
  }.bind(this);

  this.logout = function() {
    var action = 'logout';
    app.get(this.controller, action);
    app.clearCredentials();
    window.location.href = '#';
  }.bind(this);
}