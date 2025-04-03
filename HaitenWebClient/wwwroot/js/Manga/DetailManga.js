import ApiService from "/js/ApiService.js";

// Lấy ID manga từ URL
const urlParams = new URLSearchParams(window.location.search);
const mangaId = urlParams.get("id");

// Lấy và hiển thị thông tin manga
async function loadMangaDetails() {
    try {
        const manga = await ApiService.get(`manga/${mangaId}`);
        document.getElementById("mangaId").textContent = manga.id;
        document.getElementById("mangaTitle").textContent = manga.title;
        document.getElementById("mangaAuthor").textContent = manga.author;
        document.getElementById("mangaDescription").textContent = manga.description;
        document.getElementById("mangaType").textContent = manga.type;
        document.getElementById("mangaGenre").textContent = manga.genreId; // Có thể cần lấy tên thể loại từ API khác
        document.getElementById("mangaStatus").textContent = manga.status;

        // Hiển thị danh sách chapter
        const chapterTableBody = document.getElementById("chapterTableBody");
        chapterTableBody.innerHTML = "";
        manga.chapters.forEach(chapter => {
            const row = `
                <tr>
                    <td>${chapter.id}</td>
                    <td>${chapter.name}</td>
                    <td>${chapter.status ? "Active" : "Inactive"}</td>
                    <td>${chapter.viewCount}</td>
                    <td>
                        <button class="btn btn-info btn-sm" onclick="editChapter(${chapter.id})">Xem và Sửa</button>
                    </td>
                </tr>
            `;
            chapterTableBody.innerHTML += row;
        });
    } catch (error) {
        console.error("Lỗi khi lấy thông tin manga:", error);
    }
}

// Xử lý thêm chapter mới
window.handleAddChapter = async () => {
    const chapterData = {
        name: document.getElementById("chapterName").value,
        content: document.getElementById("chapterContent").value, // Thêm trường content
        status: document.getElementById("chapterStatus").value === "true",
        mangaId: parseInt(document.getElementById("mangaIdForChapter").value)
    };

    try {
        await ApiService.post("chapter", chapterData);
        alert("Thêm Chapter thành công!");
        document.getElementById("addChapterForm").reset(); // Reset form
        loadMangaDetails(); // Tải lại danh sách chapter
    } catch (error) {
        console.error("Lỗi khi thêm Chapter:", error);
        alert("Đã có lỗi xảy ra khi thêm Chapter.");
    }
};
function editChapter(chapterId) {
    console.log("Chỉnh sửa Chapter có ID: " + chapterId);

    // Gọi API để lấy thông tin chapter cơ bản
    const chapterPromise = ApiService.get(`chapter/${chapterId}`);
    // Gọi API để lấy nội dung chapter từ ChapterText
    const chapterTextPromise = ApiService.get(`chaptertext/${chapterId}`);

    // Chờ cả hai API trả về kết quả
    Promise.all([chapterPromise, chapterTextPromise])
        .then(([chapter, chapterText]) => {
            // Điền dữ liệu vào form chỉnh sửa
            document.getElementById("editChapterId").value = chapter.id;
            document.getElementById("editChapterName").value = chapter.name;
            document.getElementById("editChapterContent").value = chapterText.content || ""; // Nội dung từ ChapterText
            

            // Hiển thị modal chỉnh sửa
            document.getElementById("editChapterModal").style.display = "block";
        })
        .catch(error => {
            console.error("Lỗi khi lấy thông tin chapter:", error);
            alert("Không thể lấy thông tin chapter. Vui lòng thử lại.");
        });
}
window.handleEditChapter = async () => {
    const chapterData = {
        name: document.getElementById("editChapterName").value,
        content: document.getElementById("editChapterContent").value,
        status: false
       
    };

    const chapterId = document.getElementById("editChapterId").value;

    try {
        // Gửi yêu cầu cập nhật chapter qua API
        await ApiService.put(`chapter/${chapterId}`, chapterData);

        alert("Cập nhật Chapter thành công!");
        closeEditChapterModal();
        loadMangaDetails(); // Tải lại danh sách chapter
    } catch (error) {
        console.error("Lỗi khi cập nhật Chapter:", error);
        alert("Đã có lỗi xảy ra khi cập nhật Chapter.");
    }
};
function closeEditChapterModal() {
    document.getElementById("editChapterModal").style.display = "none";
}
window.editChapter = editChapter;
window.closeEditChapterModal = closeEditChapterModal; // Đưa vào phạm vi toàn cục để sử dụng trong HTML
// Xem chi tiết chapter (có thể tùy chỉnh)
function viewChapterDetails(chapterId) {
    window.location.href = `/chapter/${chapterId}`;
}

// Gọi hàm loadMangaDetails khi trang được tải
document.addEventListener("DOMContentLoaded", loadMangaDetails);