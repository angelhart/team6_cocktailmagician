$('input[type="checkbox"]').on('click', function () {

    //console.log(this);
    //console.log($(this));
    var data = {}; // container for post data
    data.id = $(this).attr('id');
    data.state = $(this).is(':checked') ? 1 : 0;

    var form = $('#__AjaxAntiForgeryForm');
    data.__RequestVerificationToken = $('input[name="__RequestVerificationToken"]', form).val();

    //console.log(token);
    //console.log(data);

    $.ajax({
        type: "POST",
        url: "/magician/cocktails/updatelisting",
        data: data
    }).done(function (data) {
        //console.log(data);
        //console.log('success');
    });
});