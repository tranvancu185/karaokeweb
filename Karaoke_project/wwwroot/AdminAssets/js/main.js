
$(document).ready(function () {
    $(function () {
        $('#profile-image1').on('click', function () {
            $('#profile-image-upload').click();
        });
    });

    $('#myModal').on('shown.bs.modal', function () {
        $('#myInput').trigger('focus')
    })
    
    
});

// Function Show pwd
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

// Function validate Phone VietNam
function validatePhone(mobile) {
    var vnf_regex = /((09|03|07|08|05)+([0-9]{8})\b)/g;
    var error = "";
    if (mobile !== '') {
        if (vnf_regex.test(mobile) == false) {
            error = "Số điện thoại của bạn không đúng định dạng!";
        } else {
            error = "";
        }
    } else {
        error = "Bạn chưa điền số điện thoại!";
    }
    return error;
}

//function convert Date to yyyy-mm-dd
function convert(str) {
    var date = new Date(str),
        mnth = ("0" + (date.getMonth() + 1)).slice(-2),
        day = ("0" + date.getDate()).slice(-2);
    return [date.getFullYear(), mnth, day].join("-");
}


function convertVND(price) {
    return price.toLocaleString('it-IT', { style: 'currency', currency: 'VND' });
}

// function Calculate Time Difference
function diff(start, end) {
    start = start.split(":");
    end = end.split(":");
    var startDate = new Date(0, 0, 0, start[0], start[1], 0);
    var endDate = new Date(0, 0, 0, end[0], end[1], 0);
    var diff = endDate.getTime() - startDate.getTime();
    var hours = Math.floor(diff / 1000 / 60 / 60);
    diff -= hours * 1000 * 60 * 60;
    var minutes = Math.floor(diff / 1000 / 60);

    // If using time pickers with 24 hours format, add the below line get exact hours
    if (hours < 0)
        hours = hours + 24;
    return (hours <= 9 ? "0" : "") + hours + ":" + (minutes <= 9 ? "0" : "") + minutes;
}



