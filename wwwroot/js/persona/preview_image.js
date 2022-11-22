$(document).ready(() => {
  $('#perfil').change(function () {
    const file = this.files[0];
    console.log(file);
    if (file) {
      let reader = new FileReader();
      reader.onload = function (event) {
        console.log(event.target.result);
        $('#imgPreview').attr('src', event.target.result);
      }
      reader.readAsDataURL(file);
    }
  });
});

$("#upload_perfil").on("click", function () {
  console.log(' evento desplegado')
  $("#perfil").trigger("click");
});
