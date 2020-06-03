$(document).ready(function () {
    $('#ingredientsTable').DataTable({
        processing: true, //progress bar
        serverSide: true, //server side processing
        filter: true, //disable search box
        orderMulti: false, //multiple column sort
        ajax: {
            url: '/magician/ingredients/indextable',
            type: 'POST',
            dataSrc: 'data'
        },
        oLanguage: {
            sProcessing: '<div class="spinner-border text-danger" role="status"></div>'
        },
        columns: [
            {
                // Thumbnail
                data: "imagePath",
                render: function (url, type, full) {
                    return '<img class="img-thumbnail" height="75%" width="75%" src="' + full.imagePath + '"/>';
                },
                orderable: false
            },
            {
                // Name collumn
                name: 'name',
                render: function (data, type, full, meta) {
                    return '<a class="btn btn-success" href="/magician/ingredients/edit/' + full.id + '">' + full.name + '</a>';
                },
                orderable: false
            },
            {
                // Edit button
                render: function (data, type, full, meta) {
                    return '<a class="btn btn-info" href="/magician/ingredients/edit/' + full.id + '">Edit</a>';
                },
                orderable: false,
                visible: false
            },
            {
                // Delete button
                render: function (data, type, row) {
                    //return '<a class="btn btn-danger" href="/magician/ingredients/delete/' + row.id + '">Delete</a>';
                    return '<a href="#" class="btn btn-danger" onclick=DeleteData("' + row.id + '","' + row.name + '"); >Delete</a>';
                },
                orderable: false
            }
        ]
    });
});

function DeleteData(id, name) {
    if (confirm("Are you sure you want to delete ingredient " + name + "?")) {
        Delete(id);
    } else {
        return false;
    }
}


function Delete(Id) {
    var url = "ingredients/delete/";
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();

    $.post(url,
        {
            Id: Id,
            __RequestVerificationToken: token
        },
        function (data) {
        if (data) {
            oTable = $('#ingredientsTable').DataTable();
            oTable.draw();
        } else {
            alert("Something Went Wrong!");
        }
    });
}  