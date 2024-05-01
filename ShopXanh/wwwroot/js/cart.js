

var cookies = {}; // Tạo một đối tượng để lưu trữ các cookie

window.addEventListener('load', function () {
    var subTotal = 0;
    var cookieString = document.cookie; // Lấy chuỗi cookie từ document.cookie

    if (cookieString !== "") {
        var cookieArray = cookieString.split(";"); // Phân chia chuỗi thành mảng các cookie
        cookieArray.forEach(function (cookie) {
            var parts = cookie.split("=");
            var name = parts.shift().trim();
            if (name.includes("Cart")) {
                var value = decodeURIComponent(parts.join("="));
                var id = name.replace("Cart", "");
                document.getElementById("quantity_" + id).value = value;    
                var numPrice = parseFloat(document.getElementById("price_" + id).textContent);
                var numTotal = value * numPrice;
                document.getElementById("productTotal_" + id).innerText = numTotal.toString();
                subTotal += parseInt(document.getElementById("productTotal_" + id).textContent);
                cookies[name] = value;
            }
        });
        document.getElementById("subtotal").innerText = subTotal.toString();
        var numDilivery = parseFloat(document.getElementById("delivery").textContent);
        var numDiscount = parseFloat(document.getElementById("discount").textContent);
        var numTotal = subTotal + numDilivery - numDiscount;
        document.getElementById("total").innerText = numTotal.toString();
        document.getElementById("TotalPayment").value = numTotal;
    }
});
function changeTotal(id) {
    var numPrice = parseFloat(document.getElementById("price_" + id).textContent);
    var numQuantity = parseInt(document.getElementById("quantity_" + id).value);
    var numProductTotal = numQuantity * numPrice;
    document.getElementById("productTotal_" + id).innerText = "" + numProductTotal;
}
function loadAllTotal() {
    var subTotal = 0;   
    for (var name in cookies) {
        var idOfName = name.replace("Cart", "");
        var value = cookies[name];
        var numQuantity = parseInt(document.getElementById("quantity_" + idOfName).value);
        var numPrice = parseFloat(document.getElementById("price_" + idOfName).textContent);
        subTotal += numPrice * numQuantity;
    }
    document.getElementById("subtotal").innerText = subTotal.toString();
    var numDilivery = parseFloat(document.getElementById("delivery").textContent);
    var numDiscount = parseFloat(document.getElementById("discount").textContent);
    var numTotal = subTotal + numDilivery - numDiscount;
    document.getElementById("total").innerText = numTotal.toString();
    document.getElementById("TotalPayment").value = numTotal;
}
function changeQuantity(id) {
    var quantity = document.getElementById("quantity_" + id)
    if (quantity.value == "") { 
        quantity.value = 0;
    }
    var numQuantity = parseInt(quantity.value);
    changeTotal(id);
    setCookie("Cart"+id,numQuantity,7);
    loadAllTotal();
}
