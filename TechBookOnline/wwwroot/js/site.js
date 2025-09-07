// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function toggleFavorite(btn) {
    btn.classList.toggle("active");
    const icon = btn.querySelector("i");
    if (btn.classList.contains("active")) {
        icon.classList.remove("bi-heart");
        icon.classList.add("bi-heart-fill");
    } else {
        icon.classList.remove("bi-heart-fill");
        icon.classList.add("bi-heart");
    }
}

section Scripts {
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            document.querySelectorAll(".toggle-like").forEach(function (btn) {
                btn.addEventListener("click", function () {
                    var bookId = this.getAttribute("data-bookid");
                    var icon = this.querySelector("i");

                    fetch("/UserLikes/ToggleLike", {
                        method: "POST",
                        headers: {
                            "Content-Type": "application/x-www-form-urlencoded"
                        },
                        body: "bookId=" + encodeURIComponent(bookId)
                    })
                        .then(res => res.json())
                        .then(data => {
                            if (data.success) {
                                if (icon.classList.contains("fa-heart-o")) {
                                    icon.classList.remove("fa-heart-o");
                                    icon.classList.add("fa-heart", "text-danger");
                                } else {
                                    icon.classList.remove("fa-heart", "text-danger");
                                    icon.classList.add("fa-heart-o");
                                }
                            } else {
                                alert(data.message || "เกิดข้อผิดพลาด");
                            }
                        })
                        .catch(err => console.error("Error:", err));
                });
            });
});
    </script>
}











