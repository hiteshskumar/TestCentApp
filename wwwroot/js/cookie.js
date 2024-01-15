function readCookie() {
    return document.cookie;
}

function deleteSession() {
    console.log("Deleting  session");
    document.cookie.split(';').forEach(deleteCookie);
}

function deleteCookie(cookie, index) {
    var index = cookie.indexOf('=');
    if (index == -1) {
        return;
    }
    var key = cookie.substring(0, index);
    var value = cookie.substring(index + 1);
    var d = new Date();
    d.setTime(d.getTime() + -999 * 24 * 60 * 60 * 1000);
    expires = "expires=" + d.toUTCString();
    document.cookie = key + "=" + value + ";" + expires + ";path=/";
}
