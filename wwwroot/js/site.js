const commentForm = document.querySelector("[name='commentForm']");
const addCommentBtn = document.querySelector("[name='addCommentBtn']");
const replyBtns = document.querySelectorAll("[name='replyBtn']");
const commentId = document.querySelector(".modal-body .form-group #CommentId");

replyBtns.forEach(button =>
    button.addEventListener("click", () => {
        commentForm.action = "/Home/SaveSubComment";
        commentId.value = button.dataset.commentid;
        button.dataset.toggle = "modal";
        button.dataset.target = "#commentModal";
        $('#addComment').modal('show');
    }));

addCommentBtn.addEventListener("click", () => {
    commentForm.action = "/Home/SaveComment";
    commentId.value = 0;
});