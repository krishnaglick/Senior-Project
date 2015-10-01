function VolunteerViewModel() {
  this.username = ko.observable();
  this.password = ko.observable();

  this.controller = 'Volunteer';

  this.clearCredentials = function() {
    this.username('');
    this.password('');
  }.bind(this);

  this.login = function() {
    var action = 'login';

    app.post(this.controller, action, ko.toJSON(this))
      .success(function(data, textStatus, request) {
        var authToken = request.getResponseHeader('authToken');
        if(authToken) {
          app.authToken = authToken;
          app.username = this.username();
          this.clearCredentials();
          //TODO: Let user know they logged in.
          alert('You\'re logged in!');
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

  this.register = function() {
    var action = 'CreateVolunteer';

    app.post(this.controller, action, ko.toJSON(this))
      .success(function(data, textStatus, request) {
        if(data.result == 'success') {
          //Good things
          alert('Account Created!');
        }
        else {
          //Bad things!
          alert('Bad things!');
        }
        //debugger;
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
          alert('Please try a different username!');
        }
        //debugger;
      }.bind(this));
  }.bind(this);
}