
$(document).ready(function () {
    let _isLogged = sessionStorage.getItem("isLogged");
    sessionStorage.setItem("isLogged", "");

    if (_isLogged == "success") {
        var redirect = appPath + "/Home/Index";
        window.location.replace(redirect);
    }
    if (_isLogged == "no-match") {
        $('#loginError').show();
    }
});


function Login(event) {
    //validate
    var isvalid = true;
    $('#loginError').hide();

    var _email = document.getElementById("email").value;
    //var _pass = document.getElementById("password").value;

    // validate
    if (_email == '') {
        var fn = document.getElementById("email");
        fn.classList.add("is-invalid");
        isvalid = false;
    }
    else {
        var fn = document.getElementById("email");
        // validate regex
        var validRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

        if (!(_email.match(validRegex))) {
            isvalid = false;
            fn.classList.add("is-invalid");
        }
        else {
            fn.classList.remove("is-invalid");
        }
    };


    if (isvalid) {        
        $.ajax({
            type: "GET",
            datatype: "json",
            url: appPath + "/Accounts/PostLogin",
            data: {
                username: _email
            },
            async: false,
            contentType: "application/json; charset=utf-8",                       
            success: function (response) {                
                if (response.error == true) {
                    sessionStorage.setItem("isLogged", "no-match");
                }
                else {
                    sessionStorage.setItem("isLogged", "success");
                }    
            },
            error: function (data) {                
            }
        });
    };

};


function ValidateLength(el) {
    if (el.value.length > 2) {
        el.classList.remove("is-invalid");
    }
    else {
        el.classList.add("is-invalid")
    }
};
