$(document).ready(function () {

    // Validations
    $(':file').on('change', function () {
        var $file = this.files[0];
        var $files = this.files;

        if ($file.size > 1024 * 1024 * 1) {
            alert('Max upload size is 1Mb');
            //this.val('');
        }

        // get the file name, possibly with path (depends on browser)
        var $filename = $file.name;
        // Use a regular expression to trim everything before final dot
        var $extension = $filename.replace(/^.*\./, '');

        if ($extension !== 'jpg' && $extension !== 'png' && $extension !== 'gif') {
            alert('Only images allowed!');
x
        }

        // Also see .name, .type
    });

    var $form = $('#__AjaxAntiForgeryForm');
    var $token = $('input[name="__RequestVerificationToken"]', $form).val();

    $form = $('#ingrFrom');
    var $name = $('input[name="Name"]', $form).val();
    var $image = $(':file').files[0];

    var $url = "ingredients/create/";
    var $btn = $('#submitBtn');

    //btn.on('click', function () {
    //    var form = $('#ingrForm');
    //    var data = form.data;

    //    $.post(url,
    //        {
    //            //: data.,
    //            __RequestVerificationToken: token
    //        },
    //        function (data) {
    //            if (data) {
    //                oTable = $('#ingredientsTable').DataTable();
    //                oTable.draw();
    //            } else {
    //                alert("Something Went Wrong!");
    //            }
    //        });
    //});

    $('#submitBtn').on('click', function () {
        $.ajax({
            // Your server script to process the upload
            url: $url,
            type: 'POST',

            // Form data
            data: {
                Image: $image,
                Name: $name,
                __RequestVerificationToken: $token
            },

            // Tell jQuery not to process data or worry about content-type
            // You *must* include these options!
            cache: false,
            contentType: false,
            processData: false,

            // Custom XMLHttpRequest
            xhr: function () {
                var myXhr = $.ajaxSettings.xhr();
                if (myXhr.upload) {
                    // For handling the progress of the upload
                    myXhr.upload.addEventListener('progress', function (e) {
                        if (e.lengthComputable) {
                            $('progress').attr({
                                value: e.loaded,
                                max: e.total,
                            });
                        }
                    }, false);
                }
                return myXhr;
            }
        });
    });
});