import ApiService from "/js/ApiService.js";

let userRole = "Member"; // Mặc định là Member
let currentPage = 1;
let totalPages = 1;
const mangaSearchState = {
    mangaTitle: "",
};

window.HandleInputChange = (field, value) => {
    mangaSearchState[field] = value;
    SearchManga();
};

function SearchManga() {
    const searchQuery = mangaSearchState.mangaTitle;
    loadMangas(searchQuery);
}

// Lấy thông tin người dùng
async function loadUserInfo() {
    try {
        // const response = await ApiService.get("google-login/profile");
        //if (!response) throw new Error("Không lấy được thông tin người dùng");

        //if (response.role.includes("Admin")) {
        //  userRole = "Admin";
        //}
        userRole = "Admin";
        loadMangas();
    } catch (error) {
        console.error("Lỗi khi lấy thông tin người dùng:", error);
    }   
}
window.editManga = editManga;

// Tải danh sách Manga
async function loadMangas(searchQuery = "", page = 1) {
    try {
        // Gọi API để lấy danh sách Manga
        const response = await ApiService.get("manga", {
            $filter: `contains(title, '${searchQuery}')`,  // Bộ lọc tìm kiếm
            $top: 3, // Số manga mỗi trang
            $skip: (page - 1) * 3, // Bỏ qua các manga để phân trang
        });
        const response1 = await ApiService.get("manga", {
            $filter: `contains(title, '${searchQuery}')`, // Áp dụng bộ lọc tìm kiếm cho tất cả manga
        });

        const mangaTableBody = document.getElementById("mangaTableBody");
        mangaTableBody.innerHTML = "";

        response.forEach(manga => {
            let actionColumn = "";

            if (userRole === "Admin") {
                actionColumn = `
              <button class="btn btn-primary btn-sm" onclick="editManga(${manga.id})">Sửa</button>
              <button class="btn btn-info btn-sm" onclick="viewMangaDetails('${manga.id}')">Xem</button>

    
                `;
            } else {
                actionColumn = `
                   
                `;
            }

            const row = `
                <tr>
                    <td>${manga.id}</td>
                    <td>${manga.title}</td>
                    <td>${manga.author}</td>
                   
                    <td>${manga.status}</td>
                    <td>${actionColumn}</td>
                </tr>
            `;
            mangaTableBody.innerHTML += row;
        });

        const totalMangas = response1.length;
        totalPages = Math.ceil(totalMangas / 3);   // Tính tổng số trang dựa trên tổng số manga
        renderPagination(); // Hiển thị phân trang

    } catch (error) {
        console.error("Lỗi khi tải Manga:", error);
    }
}

function renderPagination() {
    const paginationContainer = document.getElementById("pagination");
    paginationContainer.innerHTML = ""; // Xóa phân trang cũ

    // Render nút "Previous"
    if (currentPage > 1) {
        const prevButton = document.createElement("button");
        prevButton.textContent = "Previous";
        prevButton.classList.add("btn", "btn-secondary", "mx-1");
        prevButton.onclick = () => changePage(currentPage - 1);
        paginationContainer.appendChild(prevButton);
    }

    // Render các nút trang
    for (let i = 1; i <= totalPages; i++) {
        const pageButton = document.createElement("button");
        pageButton.textContent = i;
        pageButton.classList.add("btn", "btn-secondary", "mx-1");
        pageButton.onclick = () => changePage(i);
        paginationContainer.appendChild(pageButton);
    }

    // Render nút "Next"
    if (currentPage < totalPages) {
        const nextButton = document.createElement("button");
        nextButton.textContent = "Next";
        nextButton.classList.add("btn", "btn-secondary", "mx-1");
        nextButton.onclick = () => changePage(currentPage + 1);
        paginationContainer.appendChild(nextButton);
    }
}

function changePage(page) {
    currentPage = page;
    const searchQuery = document.getElementById("mangaTitleInput").value;
    loadMangas(searchQuery, page);
}

// Mở popup thêm Manga
window.openAddMangaModal = () => {
    document.getElementById("addMangaModal").style.display = "block";
};

// Đóng popup thêm Manga
window.closeAddMangaModal = () => {
    document.getElementById("addMangaModal").style.display = "none";
};

function loadAddNewModel() {
    console.log("role : " + userRole);
    if (userRole === "Admin") {
        document.getElementById("addnew-manga").style.display = "block";
    } else {
        document.getElementById("addnew-manga").style.display = "none";
    }
}

// Thêm Manga mới
window.handleAddManga = async () => {
    const mangaData = {
        title: document.getElementById("mangaTitle").value,
        author: document.getElementById("author").value,
        type: document.getElementById("type").value,
        genreId: parseInt(document.getElementById("genre").value),
        status: document.getElementById("status").value,
        description: document.getElementById("description").value,
    };

    try {
        await ApiService.post("manga", mangaData);

        alert("Thêm Manga thành công!");
        closeAddMangaModal();
        loadMangas();
    } catch (error) {
        console.error("Lỗi:", error);
    }
};
function loadGenres() {
    // Danh sách các thể loại cứng (tùy chỉnh)
    const genres = [
        { id: 1, name: 'Action' },
        { id: 2, name: 'Adventure' },
        { id: 3, name: 'Fantasy' },
        { id: 4, name: 'Romance' },
        { id: 5, name: 'Horror' },
        { id: 6, name: 'Comedy' }
    ];

    const genreSelect = document.getElementById("genre");
    genreSelect.innerHTML = ''; // Xóa các option cũ trong dropdown

    // Thêm các thể loại vào dropdown
    genres.forEach(genre => {
        const option = document.createElement("option");
        option.value = genre.id;
        option.textContent = genre.name;
        genreSelect.appendChild(option);
    });
}
function loadGenresforedit() {
    // Danh sách các thể loại cứng (tùy chỉnh)
    const genres = [
        { id: 1, name: 'Action' },
        { id: 2, name: 'Adventure' },
        { id: 3, name: 'Fantasy' },
        { id: 4, name: 'Romance' },
        { id: 5, name: 'Horror' },
        { id: 6, name: 'Comedy' }
    ];

    const genreSelect = document.getElementById("editGenre");
    genreSelect.innerHTML = ''; // Xóa các option cũ trong dropdown

    // Thêm các thể loại vào dropdown
    genres.forEach(genre => {
        const option = document.createElement("option");
        option.value = genre.id;
        option.textContent = genre.name;
        genreSelect.appendChild(option);
    });
}
// Gọi hàm loadGenres khi trang được tải
document.addEventListener("DOMContentLoaded", loadGenres);
document.addEventListener("DOMContentLoaded", loadGenresforedit);
// Xem chi tiết Manga
window.viewMangaDetails = viewMangaDetails;
function viewMangaDetails(id) {
    window.location.href = `/Admin/DetailAndAddChapter?id=${id}`;
}
document.addEventListener("DOMContentLoaded", () => {
    const buttons = document.querySelectorAll('.btn-edit-manga');
    buttons.forEach(button => {
        button.addEventListener('click', (event) => {
            const mangaId = event.target.getAttribute('data-manga-id');
            editManga(mangaId); // Gọi hàm editManga với ID manga
        });
    });
});



function editManga(id) {
    console.log("Chỉnh sửa Manga có ID: " + id); // Kiểm tra xem ID có đúng không

    // Make the API call using the ApiService
    ApiService.get(`manga/${id}`)
        .then(manga => {
            // Điền dữ liệu vào form chỉnh sửa
            document.getElementById("editMangaTitle").value = manga.title;
            document.getElementById("editAuthor").value = manga.author;
            document.getElementById("editDescription").value = manga.description;
            document.getElementById("editStatus").value = manga.status;
            document.getElementById("genre").value = manga.genreId;
            document.getElementById("editMangaId").value = manga.id;
            // Hiển thị modal chỉnh sửa
            document.getElementById("editMangaModal").style.display = "block";
        })
        .catch(error => {
            console.error("Lỗi khi lấy thông tin manga:", error);
        });
}

// Hàm đóng modal chỉnh sửa manga
function closeEditMangaModal() {
    document.getElementById("editMangaModal").style.display = "none";
}
window.handleEditManga = handleEditManga;
// Hàm xử lý khi nhấn nút "Lưu" để chỉnh sửa manga
async function handleEditManga() {
    const mangaData = {
        title: document.getElementById("editMangaTitle").value,
        author: document.getElementById("editAuthor").value,
        description: document.getElementById("editDescription").value,
          // Giả sử loại mặc định là Manga
        genreId: parseInt(document.getElementById("editGenre").value),
        status: document.getElementById("editStatus").value
    };

    const mangaId = document.getElementById("editMangaId").value;  // ID manga cần chỉnh sửa

    try {
        // Gửi yêu cầu cập nhật manga qua API
        await ApiService.put(`manga/${mangaId}`, mangaData);  // Giả sử bạn có endpoint PUT cho chỉnh sửa

        alert("Cập nhật Manga thành công!");
        closeEditMangaModal();
        loadMangas();  // Tải lại danh sách Manga sau khi chỉnh sửa
    } catch (error) {
        console.error("Lỗi khi cập nhật Manga:", error);
        alert("Đã có lỗi xảy ra khi cập nhật Manga.");
    }
}
// Xóa Manga
function deleteManga(id) {
    if (confirm("Bạn có chắc muốn xóa Manga này?")) {
        ApiService.delete("manga", id)
            .then(() => {
                alert("Manga đã được xóa!");
                loadMangas();  // Tải lại danh sách Manga
            })
            .catch(error => {
                console.error("Lỗi xóa Manga:", error);
            });
    }
}

// Xử lý sự kiện DOMContentLoaded
document.addEventListener("DOMContentLoaded", async () => {
    await loadUserInfo();
    loadAddNewModel();
});
