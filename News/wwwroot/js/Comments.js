async function mainCommentHandler(event) {

    event.preventDefault();

    let form = new FormData(document.forms.mainCommentForm);
    const response = await fetch("?handler=CreateComment", {
        method: "POST",
        headers: {
            "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val()
        },
        body:
            new URLSearchParams(form)
    });
    if (response) {
        if (response.status == 400) {
            showCommentValidationError();
            return;
        }
        const writeComment = await response.text();
        document.getElementById("mainCommentId").innerHTML += writeComment;
        updateCommentCount(1);
        $('#comments-area').val(''); // clear the text area

    }
    else {
        console.log(response.statusText);
    }
}

const okButtonContent = '<i class="fas fa-check"></i>';
const editButtonContent = '<i class="fas fa-edit"></i>';


async function editComment(event, commentId) {
    event.preventDefault();
    var currentContent = $(`#comment${commentId} p`);
    var inputField = $('<input>').attr({
        type: 'text',
        id: 'comment-input',
        class: 'form-control mb-2',
        value: currentContent.text()
    });
    currentContent.replaceWith(inputField);
    var editBtn = $(`#edit-comment-btn${commentId}`);
    editBtn
        .removeClass('btn-primary')
        .addClass('btn-success')
        .html(okButtonContent);
    editBtn
        .click(function (e) {
        e.preventDefault();
        var newContent = $('#comment-input').val();
        updateComment(commentId, newContent);
    });
}
async function updateComment(commentId, newContent) {
    let form = new FormData();
    form.append("id", commentId);
    form.append("content", newContent);

    const response = await fetch("?handler=UpdateComment", {
        method: "POST",
        headers: {
            "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val()
        },
        body:
            new URLSearchParams(form)
    });

    if (response.ok) {
        const updatedComment = await response.text();
        $(`#comment${commentId}`).replaceWith(updatedComment);
    }
    else
    if (response.status === 400) {
        showCommentValidationError();
    } else {
        console.log(response.statusText);
    }
}

async function showCommentValidationError() {
    //alert('Comment is forbidden!');
    var toast = $('<div class="toast" role="alert" aria-live="assertive" aria-atomic="true" data-autohide="true" data-delay="5000">')
        .addClass('bg-danger text-light')
        .append($('<div class="toast-body">').text('Comment is forbidden!'));

    $('#toast-container').append(toast);
    toast.toast('show');
}

async function deleteComment(event, id) {
    event.preventDefault();

    const response = await fetch("?handler=DeleteComment", {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json",
            "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val()
        },

        body: JSON.stringify(
            id
        )
    });

    if (response.ok) {
        console.log("Comment deleted!");
        document.querySelector(`#comment${id}`).remove();
        updateCommentCount(-1);
    } else {
        console.log("Error deleting comment.");
    }
}


const countLbl = document.querySelector("#comments-count");
const commentLbl = document.querySelector("#comments-label");

async function commentCount(count) {
    countLbl.innerHTML = count;
    commentLbl.innerHTML = count === 1 ? "comment" : "comments";
}
async function updateCommentCount(count) {
    commentCount(+countLbl.innerHTML + count);
}