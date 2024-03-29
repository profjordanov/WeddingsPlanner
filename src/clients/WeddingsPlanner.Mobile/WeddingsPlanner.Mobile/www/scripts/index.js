﻿(function () {
    "use strict";

    document.addEventListener( 'deviceready', onDeviceReady.bind( this ), false );

    function onDeviceReady() {
        // Handle the Cordova pause and resume events
        document.addEventListener( 'pause', onPause.bind( this ), false );
        document.addEventListener('resume', onResume.bind(this), false);

        $("#get-agencies-btn").click(getAgencyByName);
        $("#add-new-agency-btn").click(loadAgenciesAddPage);
        $("#add-agency-btn").click(addNewAgency);
        $("#login-btn").click(login);
        $("#goto-register-btn").click(loadRegisterPage);
        $("#register-btn").click(register);
        $("#logout-btn").click(logout);
        $("#delete-current-agency-btn").click(confirmCurrentAgencyDelete);
        // Camera Plugin
        $("#agency-pic-btn").click(getAgencyPicture);

    };

    function onPause() {
        // TODO: This application has been suspended. Save application state here.
    };

    function onResume() {
        // TODO: This application has been reactivated. Restore application state here.
    };
} )();