function App() {
  this.pageTitle = ko.observable('Home');

  this.apiBase = 'API/';

  this.authToken = '';

  this.actionBegin = function() { };
  this.actionEnd = function() { };
}