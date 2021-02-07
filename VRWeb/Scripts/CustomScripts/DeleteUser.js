//$(function () {
//    $("#userTable .FshiUserRekorde").click(function () {
//        if (confirm("Jeni i sigurt qe deshironi te fshnini mesazhin tuaj ?")) {
//            var element = $(this);
//            var del_id = element.attr("id");
//            var info = "id=" + del_id;
//            $.ajax({
//                type: "POST",
//                url: "/Administrimi/DeleteConfirmed",
//                data: info,
//                success: function (data) {
//                    if (data) {
//                        $('#User' + del_id).fadeOut();
//                    }
//                    else {
//                        alert('Fshirja nuk mund te kryhet !');
//                    }
//                }
//            });
//        }
//        return false;
//    });
//});