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
            const writeComment = await response.text();
            document.getElementById("mainCommentId").innerHTML += writeComment;
            updateCommentCount(1);
        }
        else {
            console.log(response.statusText);
        }
}

async function deleteComment(event, id) {
    event.preventDefault();
    const comment = document.querySelector(`#comment${id}`);

    const response = await fetch("?handler=DeleteComment", {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json",
            "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val()
        },

        body:JSON.stringify(
            id
        )
    });

    if (response.ok) {
        console.log("Comment deleted!");
        comment.style.display = 'none';
        updateCommentCount(-1);
    } else {
        console.log("Error deleting comment.");
    }
}


const countLbl = document.querySelector("#comments-count");
const commentLbl = document.querySelector("#comments-label");

async function commentCount(count) {
    countLbl.innerHTML = count;
    commentLbl.innerHTML = count === 1? "comment":"comments";
}
async function updateCommentCount(count) {
    commentCount(+countLbl.innerHTML + count);
}