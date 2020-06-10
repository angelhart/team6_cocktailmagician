$(document).ready(function () {
    $('#ingredientsTable').DataTable({
        processing: true, //progress bar
        serverSide: true, //server side processing
        filter: true, //disable search box
        orderMulti: false, //multiple column sort
        order: [1, "asc"], // override default sort column and direction
        responsive: false, // supposed to adds responsive, but needs further investigation
        ajax: {
            url: '/magician/ingredients/index',
            type: 'POST',
            dataSrc: 'data',
            data: function (d) {
                d.myKey = 'role';
                // d.custom = $('#myInput').val();
                // etc
            }
        },
        drawCallback: function (settings) { 
            // Here the response
            var response = settings.json;
            //console.log(response);
            var role = response.role;
            //console.log(role);
            //return role;
        },
        oLanguage: {
            sProcessing: '<div class="spinner-border text-danger" role="status"></div>'
        },
        columns: [
            {
                // Thumbnail
                // data: "imagePath",
                render: function (url, type, full) {
                    return '<img height="100" width="auto" src="' + full.imagePath + '"/>'; //class="img-thumbnail" 
                },
                orderable: false
            },
            {
                // Name collumn
                name: 'name',
                render: function (data, type, full, meta) {
                    return '<a class="btn btn-success" href="/magician/ingredients/details/' + full.id + '">' + full.name + '</a>';
                },
                orderable: true
            },
            {
                // Edit button
                render: function (data, type, full, meta) {
                    return '<a class="btn btn-info" href="/magician/ingredients/edit/' + full.id + '">Edit</a>';
                },
                orderable: false,
                visible: true // TODO: function that checks role
            },
            {
                // Delete button
                render: function (data, type, row) {
                    //return '<a class="btn btn-danger" href="/magician/ingredients/delete/' + row.id + '">Delete</a>';
                    return '<a href="#" class="btn btn-danger" onclick=DeleteData("' + row.id + '","' + encodeURIComponent(row.name) + '"); >Delete</a>';
                },
                orderable: false
            }
        ]
    });
});

function DeleteData(id, name) {
    if (confirm("Are you sure you want to delete ingredient " + decodeURIComponent(name) + "?")) {
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