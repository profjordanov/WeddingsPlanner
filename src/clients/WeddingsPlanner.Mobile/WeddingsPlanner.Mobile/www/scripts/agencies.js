//API
const baseAgenciesUrl = "http://localhost:5000/api/agencies";

function getAgencyByName() {
    const agencyName = $('#agency-name-input').val();
    const queryString = baseAgenciesUrl + "/by-name/" + agencyName;

    $.ajax({
        type: "GET",
        url: queryString,
        beforeSend: function (xhr) {
            if (localStorage.token) {
                xhr.setRequestHeader("Authorization", "Bearer " + localStorage.token);
            } else {
                alert("You are unauthorized to accomplish this action!");
                return false;
            }
        },
        success: function (results) {
            showAgenciesData(results);
        },
        error: function (jqXHR) {
            $('#error-msg').show();
            $('#error-msg').text("Error retrieving data. " + jqXHR.statusText);
        }
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
        url: baseAgenciesUrl,
        beforeSend: function (xhr) {
            if (localStorage.token) {
                xhr.setRequestHeader("Authorization", "Bearer " + localStorage.token);
            } else {
                alert("You are unauthorized to accomplish this action!");
                return false;
            }
        },
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(requestData),
        success: function () {
            alert("Successfully added!");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Status: " + textStatus);
            alert("Error: " + errorThrown);
        }
    });

    return true;
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