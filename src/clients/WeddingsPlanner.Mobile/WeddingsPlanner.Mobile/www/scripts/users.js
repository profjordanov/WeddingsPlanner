//API
const baseUsersUrl = "http://localhost:5000/api/users/";

function loadLoginPage() {
    const userLoginPage = $("#users-login-page");
    $.mobile.pageContainer.pagecontainer("change", userLoginPage, {});
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
            $("#user-email").text("");
            $("#user-password").text("");
            alert("Got a token from the server! Token: " + data.tokenString);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Status: " + textStatus);
            alert("Error: " + errorThrown);
        }
    });
}