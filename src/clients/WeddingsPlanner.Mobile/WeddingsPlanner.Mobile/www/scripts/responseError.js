function parseResponseErrors(responseText) {
    const errorObj = JSON.parse(responseText);
    const errors = errorObj.messages.join();
    console.log(errors);
    alert(errors);
}