ko.components.register('emergencyContacts', {
  viewModel: function(params) {
    this.data = params.data;
  },
  template:
    '<div class="clickable" data-bind="click: $root.addEmergencyContact">Add An Emergency Contact <i class="clickable plus icon"></i></div>' +
    '<!-- ko foreach: data -->' +
    '<div class="fields">' +
      '<div class="field">' +
        '<input type="text" placeholder="First Name" data-bind="textInput: firstName" />' +
      '</div>' +
      '<div class="field">' +
        '<input type="text" placeholder="Last Name" data-bind="textInput: lastName" />' +
      '</div>' +
      '<div class="field">' +
        '<input type="text" placeholder="XXX-XXX-XXXX" data-bind="textInput: phoneNumber" />' +
      '</div>' +
      '<div class="field">' +
        '<button class="circular ui icon button" data-bind="click: $root.removeEmergencyContact">' +
          '<i class="close icon"></i>' +
        '</button>' +
      '</div>' +
    '</div>' +
    '<!-- /ko -->'
});