$(document).ready(function () {
    $('#ingredientsTable').DataTable({
        processing: true, //progress bar
        serverSide: true, //server side processing
        filter: true, //disable search box
        orderMulti: false, //multiple column sort
        ajax: {
            url: '/ingredients/home/indextable',
            type: 'POST',
            dataSrc: 'data'
        },
        columns: [
            {
                // Thumbnail
                data: "ImagePath",
                render: function (url, type, full) {
                    return '<img class="img-thumbnail" height="75%" width="75%" src="' + full.ImagePath + '"/>';
                },
                orderable: false
            },
            {
                // Name collumn
                data: 'name'
            },
            {
                // Edit button
                render: function (data, type, full, meta) {
                    return '<a class="btn btn-info" href="/ingredients/home/Edit/' + full.Id + '">Edit</a>';
                },
                orderable: false
            },
            {
                // Delete button
                render: function (data, type, row) {
                    return "<a href='#' class='btn btn-danger' onclick=DeleteData('" + row.Id + "'); >Delete</a>";
                },
                orderable: false
            }
        ]
    });
});

function DeleteData(CustomerID) {
    if (confirm("Are you sure you want to delete ...?")) {
        Delete(Id);
    } else {
        return false;
    }
}


function Delete(Id) {
    var url = '@Url.Content("~/")' + "ingredients/home/Delete";

    $.post(url, { ID: Id }, function (data) {
        if (data) {
            oTable = $('#example').DataTable();
            oTable.draw();
        } else {
            alert("Something Went Wrong!");
        }
    });
}  