function SaveFilter(_filter, el) {    
    var _value = el.innerText;

    // highlight filter selection
    var elem_id = "." + _filter.toLowerCase();    
    $(el).addClass('active');

    GetFilterData(_filter, _value, "save");

    // highlight sort selection     
    if (_filter == 'sorting') {
        var selection = el.parentNode.innerText;
        var elementsort = "sort-menu-list-" + _value;
        document.getElementById("btnLibSort").innerText = selection;
        $("#" + elementsort).removeClass('active');
    }
}

function RemoveFilter(_filter, el)
{
    // remove pill
    var value = el.id;
    var elem_id = "#" + _filter.toLowerCase() + "_" + value.replaceAll(' ', '').toLowerCase();
    $(elem_id).removeClass('active');

    GetFilterData(_filter, value, "remove");

    //remove search text if "search"
    if (_filter == 'search') {
        document.getElementById("pa_global_srch_term").value = '';
    }
}

function GetFilterData(_filter, _value, _action) {
    $.ajax({
        url: appPath + "/Physicians/PhysicianResults",
        type: "GET",
        contentType: "application/json; charset=utf-8",       
        data: { filter: _filter, value: _value, actions: _action },
        datatype: "json",        
        async: true,
        success: function (data) {
            if (data.success != undefined && data.success == false) {
                $('#physicianresults-error').html("");
                $('#physicianresults-error').html(data.message);
            }
            else {
                $('#physicianresults-section').html("");
                $('#physicianresults-section').html(data);
            }
        },
        error: function (data) {
            $('#physicianresults-error').html("");
            $('#physicianresults-error').html(data);
        }
    });
}


function ShowPhysician(_id) {
    $.ajax({
        type: "GET",
        datatype: "json",
        url: appPath + "/Physicians/GetPhysiciansDetails",
        data: {
            ID: _id
        },        
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $("#PhysicianDetails").html(response);
            $("#physicianModal").modal("show");
        },
        error: function (data) {
        }
    });
}

function PhysicianSearch() {
    var _value = document.getElementById("pa_global_srch_term").value;

    if (_value != '') {
        GetFilterData("search", _value, "save")
    }
}