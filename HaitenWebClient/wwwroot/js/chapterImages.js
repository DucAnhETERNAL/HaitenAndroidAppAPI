function fetchChapterImages() {
    var chapterId = $("#chapterId").val();
    if (!chapterId || chapterId <= 0) {
        alert("❌ Vui lòng nhập ChapterId hợp lệ!");
        return;
    }

    $.ajax({
        url: `https://localhost:7016/api/chapterimages/getImagesByChapter?ChapterId=${chapterId}`,
        type: "GET",
        success: function (response) {
            if (response.length === 0) {
                $("#imageContainer").html("<p class='text-center'>❌ Không có hình ảnh nào!</p>");
                return;
            }

            var html = "";
            response.forEach(img => {
                html += `<img src="${img.imageUrl}" alt="Chapter Image">`;
            });

            $("#imageContainer").html(html);
        },
        error: function () {
            alert("❌ Lỗi khi lấy danh sách ảnh!");
        }
    });
}
