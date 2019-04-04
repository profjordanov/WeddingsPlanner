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
    const agencyImage = $("#base64-agency-pic").val();

    const requestData = {
        name: agencyName,
        employeesCount: agencyEmployeesCount,
        town: agencyTown,
        base64Image: agencyImage
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
            loadAgenciesMainPage();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Status: " + textStatus);
            alert("Error: " + errorThrown);
        }
    });

    return true;
}

function confirmCurrentAgencyDelete() {
    const isConfirmed = confirm("Do you want to delete this agency?");
    if (isConfirmed) {
        deleteCurrentAgency();
    } else {
        return false;
    }
}

function deleteCurrentAgency() {
    const agencyId = $("#current-agency-id").text();
    const agencyName = $("#name").text();
    const agencyEmployeesCount = $("#employees-count").text();
    const agencyTown = $("#town").text();

    const requestData = {
        id: agencyId,
        name: agencyName,
        employeesCount: agencyEmployeesCount,
        town: agencyTown
    };
    console.log(requestData);

    $.ajax({
        type: "DELETE",
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
            alert(agencyName + " successfully deleted!");
            $("#agencies-data").hide();
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

function loadAgenciesMainPage() {
    const agenciesPage = $("#agencies-page");
    $.mobile.pageContainer.pagecontainer("change", agenciesPage, {});
}

function showAgenciesData(agency) {
    $("#error-msg").hide();
    $("#agencies-data").show();

    $("#current-agency-id").text(agency.id);
    $("#name").text(agency.name);
    $("#employees-count").text(agency.employeesCount);
    $("#town").text(agency.town);
    $("#agency-img").attr("src", `data:image/jpeg;base64,${agency.base64Image}`);
}