function SaveContactProfile() {    
    var isValid = true;

    var _id = $('#hiddenPhysicianID').val();
    var _firstname = $('#card_firstname').val();
    var _lastname = $('#card_lastname').val();
    var _npi = $('#card_npi').val();
    var _mailingaddress = $('#card_mailingaddress').val();
    var _primaryemail = $('#card_primaryemail').val();
    var _secondaryemail = $('#card_secondaryemail').val();

    if (_firstname == '') {
        var fn = document.getElementById("card_firstname");
        fn.classList.remove("is-invalid");
        fn.classList.add("is-invalid");
        isValid = false;
    }
    else {
        var fn = document.getElementById("card_firstname");
        fn.classList.remove("is-invalid");
    }

    if (_lastname == '') {
        var fn = document.getElementById("card_lastname");
        fn.classList.remove("is-invalid");
        fn.classList.add("is-invalid");
        isValid = false;
    }
    else {
        var fn = document.getElementById("card_lastname");
        fn.classList.remove("is-invalid");
    }

    if (_mailingaddress == '') {
        var fn = document.getElementById("card_mailingaddress");
        fn.classList.remove("is-invalid");
        fn.classList.add("is-invalid");
        isValid = false;
    }
    else {
        var fn = document.getElementById("card_mailingaddress");
        fn.classList.remove("is-invalid");
    }

    if (_primaryemail == '') {
        var fn = document.getElementById("card_primaryemail");
        fn.classList.remove("is-invalid");
        fn.classList.add("is-invalid");
        isValid = false;
    }
    else {
        var fn = document.getElementById("card_primaryemail");
        fn.classList.remove("is-invalid");
    }

    if (isValid) {

        var _object = { ID: _id, PhysicianFirstName: _firstname, PhysicianLastName: _lastname, NPI: _npi, BillingAddress: _mailingaddress, PrimaryEmail: _primaryemail, SecondaryEmail: _secondaryemail };
        var _jsonObj = JSON.stringify(_object);

        $.ajax({
            type: "GET",
            datatype: "json",
            url: appPath + "/Admin/PostContactCard",
            async: false,
            data: { PhysicianCard: _jsonObj, },
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.error == true) {
                    //nothing to update
                    $("#ToastBody").html(data.message);
                    $("#PhysicianUpdateToast").show();
                }
                else { //success
                    $("#ToastBody").html(data.message);
                    $("#PhysicianUpdateToast").show();
                    $("#contactCardAlertMessage").show();
                    $("#contactCardFieldset").prop('disabled', true);
                    $("#contactCardBtnFieldset").prop('disabled', true);
                }                
            }
        });

    }


}

function SavePhysicianProfile() {    
    var isValid = true;
    var _credentials = _specialty = _subSpecialty = _healthSystem = _hospitalAffiliations = _facilityType = _education = _residency = _fellowship = _boardCertifications = _biography = "";

    var _id = $('#hiddenPhysicianID').val();
    //var _primaryEmail = $('#card_PrimaryEmail').val();
    _credentials = DelimitedString(document.querySelectorAll('#ms-list-1 li.selected'));
    _specialty = DelimitedString(document.querySelectorAll('#ms-list-2 li.selected'));
    _subSpecialty = DelimitedString(document.querySelectorAll('#ms-list-3 li.selected'));
    _healthSystem = DelimitedString(document.querySelectorAll('#ms-list-4 li.selected'));
    _hospitalAffiliations = DelimitedString(document.querySelectorAll('#ms-list-5 li.selected'));
    _facilityType = DelimitedString(document.querySelectorAll('#ms-list-6 li.selected'));
    _education = DelimitedString(document.querySelectorAll('#ms-list-7 li.selected'));
    _residency = DelimitedString(document.querySelectorAll('#ms-list-8 li.selected'));
    _fellowships = DelimitedString(document.querySelectorAll('#ms-list-9 li.selected'));
    _boardCertifications = DelimitedString(document.querySelectorAll('#ms-list-10 li.selected'));
    _biography = $('#frmBiography').val().trim();

    // others
    if (IncludesOther(_hospitalAffiliations)) {
        if ($('#HospitalAffiliationsOther').val() == "") {
            var fn = document.getElementById("HospitalAffiliationsOther");
            fn.classList.remove("is-invalid");
            fn.classList.add("is-invalid");
            isValid = false;
        }
        else {
            var fn = document.getElementById("HospitalAffiliationsOther");
            fn.classList.remove("is-invalid");
            _hospitalAffiliations = padDropdownResults(_hospitalAffiliations, $('#HospitalAffiliationsOther').val());
        }
    }
    if (IncludesOther(_education)) {
        if ($('#MedicalSchoolOther').val() == "") {
            var fn = document.getElementById("MedicalSchoolOther");
            fn.classList.remove("is-invalid");
            fn.classList.add("is-invalid");
            isValid = false;
        }
        else {
            var fn = document.getElementById("MedicalSchoolOther");
            fn.classList.remove("is-invalid");
            _education = padDropdownResults(_education, $('#MedicalSchoolOther').val());
        }
    }
    if (IncludesOther(_residency)) {
        if ($('#ResidencyOther').val() == "") {
            var fn = document.getElementById("ResidencyOther");
            fn.classList.remove("is-invalid");
            fn.classList.add("is-invalid");
            isValid = false;
        }
        else {
            var fn = document.getElementById("ResidencyOther");
            fn.classList.remove("is-invalid");
            _residency = padDropdownResults(_residency, $('#ResidencyOther').val());
        }
    }
    if (IncludesOther(_fellowships)) {
        if ($('#FellowshipOther').val() == "") {
            var fn = document.getElementById("FellowshipOther");
            fn.classList.remove("is-invalid");
            fn.classList.add("is-invalid");
            isValid = false;
        }
        else {
            var fn = document.getElementById("FellowshipOther");
            fn.classList.remove("is-invalid");
            _fellowships = padDropdownResults(_fellowships, $('#FellowshipOther').val());
        }
    }
    if (IncludesOther(_boardCertifications)) {
        if ($('#BoardCertificationOther').val() == "") {
            var fn = document.getElementById("BoardCertificationOther");
            fn.classList.remove("is-invalid");
            fn.classList.add("is-invalid");
            isValid = false;
        }
        else {
            var fn = document.getElementById("BoardCertificationOther");
            fn.classList.remove("is-invalid");
            _boardCertifications = padDropdownResults(_boardCertifications, $('#BoardCertificationOther').val());
        }
    }

    if (isValid) {        
        var _object = {
            ID: _id,            
            Credentials: _credentials,
            Specialty: _specialty,
            SubSpecialty: _subSpecialty,
            IDN: _healthSystem,
            HospitalAffiliations: _hospitalAffiliations,
            FacilityType: _facilityType,
            MedicalSchool: _education,
            Residency: _residency,
            Fellowships: _fellowships,
            BoardCertifications: _boardCertifications,
            Biography: _biography
        };
        var _jsonObj = JSON.stringify(_object);

        $.ajax({
            type: "GET",
            datatype: "json",
            url: appPath + "/Admin/PostPhysicianCard",
            async: false,
            data: { PhysicianCard: _jsonObj, },
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.error == true) {
                    //nothing to update
                    $("#ToastBody").html(data.message);
                    $("#PhysicianUpdateToast").show();                    
                }
                else { //success
                    $("#ToastBody").html(data.message);
                    $("#PhysicianUpdateToast").show();
                    $("#profileCardAlertMessage").show();
                    $("#profileCardFieldset").prop('disabled', true);
                    $("#profileCardBtnFieldset").prop('disabled', true);
                }                
            }
        });
    }
}

function ValidateLength(el) {
    if (el.value.length > 2) {
        el.classList.remove("is-invalid");
    }
    else {
        el.classList.add("is-invalid")
    }
};

function DelimitedString(SelectedList) {
    var rtnString = "";

    if (SelectedList.length > 0) {
        SelectedList.forEach(function (item) {
            rtnString += item.textContent + "|";
        })
        rtnString = (rtnString.length > 0) ? rtnString.slice(0, -1) : "";        
    }
    
    return rtnString;
}

function CloseToast() {
    $("#PhysicianUpdateToast").hide();
};


function DropdownChange(inputField, dropDown) {
    var Selector = DelimitedString(document.querySelectorAll('#' + dropDown + ' li.selected')).split("|");
    
    if (IncludesOther(Selector)) {
        $('#' + inputField).show();
        $('#' + dropDown).removeClass("ms-active");
    }
    else {
        $('#' + inputField).val("");
        $('#' + inputField).hide();
    }
}

function IncludesOther(Selector) {
    if (Selector.includes('Other')) {
        return true;
    }
    else {
        return false;
    }
}

function padDropdownResults(demList, value) {
    var delimitedArray = demList.split("|");

    if (delimitedArray.length > 1) {
        demList = demList.replace("|Other", "|Other = " + value);
    }
    else {
        demList = demList.replace("Other", "Other = " + value);
    }
    
    return demList;
}