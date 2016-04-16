ko.components.register(`phoneNumbers`, {
  viewModel: function(params) {
    this.data = params.data;
  },
  template:
  `<button class="ui positive labeled icon button" style="margin-bottom: 10px;" data-bind="click: $root.addPhoneNumber">
      <i class="plus icon"></i>
      Add A Phone Number
    </button>
    <!-- ko foreach: data -->
    <div class="field phoneNumber">
      <div class="ui icon input close">
        <input type="text" class="phoneField" placeholder="XXX-XXX-XXXX" data-bind="textInput: phoneNumber" />
        <i class="clickable link close icon" data-bind="click: $root.removePhoneNumber"></i>
      </div>
    </div>
    <!-- /ko -->`,
  synchronous: true
});