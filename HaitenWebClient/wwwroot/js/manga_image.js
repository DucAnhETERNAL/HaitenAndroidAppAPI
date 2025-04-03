const apiBaseUrl = "https://localhost:7016/api/manga";

// Load manga list from API
async function loadMangaList() {
    try {
        const response = await fetch(apiBaseUrl);
        const mangas = await response.json();

        let tableBody = document.getElementById("mangaTableBody");
        tableBody.innerHTML = "";  // Clear the loading text

        mangas.forEach(manga => {
            let row = `
                <tr>
                    <td>${manga.title}</td>
                    <td>${manga.author}</td>
                    <td>${manga.genreName}</td>
                    <td>${manga.status}</td>
                    <td>${manga.averageRating}</td>
                    <td>
                        <a href="/Chapter/Index?mangaId=${manga.id}" class="btn btn-primary">Đọc truyện</a>
                    </td>
                </tr>
            `;
            tableBody.innerHTML += row;
        });
    } catch (error) {
        console.error("Error loading manga list:", error);
    }
}

// Load manga list on page load
document.addEventListener("DOMContentLoaded", () => {
    loadMangaList();  // Load manga list when the page loads
});
