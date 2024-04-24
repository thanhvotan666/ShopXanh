function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}
function cookieYeuThichAdd(id) {
    var cname = "SanPham" + id;
    setCookie(cname, "YeuThich", 7);
    showNotification("clickCookieYeuThich");
}
function cookieCartAdd(id) {
    var cname = "Cart" + id;
    setCookie(cname, 1, 7);
    showNotification("clickCookieCart");
}
function showNotification(tagId) {
    var notification = document.getElementById(tagId);
    notification.style.display = "block"; // Hiển thị thông báo

    setTimeout(function () {
        notification.style.display = "none"; // Tắt thông báo sau 5 giây
    }, 2000);
}
function getAllCookies() {
    var cookies = {}; // Tạo một đối tượng để lưu trữ các cookie

    var cookieString = document.cookie; // Lấy chuỗi cookie từ document.cookie

    if (cookieString !== "") {
        var cookieArray = cookieString.split(";"); // Phân chia chuỗi thành mảng các cookie

        cookieArray.forEach(function (cookie) {
            var parts = cookie.split("="); // Phân chia mỗi cookie thành tên và giá trị
            var name = parts.shift().trim(); // Lấy tên cookie và loại bỏ các ký tự trắng
            var value = decodeURIComponent(parts.join("=")); // Lấy giá trị cookie và giải mã URI

            cookies[name] = value; // Thêm cookie vào đối tượng cookies
        });
    }

    return cookies; // Trả về đối tượng chứa tất cả các cookie
}