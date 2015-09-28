var pass = '';
var raw = '';
var fullUrl = '';
var urlP = Uheprng();
var passwordP = Uheprng();
var result = '';
var id = '';


// ---------------------------------------------------------------------
// Set up pseudo random number generators for the url token and password
// ---------------------------------------------------------------------
urlP.initState();
passwordP.initState();
urlP.addEntropy();
passwordP.addEntropy();

// ---------------------------------------------------------------------
//                        Interaction handlers
// ---------------------------------------------------------------------

raw = sjcl.codec.base64.fromBits(sjcl.hash.sha256.hash(urlP.string(64)), true);
id = encodeURIComponent(raw);
fullUrl = url + '?id=' + id;
pass = sjcl.codec.base64.fromBits(sjcl.hash.sha256.hash(passwordP.string(64)), true);
fullUrl += '#' + pass;

//var data = document.getElementById('data').value;
result = sjcl.encrypt(pass, plaintext);