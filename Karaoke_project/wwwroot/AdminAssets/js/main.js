
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
$('#myTable').DataTable({
    "scrollY": "50vh",
    "scrollCollapse": true,
});
$(function () {
    $('#profile-image1').on('click', function () {
        $('#profile-image-upload').click();
    });
});

$('#myModal').on('shown.bs.modal', function () {
    $('#myInput').trigger('focus')
})

$(document).ready(function () {
    $("#form-validation").validate({
        ignore: ':hidden:not(:checkbox)',
        errorElement: 'label',
        errorClass: 'is-invalid',
        validClass: 'is-valid',
        rules: {
            inputRequired: {
                required: true
            },
            inputMinLength: {
                required: true,
                minlength: 6
            },
            inputMaxLength: {
                required: true,
                minlength: 8
            },
            inputUrl: {
                required: true,
                url: true
            },
            inputRangeLength: {
                required: true,
                rangelength: [2, 6]
            },
            inputMinValue: {
                required: true,
                min: 8
            },
            inputMaxValue: {
                required: true,
                max: 6
            },
            inputRangeValue: {
                required: true,
                max: 6,
                range: [6, 12]
            },
            inputEmail: {
                required: true,
                email: true
            },
            inputPassword: {
                required: true
            },
            inputPasswordConfirm: {
                required: true,
                equalTo: '#password'
            },
            inputDigit: {
                required: true,
                digits: true
            },
            inputValidCheckbox: {
                required: true
            }
        }
    });
});

