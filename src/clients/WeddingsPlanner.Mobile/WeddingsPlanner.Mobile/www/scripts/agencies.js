function getAllAgencies() {
    const agencyName = $('#agency-name-input').val();
    const queryString = "http://localhost:5000/api/agencies/by-name/" + agencyName;
    $.getJSON(queryString, function (results) {
        showAgenciesData(results);
    }).fail(function (jqXHR) {
        $('#error-msg').show();
        $('#error-msg').text("Error retrieving data. " + jqXHR.statusText);
    });
    return false;
}

function showAgenciesData(agency) {
    $("#error-msg").hide();
    $("#agencies-data").show();

    $("#name").text(agency.name);
    $("#employees-count").text(agency.employeesCount);
    $("#town").text(agency.town);
}