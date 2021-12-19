
function showPwd() {
    var x = document.getElementById("pwdInput");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}

function previewFile() {
    var preview = document.getElementById('profile-image1');
    var file = document.querySelector('input[type=file]').files[0];
    var reader = new FileReader();

    reader.addEventListener("load", function () {
        console.log(file);
        preview.src = reader.result;

    }, false);

    if (file) {
        reader.readAsDataURL(file);
    }
}

$(function () {
    $('#profile-image1').on('click', function () {
        $('#profile-image-upload').click();
    });
});