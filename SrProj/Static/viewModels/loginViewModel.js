$(function() {
  window.LoginViewModel = new ViewModelBase();
  
  LoginViewModel.username = ko.observable();
  LoginViewModel.password = ko.observable();

  LoginViewModel.login = function() {
    console.log('potato');
  }.bind(this);

  //TODO: Not this
  router.routes.Login.vm = LoginViewModel;
});