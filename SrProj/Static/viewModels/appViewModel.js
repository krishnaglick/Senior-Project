function App() {
  this.pageTitle = ko.observable('Home');

  this.apiBase = 'API';

  this.authToken = '';
  this.username = '';
  this.headers = function() {
    return {
      authToken: this.authToken,
      username: this.username
    };
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
        url: this.apiBase + '/' + controller + '/' + action,
        complete: function() {
          this.actionEnd();
        }.bind(this)
      });
  }.bind(this);
}