import ApiService from "/js/ApiService.js";
document.addEventListener("DOMContentLoaded", function () {
    const jwtToken = localStorage.getItem("jwtToken");

    if (jwtToken) {
        // Sử dụng ApiService để lấy thông tin người dùng từ backend
        ApiService.get("google-login/profile", {}, { Authorization: `Bearer ${jwtToken}` })
            .then(response => {
                // Lấy thông tin người dùng từ phản hồi
                const userId = response.userId;
                const userName = response.userName;
                const role = response.role;
                localStorage.setItem("userId", userId);
                localStorage.setItem("userName", userName);




                // Hiển thị tên người dùng trong dropdown
                const userInfoNav = document.getElementById("userInfoNav");
                const loginNav = document.getElementById("loginNav");
                const adminButton = document.getElementById("adminButton"); 
                if (userInfoNav && loginNav) {
                    userInfoNav.style.display = "block";
                    loginNav.style.display = "none";

                    // Cập nhật tên người dùng
                    const userDropdown = document.getElementById("userDropdown");
                    userDropdown.textContent = userName;
                    if (role === "Admin") {
                        // Hiển thị nút quản lý cho admin
                        if (adminButton) {
                            adminButton.style.display = "block";  // Hiển thị nút quản lý
                        }
                    } else {
                        // Ẩn nút quản lý nếu không phải admin
                        if (adminButton) {
                            adminButton.style.display = "none";
                        }
                    }
                }
            })
            .catch(error => {
                console.error("Không thể lấy thông tin người dùng:", error);
    
            });

        document.addEventListener("DOMContentLoaded", function () {
            const jwtToken = localStorage.getItem("jwtToken");

            if (jwtToken) {
                ApiService.get("google-login/profile", {}, { Authorization: `Bearer ${jwtToken}` })
                    .then(response => {
                        const userId = response.userId;
                        localStorage.setItem("userId", userId);
                        sessionStorage.setItem("userId", userId);  // Lưu userId vào session

                        const userName = response.userName;
                        localStorage.setItem("userName", userName);
                        sessionStorage.setItem("userName", userName);  


                    })
                    .catch(error => {
                        console.error("Không thể lấy thông tin người dùng:", error);
                    });
            }
        });

    }
    // Handle logout
    const handleLogout = () => {
        // Clear JWT token and redirect to login page
        localStorage.removeItem("jwtToken");

        // Hide the user info nav and show login nav
        const userInfoNav = document.getElementById("userInfoNav");
        const loginNav = document.getElementById("loginNav");

        if (userInfoNav && loginNav) {
            userInfoNav.style.display = "none";
            loginNav.style.display = "block"; // Show login nav
        }

        // Redirect to login page
        window.location.href = '/home';
    };

    // Attach the logout function to the button
    const logoutButton = document.getElementById("logoutButton");
    if (logoutButton) {
        logoutButton.addEventListener("click", handleLogout);
    }

    const payButton = document.getElementById("paybutton");
    if (payButton) {
        payButton.addEventListener("click", function () {
            // Redirect to payment page when "Đăng ký hội viên" button is clicked
            window.location.href = "Payment/Payments";  // Đảm bảo URL này là đúng với route thanh toán của bạn
        });
    }
});