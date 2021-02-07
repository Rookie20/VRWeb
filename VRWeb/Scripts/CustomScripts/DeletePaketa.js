$(function () {
    $("#paketaTable .FshiPaketaRekorde").click(function () {
        if (confirm("Jeni i sigurt qe deshironi te fshnini paketen tuaj ?")) {
            var element = $(this);
            var del_id = element.attr("id");
            var info = "id=" + del_id;
            $.ajax({
                type: "POST",
                url: "/PaketaInfoes/DeleteConfirmed",
                data: info,
                success: function (data) {
                    if (data) {
                        $('#Paketa' + del_id).fadeOut();
                    }
                    else {
                        alert('Fshirja nuk mund te kryhet !');
                    }
                }
            });
        }
        return false;
    });
});