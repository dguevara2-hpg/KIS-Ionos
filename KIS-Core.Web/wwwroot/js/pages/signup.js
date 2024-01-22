$(document).ready(function () {
    let _isRegistered = sessionStorage.getItem("isRegistered");
    let _isRegisteredMessage = sessionStorage.getItem("isRegisteredMessage");
    sessionStorage.setItem("isRegistered", "");
    sessionStorage.setItem("isRegisteredMessage", "");

    if (_isRegistered == "success") {
        $('#registerSuccess').show();
        $('#registerError').hide();
    }
    if (_isRegistered == "error") {
        $('#registerError').show();
        document.getElementById("registerError").innerHTML = _isRegisteredMessage;
        $('#registerSuccess').hide();
    }
});


function Register(event) {    
    var isvalid = true;
    var _fname = document.getElementById("reg_firstname").value;
    var _lname = document.getElementById("reg_lastname").value;
    var _facilityidn = $("#reg_facilityidn option:selected").text();
    var _jobtitle = document.getElementById("reg_jobtitle").value;
    var _email = document.getElementById("reg_email").value;
    var _accept = $('#cbkAccept').is(":checked");

    // validate
    if (_fname == '') {
        var fn = document.getElementById("reg_firstname");
        fn.classList.remove("is-invalid");
        fn.classList.add("is-invalid");
        isvalid = false;
    }
    else {
        var fn = document.getElementById("reg_firstname");
        fn.classList.remove("is-invalid");
    }
    if (_lname == '') {
        var fl = document.getElementById("reg_lastname");
        fl.classList.remove("is-invalid");
        fl.classList.add("is-invalid");
        isvalid = false;
    }
    else {
        var fl = document.getElementById("reg_lastname");
        fl.classList.remove("is-invalid");
    }
    if (_facilityidn == "Your Facility/IDN") {
        var fi = document.getElementById("reg_facilityidn");
        fi.classList.remove("is-invalid");
        fi.classList.add("is-invalid");
        isvalid = false;
    }
    else {
        var fi = document.getElementById("reg_facilityidn");
        fi.classList.remove("is-invalid");
    }
    if (_jobtitle == '') {
        var jt = document.getElementById("reg_jobtitle");
        jt.classList.remove("is-invalid");
        jt.classList.add("is-invalid");
        isvalid = false;
    }
    else {
        var jt = document.getElementById("reg_jobtitle");
        jt.classList.remove("is-invalid");
    }
    if (_email == '') {
        var em = document.getElementById("reg_email");
        em.classList.remove("is-invalid");
        em.classList.add("is-invalid");
        isvalid = false;
    }
    else {
        var em = document.getElementById("reg_email");
        em.classList.remove("is-invalid");
    }
    if (_accept == false) {
        var ac = document.getElementById("cbkAccept");
        ac.classList.remove("is-invalid");
        ac.classList.add("is-invalid");
        isvalid = false;
    }
    else {
        var ac = document.getElementById("cbkAccept");
        ac.classList.remove("is-invalid");
    }

    if (isvalid) {

        var _object = { firstName: _fname, lastName: _lname, facilityIDN: _facilityidn, jobTitle: _jobtitle, emailAddress: _email };
        var _jsonObj = JSON.stringify(_object);

       

        $.ajax({
            type: "GET",
            datatype: "json",
            url: appPath + "/Accounts/PostSignup",            
            async: false,
            data: { loginModel: _jsonObj, },
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.error == true) {
                    sessionStorage.setItem("isRegistered", "error");
                }
                else {
                    sessionStorage.setItem("isRegistered", "success");
                }
                sessionStorage.setItem("isRegisteredMessage", data.message);
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
