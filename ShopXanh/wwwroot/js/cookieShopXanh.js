function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}
function cookieYeuThichAdd(id) {
    var cname = "SanPham" + id;
    setCookie(cname, "YeuThich", 7);
    showNotification();
}
function cookieCartAdd(id) {
    var cname = "Cart" + id;
    setCookie(cname, "Cart", 7);
    showNotification();
}
function showNotification() {
    var notification = document.getElementById("notification");
    notification.style.display = "block"; // Hiển thị thông báo

    setTimeout(function () {
        notification.style.display = "none"; // Tắt thông báo sau 5 giây
    }, 5000);
}