function getAgencyPicture() {
    navigator.camera.getPicture(succeededCameraCallback, failedCameraCallback, {
        quality: 25,
        destinationType: Camera.DestinationType.DATA_URL
    });
}

function succeededCameraCallback(imageData) {
    $("#base64-agency-pic").val(imageData);
    alert($("#base64-agency-pic").val());
}

function failedCameraCallback(message) {
    alert(message);
}