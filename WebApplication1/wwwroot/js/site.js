
$(document).ready(function () {
    $('#btnget').click(function () {
       GetData();
    });

    function GetData() {
        let postdata = $('#postdata').val();
     //   alert(postdata);

        //https://api.miraclecat.com/api/numbersets
        $.ajax({
            url: "https://api.miraclecat.com/api/numbersets",
            type: "post",
            data: postdata,
            dataType: 'text', //returns json when set to text - go figure, returns staggered array when set to json
            contentType: "application/json",
            success: function (result, status, xhr) {

            /*    console.log(status);*/
                console.log(result);

            },
            error: function (xhr, status, error) {
                console.log(xhr)
            }



        });


    };


});

