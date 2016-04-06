ko.components.register('addresses', {
  viewModel: function(params) {
    this.data = params.data;
  },
  template:`
    <button class="ui positive labeled icon button" style="margin-bottom: 10px;" data-bind="click: $root.addAddress">
      <i class="plus icon"></i>
      Add An Address
    </button>
    <!-- ko foreach: data -->
    <div class="ui address segment">
      <div class="clickable floating ui red label" data-bind="click: $root.removeAddress">
        Remove
      </div>
      <div class="fields">
        <div class="field">
          <input type="text" placeholder="Street Address" data-bind="textInput: streetAddress" />
        </div>
        <div class="field">
          <input type="text" placeholder="City" data-bind="textInput: city">
        </div>
      </div>
      <div class="fields">
        <div class="field">
          <input type="text" placeholder="State" data-bind="textInput: state"></input>
        </div>
        <div class="field">
          <input type="text" class="zipField" placeholder="Zip Code" data-bind="textInput: zip"></input>
        </div>
      </div>
    </div>
    <!-- /ko -->`
});