function CreateVolunteerViewModel() {
  this.username = ko.observable();
  this.password = ko.observable();
  this.confirmPassword = ko.observable();

  this.controller = 'Volunteer';

  this.clearCredentials = function() {
    this.username('');
    this.password('');
    this.confirmPassword('');
  }.bind(this);

  this.validate = function() {
    var errors = [];
    if(!this.username()) {
      errors.push('You need a username!');
    }

    if(this.password() != this.confirmPassword()) {
      errors.push('Your passwords don\'t match!');
    }

    return errors;
  }.bind(this);

  this.createVolunteer = function() {
    var validationErrors = this.validate();
    if(validationErrors.length) {
      alert(validationErrors);
      return;
    }

    var action = 'CreateVolunteer';

    app.post(this.controller, action, ko.toJSON(this))
      .success(function(data, textStatus, request) {
        var authToken = request.getResponseHeader('authToken');
        if(authToken) {
          app.authToken(authToken);
          app.username(this.username());
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
}