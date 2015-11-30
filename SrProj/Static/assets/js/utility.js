
Array.prototype.contains = function(eqObj) {
  if(!this.length || !eqObj) return false;

  for (var i = this.length - 1; i >= 0; i--) {
    var doesContain = true;
    for(var key in this[i]) {
      if(this[i].hasOwnProperty(key) && eqObj.hasOwnProperty(key)){
        doesContain = this[i][key] === eqObj[key];
      }
    }
    if(doesContain) return true;
  }

  return false;
};
