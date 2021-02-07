$(function () {
    $("#messageTable .FshiMesazheRekorde").click(function () {
        if (confirm("Jeni i sigurt qe deshironi te fshnini mesazhin tuaj ?")) {
            var element = $(this);
            var del_id = element.attr("id");
            var info = "id=" + del_id;
            $.ajax({
                type: "POST",
                url: "/FaqeKontakt/DeleteConfirmed",
                data: info,
                success: function (data) {
                    if (data) {
                        $('#Mesazhe' + del_id).fadeOut();
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