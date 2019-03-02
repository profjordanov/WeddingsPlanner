$("form#data").submit(function(e) {
  e.preventDefault();
  let formData = new FormData(this);
  let fileInput = $('#file')[0];
  let file = fileInput.files[0];
  formData.append("File", file);

  $.ajax({
    url: "http://localhost:5000/api/Onboarding/json-agencies",
    type: 'POST',
    data: formData,
    xhrFields: {
      responseType: 'blob'
    },
    success: function(data) {
      let a = document.createElement('a');
      let url = window.URL.createObjectURL(data);
      a.href = url;
      a.download = 'agencies-onboarding-' + Date.now() + '.csv';
      a.click();
      window.URL.revokeObjectURL(url);
    },
    cache: false,
    contentType: false,
    processData: false
  });
});