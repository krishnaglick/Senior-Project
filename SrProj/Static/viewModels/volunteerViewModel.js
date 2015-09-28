function VolunteerViewModel() {
  this.username = ko.observable();
  this.password = ko.observable();

  this.apiRoute = 'Volunteer/';

  this.login = function() {
    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        contentType: "application/json",
        url: '../' + app.apiBase + this.apiRoute + 'Login',
        data: ko.toJSON(this),
        success: function(data, textStatus, request) {
          debugger;
          var authToken = request.getResponseHeader('authToken');
          Cookies.set('authToken', authToken, Cookies.defaults);

          this.forwardUserIntoApp();
        }.bind(this),

        error: function(data) {
          debugger;
          var errorID = data.responseJson.id;
        }.bind(this),

        complete: function() {
          this.actionEnd();
        }.bind(this)
      });
  }.bind(this);

  this.register = function() {
    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        contentType: "application/json",
        url: '../' + app.apiBase + this.apiRoute + 'CreateVolunteer',
        data: ko.toJSON(this),
        success: function(data, textStatus, request) {
          debugger;
        }.bind(this),

        error: function(data) {
          debugger;
          var errorID = data.responseJson.id;
        }.bind(this),

        complete: function() {
          this.actionEnd();
        }.bind(this)
      });
  }.bind(this);
}