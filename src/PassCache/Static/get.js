function decrypt() {
    var encrypted = document.getElementById('encrypted').value;
    var pass = document.getElementById('pass').value.replace(/ /g, '');
    document.getElementById('data').value = sjcl.decrypt(pass, encrypted);
}