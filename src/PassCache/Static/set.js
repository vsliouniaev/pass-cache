var showCreds = false;
var stop = false;
var urlInteverval = (Math.random() * 10) + 5;
var passInterval = (Math.random() * 10) + 5;
var pass = '';
var raw = '';
var fullUrl = '';
var urlP = Uheprng();
var passwordP = Uheprng();

// ---------------------------------------------------------------------
// Set up pseudo random number generators for the url token and password
// ---------------------------------------------------------------------

urlP.initState();
urlP.initState();
urlP.addEntropy("@Model[0]");
passwordP.addEntropy("@Model[1]");

function urlG() {
    urlP.addEntropy();
    if (!stop) {
        setTimeout(urlG, urlInteverval);
    }
}

function passG() {
    passwordP.addEntropy();
    if (!stop) {
        setTimeout(passG, urlInteverval);
    }
}

urlG();
passG();

// ---------------------------------------------------------------------
//                        Interaction handlers
// ---------------------------------------------------------------------

function beforeFormSubmit() {
    stopPrngs();
    var data = document.getElementById('data').value;
    document.getElementById('id').value = raw;
    document.getElementById('encrypted').value = sjcl.encrypt(pass, data);
    showCreds = true;
};

function stopPrngs() {
    stop = true;
    raw = sjcl.codec.base64.fromBits(sjcl.hash.sha256.hash(urlP.string(64)), true);
    var id = encodeURIComponent(raw);
    fullUrl = url + '?id=' + id;
    pass = sjcl.codec.base64.fromBits(sjcl.hash.sha256.hash(passwordP.string(64)), true);
};

function afterFormSubmit() {
    if (showCreds) {
        document.title = "passcache";
        document.getElementById('result').removeAttribute("hidden");
        document.getElementById('accessUrl').innerHTML = fullUrl + '#' + pass;
        document.getElementById('inputs').innerHTML = "";
    }
    showCreds = false;
};
