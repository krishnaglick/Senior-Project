function ManageVolunteersViewModel() {
  this.volunteers = ko.observableArray([]);
  this.targetVolunteer = ko.observable();

  this.controller = 'Volunteer';

  this.loadVolunteers = function() {
    var action = 'GetVolunteers';
    
    app.get(this.controller, action)
    .success(function(data) {
      this.volunteers(data.volunteers);
    }.bind(this));
  }.bind(this);

  this.editVolunteer = function(data, event) {
    this.targetVolunteer(data);
    //TODO: Fix the modal opening twice :/
    $('.ui.modal.editVolunteer').modal('show');
    $('.ui.dropdown').dropdown();
  }.bind(this);
}