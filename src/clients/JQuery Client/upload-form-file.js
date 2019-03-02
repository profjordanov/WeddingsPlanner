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
    success: function (data) {
      alert(data)
    },
    cache: false,
    contentType: false,
    processData: false
  });
});