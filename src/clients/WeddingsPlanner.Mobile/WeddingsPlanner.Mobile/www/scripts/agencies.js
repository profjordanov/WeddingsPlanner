//API
const baseUrl = "http://localhost:5000/api/agencies";

function getAllAgencies() {
    const agencyName = $('#agency-name-input').val();
    const queryString = baseUrl+ "/by-name/" + agencyName;
    $.getJSON(queryString, function (results) {
        showAgenciesData(results);
    }).fail(function (jqXHR) {
        $('#error-msg').show();
        $('#error-msg').text("Error retrieving data. " + jqXHR.statusText);
    });
    return false;
}

function addNewAgency() {
    const agencyName = $("#add-agency-name").val();
    const agencyEmployeesCount = $("#add-agency-employees-count").val();
    const agencyTown = $("#add-agency-town").val();

    const requestData = {
        name: agencyName,
        employeesCount: agencyEmployeesCount,
        town: agencyTown
    };

    $.ajax({
        type: "POST",
		url: baseUrl,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(requestData),
        success: function() {
            alert("Successfully added!");
        },
        error:function(XMLHttpRequest, textStatus, errorThrown) {
            alert("Status: " + textStatus);
            alert("Error: " + errorThrown);
        }  
    });
}

function loadAgenciesAddPage() {
    const addAgenciesPage = $("#agencies-add-page");
    $.mobile.pageContainer.pagecontainer("change", addAgenciesPage, {});
}

function showAgenciesData(agency) {
    $("#error-msg").hide();
    $("#agencies-data").show();

    $("#name").text(agency.name);
    $("#employees-count").text(agency.employeesCount);
    $("#town").text(agency.town);
}