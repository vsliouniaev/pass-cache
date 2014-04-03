window.onload = function() {
    var encrypted = document.getElementById('encrypted').value;
    var pass = window.location.toString().split('#');
    if (pass.length == 2) {
        document.getElementById('data').value = sjcl.decrypt(pass[1], encrypted);
    }
}