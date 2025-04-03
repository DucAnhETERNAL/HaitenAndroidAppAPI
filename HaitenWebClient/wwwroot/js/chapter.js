const apiBaseUrl = "https://localhost:7016/api/chapter";

// Load chapters by MangaId
async function loadChapters(mangaId) {
    try {
        let url = `${apiBaseUrl}?mangaId=${mangaId}`;

        const response = await fetch(url);
        const chapters = await response.json();

        let tableBody = document.getElementById("chapterTableBody");
        tableBody.innerHTML = "";

        chapters.forEach(chapter => {
            let row = `
                <tr>
                    <td>${chapter.name}</td>
                    <td>${chapter.status}</td>
                    <td>${chapter.viewCount}</td>
                    <td>
                        <a href="/Chapter/Edit?id=${chapter.id}" class="btn btn-warning">Edit</a>
                        <button class="btn btn-success" onclick="incrementView(${chapter.id})">View+</button>
                    </td>
                </tr>
            `;
            tableBody.innerHTML += row;
        });
    } catch (error) {
        console.error("Error loading chapters:", error);
    }
}

// Optional: Implement view increment functionality
async function incrementView(chapterId) {
    // Implement increment view functionality (send PUT request or similar)
    console.log("Incrementing view for chapter", chapterId);
}
