$(document).ready(function () {


    $("#loaddata").click(function () {


        $.ajax({

            url: '/dashboard/get_uet_users',
            type: 'GET',
            success: function (result) {


                $.each(result, function (index, user) {
                   
                    var row = '<tr><td>'+user.username+'</td><td>'+ user.password +'</td><td>abc</td></tr>';
                    $("#tbl").append(row);
                });
            }

        });


        ///////////////////////////////

        //alert("danish");
        //var id = $(this).data("id");

        //var element = $(this).closest("tr");

        //$("table").append(element);
        //$.ajax({

        //    url: '/dashboard/deleteuser',
        //    type: 'Post',
        //    data: { pkid: id},
        //    success: function (result) {

        //        if (result == true) {
        //            $("table").append(element)
        //        }
        //    }

        //});
            


        });





        });

