// /News/Details?handler=CreateComment

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
        }
        else {
            console.log(response.statusText);
        }
    
}
