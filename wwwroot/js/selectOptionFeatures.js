//method to clear selected options
function clearSelectOptions() {
    $("#ddlEmployees").prop('selectedIndex', -1);
    $("#ddlRoleEmployees").prop('selectedIndex', -1);
}


//method to return employee to add for selected role
function addEmployeeForRole() {
    var selected = "";
    $('#ddlEmployees :selected').each(function () {
        if (selected == "")
            selected = $(this).val();
        else
            selected = selected + "," + $(this).val();
    });
    return selected;
}


//method to return employee to remove for selected role
function removeEmployeeFormRole() {
    var selected = "";
    $('#ddlRoleEmployees :selected').each(function () {
        if (selected == "")
            selected = $(this).val();
        else
            selected = selected + "," + $(this).val();
    });
    return selected;
}
    