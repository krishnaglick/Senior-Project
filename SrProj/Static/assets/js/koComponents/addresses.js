ko.components.register('addresses', {
  viewModel: function(params) {
    this.data = params.data;
  },
  template:
    '<button class="ui positive labeled icon button" style="margin-bottom: 10px;" data-bind="click: $root.addAddress">' +
      '<i class="plus icon"></i>' +
      'Add An Address' +
    '</button>' +
    '<!-- ko foreach: data -->' +
    '<div class="fields">' +
      '<div class="field">' +
        '<input type="text" placeholder="Street Address" data-bind="textInput: streetAddress" />' +
      '</div>' +
      '<div class="field">' +
        '<input type="text" placeholder="City" data-bind="textInput: city">' +
      '</div>' +
      '<div class="field">' +
        '<input type="text" placeholder="State" data-bind="textInput: state"></input>' +
      '</div>' +
      '<div class="field">' +
        '<input type="text" placeholder="Zip Code" data-bind="textInput: zip"></input>' +
      '</div>' +
      '<div class="field">' +
        '<button class="circular red ui icon button" data-bind="click: $root.removeAddress">' +
          '<i class="close icon"></i>' +
        '</button>' +
      '</div>' +
    '</div>' +
    '<!-- /ko -->'
});