
$(document).ready(function () {
    $('table tbody tr  td').on('click', function () {
        var guid = $(this).closest('tr').attr("data-id");        
        $.ajax({
            type: "GET",
            datatype: "json",
            url: appPath + "/User/GetUserDetails",
            data: {
                GUID: guid
            },
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                $("#UserRequestDetails").html(response);
                $("#myModal").modal("show");
            },
            error: function (data) {
            }
        });
    });
});


function sortTable(n) {
  var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
    table = document.getElementById("AccessRequestTable");
    switching = true;
    // Set the sorting direction to ascending:
    dir = "asc";
    /* Make a loop that will continue until
    no switching has been done: */
    while (switching) {
        // Start by saying: no switching is done:
        switching = false;
    rows = table.rows;
    /* Loop through all table rows (except the
    first, which contains table headers): */
    for (i = 1; i < (rows.length - 1); i++) {
        // Start by saying there should be no switching:
        shouldSwitch = false;
    /* Get the two elements you want to compare,
    one from current row and one from the next: */
    x = rows[i].getElementsByTagName("TD")[n];
    y = rows[i + 1].getElementsByTagName("TD")[n];
    /* Check if the two rows should switch place,
    based on the direction, asc or desc: */
    if (dir == "asc") {
        if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
        // If so, mark as a switch and break the loop:
        shouldSwitch = true;
    break;
        }
      } else if (dir == "desc") {
        if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
        // If so, mark as a switch and break the loop:
        shouldSwitch = true;
    break;
        }
      }
    }
    if (shouldSwitch) {
        /* If a switch has been marked, make the switch
        and mark that a switch has been done: */
        rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
    switching = true;
    // Each time a switch is done, increase this count by 1:
    switchcount ++;
    } else {
      /* If no switching has been done AND the direction is "asc",
    set the direction to "desc" and run the while loop again. */
      if (switchcount == 0 && dir == "asc") {
        dir = "desc";
    switching = true;
      }
    }
  }
};

function SaveUserDetails() {    
    var userForm = document.forms["frmUserDetails"];
    var FormObject = {
        guid: userForm.mdlGUID.value,
        firstName: userForm.mdlFirstName.value,
        lastName: userForm.mdlLastName.value,
        facilityID: userForm.mdlFacilityDDL.value,
        roleID: userForm.mdlRoleDDL.value,
        statusID: userForm.mdlStatusDDL.value,
        notes: userForm.mdlInternalNote.value,
        emailAddress: userForm.mdlEmailAddress.value,
        sendEmail: userForm.mdlEmailNotification.checked,
    };

    $.ajax({
        type: "GET",
        datatype: "json",
        url: appPath + "/User/PostUserDetails",
        data: {
            obj: JSON.stringify(FormObject)
        },
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response.error == true) {
            //    sessionStorage.setItem("isLogged", "no-match");
                $("#ToastBody").html(response.message);
            }
            else {
            //    sessionStorage.setItem("isLogged", "success");
            }
        },
        error: function (data) {
        }
    });

    $("#myModal").modal("hide");
    $("#UserUpdateToast").show();
};

function CloseToast() {
    $("#UserUpdateToast").hide();
};