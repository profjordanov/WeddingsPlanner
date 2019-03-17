//API
const baseUsersUrl = "http://localhost:5000/api/users/";

function loadRegisterPage() {
    const userRegisterPage = $("#users-register-page");
    $.mobile.pageContainer.pagecontainer("change", userRegisterPage, {});
}

function login() {
    const email = $("#user-email").val();
    const password = $("#user-password").val();

    const requestData = {
        email: email,
        password: password
    };

    $.ajax({
        type: "POST",
        url: baseUsersUrl + "login",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(requestData),
        success: function (data) {
            localStorage.token = data.tokenString;
            alert("Got a token from the server! Token: " + data.tokenString);
            loadAgenciesPage();
        },
        error: function (response) {
            parseResponseErrors(response.responseText);
        }
    });
}

function register() {
    const firstName = $("#add-user-firstName").val();
    const lastName = $("#add-user-lastName").val();
    const email = $("#add-user-email").val();
    const password = $("#add-user-password").val();

    const requestData = {
        firstName: firstName,
        lastName: lastName,
        email: email,
        password: password
    };

    $.ajax({
        type: "POST",
        url: baseUsersUrl + "register",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(requestData),
        success: function (data) {
            $("#user-email").val(data.email);
            loadLoginPage();
        },
        error: function (response) {
            parseResponseErrors(response.responseText);
        }
    });
}

function logout() {
    localStorage.clear();
    loadLoginPage();
}

function loadAgenciesPage() {
    const agenciesPage = $("#agencies-page");
    $.mobile.pageContainer.pagecontainer("change", agenciesPage, {});
}

function loadLoginPage() {
    const userLoginPage = $("#users-login-page");
    $.mobile.pageContainer.pagecontainer("change", userLoginPage, {});
}