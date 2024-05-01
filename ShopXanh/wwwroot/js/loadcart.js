window.addEventListener('load', function () {
    LoadCart();
});
function LoadCart() {
    var numCart = 0;
    var cookieString = document.cookie; // Lấy chuỗi cookie từ document.cookie
    if (cookieString !== "") {
        var cookieArray = cookieString.split(";"); // Phân chia chuỗi thành mảng các cookie
        cookieArray.forEach(function (cookie) {
            var parts = cookie.split("=");
            var name = parts.shift().trim();
            if (name.includes("Cart")) {
                numCart++;
            }
        });
    }
    document.getElementById("numCart").innerText = "[" + numCart.toString() + "]";
}