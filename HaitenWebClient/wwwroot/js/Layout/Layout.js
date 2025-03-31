import ApiService from "/js/ApiService.js";
document.addEventListener("DOMContentLoaded", function () {
    const jwtToken = localStorage.getItem("jwtToken");

    if (jwtToken) {
        // Sử dụng ApiService để lấy thông tin người dùng từ backend
        ApiService.get("google-login/profile", {}, { Authorization: `Bearer ${jwtToken}` })
            .then(response => {
                // Lấy thông tin người dùng từ phản hồi
                const userName = response.userName;

                // Hiển thị tên người dùng trong dropdown
                const userInfoNav = document.getElementById("userInfoNav");
                const loginNav = document.getElementById("loginNav");

                if (userInfoNav && loginNav) {
                    userInfoNav.style.display = "block";
                    loginNav.style.display = "none";

                    // Cập nhật tên người dùng
                    const userDropdown = document.getElementById("userDropdown");
                    userDropdown.textContent = userName;
                }
            })
            .catch(error => {
                console.error("Không thể lấy thông tin người dùng:", error);
    
            });
    }
});