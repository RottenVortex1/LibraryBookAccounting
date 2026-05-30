const apiUrl = "/api/books";

let books = [];

const form = document.getElementById("bookForm");
const tableBody = document.getElementById("booksTableBody");
const countText = document.getElementById("countText");
const searchInput = document.getElementById("searchInput");
const statusFilter = document.getElementById("statusFilter");
const newBookButton = document.getElementById("newBookButton");

const statusNames = {
    Available: "В наличии",
    Issued: "Выдана",
    WrittenOff: "Списана",
    Repair: "На ремонте"
};

async function loadBooks() {
    const response = await fetch(apiUrl);
    books = await response.json();
    renderBooks();
}

function renderBooks() {
    const search = searchInput.value.toLowerCase();
    const selectedStatus = statusFilter.value;

    const filteredBooks = books.filter(book => {
        const text = `${book.title} ${book.author} ${book.genre} ${book.description}`.toLowerCase();

        const matchesSearch = text.includes(search);
        const matchesStatus = selectedStatus === "all" || book.status === selectedStatus;

        return matchesSearch && matchesStatus;
    });

    countText.textContent = `${filteredBooks.length} книг`;

    tableBody.innerHTML = "";

    filteredBooks.forEach(book => {
        const row = document.createElement("tr");

        row.innerHTML = `
            <td>
                <div class="book-title">${book.title}</div>
                <div class="book-description">${book.description || ""}</div>
            </td>
            <td>${book.author}</td>
            <td>${book.publishYear}</td>
            <td>${book.genre}</td>
            <td>${book.inventoryNumber}</td>
            <td>${statusNames[book.status] || book.status}</td>
            <td class="actions">
                <button onclick="editBook('${book.id}')">Изм.</button>
                <button onclick="deleteBook('${book.id}')">Удалить</button>
            </td>
        `;

        tableBody.appendChild(row);
    });
}

form.addEventListener("submit", async event => {
    event.preventDefault();

    const book = {
        title: document.getElementById("title").value,
        author: document.getElementById("author").value,
        genre: document.getElementById("genre").value,
        publishYear: Number(document.getElementById("publishYear").value),
        inventoryNumber: document.getElementById("inventoryNumber").value,
        description: document.getElementById("description").value,
        status: document.getElementById("status").value
    };

    await fetch(apiUrl, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(book)
    });

    form.reset();
    await loadBooks();
});

async function deleteBook(id) {
    await fetch(`${apiUrl}/${id}`, {
        method: "DELETE"
    });

    await loadBooks();
}

async function editBook(id) {
    const book = books.find(item => item.id === id);

    if (!book) {
        return;
    }

    const newTitle = prompt("Название книги:", book.title);
    const newAuthor = prompt("Автор:", book.author);
    const newGenre = prompt("Жанр:", book.genre);
    const newYear = prompt("Год издания:", book.publishYear);
    const newInventoryNumber = prompt("Инвентарный номер:", book.inventoryNumber);
    const newDescription = prompt("Описание:", book.description || "");

    if (!newTitle || !newAuthor || !newGenre || !newYear || !newInventoryNumber) {
        return;
    }

    const updatedBook = {
        ...book,
        title: newTitle,
        author: newAuthor,
        genre: newGenre,
        publishYear: Number(newYear),
        inventoryNumber: newInventoryNumber,
        description: newDescription
    };

    await fetch(`${apiUrl}/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(updatedBook)
    });

    await loadBooks();
}

searchInput.addEventListener("input", renderBooks);
statusFilter.addEventListener("change", renderBooks);

newBookButton.addEventListener("click", () => {
    document.getElementById("title").focus();
});

loadBooks();