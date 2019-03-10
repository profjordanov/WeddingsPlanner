function getAllAgencies() {
    const queryString = "http://localhost:5000/api/Agencies/all";
    $.getJSON(queryString, function (results) {
        showAgenciesData(results);
    }).fail(function (jqXHR) {
        $('#error-msg').show();
        $('#error-msg').text("Error retrieving data. " + jqXHR.statusText);
    });
    return false;
}

function showAgenciesData(agencies) {
    $("#error-msg").hide();
    $("#agencies-data").show();

    $("#name").text(agencies[0].name);
    $("#employees-count").text(agencies[0].employeesCount);
    $("#town").text(agencies[0].town);
}