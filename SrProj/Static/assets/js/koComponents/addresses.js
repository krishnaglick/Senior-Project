ko.components.register('addresses', {
  viewModel: function(params) {
    this.data = params.data;
  },
  template:
    '<div class="clickable" data-bind="click: $root.addAddress">Add An Address <i class="clickable plus icon"></i></div>' +
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
      '<i class="clickable close icon" data-bind="click: $root.removeAddress"></i>' +
    '</div>' +
    '<!-- /ko -->'
});