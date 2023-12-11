function SaveFilter(_filter, el) {    
    var _value = el.innerText;

    // highlight filter selection
    var elem_id = "." + _filter.toLowerCase();     
    $(el).addClass('active');

    // highlight sort selection     
    if (_filter == 'search') {        
        _value = document.getElementById("GlobalSearch").value;
    }

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
        document.getElementById("lb_global_srch_term").value = '';
    }
}

function GetFilterData(_filter, _value, _action) {
    $.ajax({
        url: appPath + "/Library/LibraryResults",
        type: "GET",
        contentType: "application/json",
        data: { filter: _filter, value: _value, actions: _action },
        datatype: "json",
        success: function (data) {

            
            $('#libraryresults-section').html("");
            $('#libraryresults-section').html(data);
        }
    });
}

function LibrarySearch() {
    var _value = document.getElementById("lb_global_srch_term").value;
    if (_value != '') {
        GetFilterData("search", _value, "save")
    }
}