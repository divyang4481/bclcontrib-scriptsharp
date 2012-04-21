//! ScriptApp.Test.debug.js
//

(function() {
function executeScript() {

////////////////////////////////////////////////////////////////////////////////
// IBase

window.IBase = function() { 
};
IBase.prototype = {
    print : null
}
IBase.registerInterface('IBase');



}
ss.loader.registerScript('ScriptApp.Test', [], executeScript);
})();

//! This script was generated using Script# v0.6.3.0
