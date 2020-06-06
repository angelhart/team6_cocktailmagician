$(document).ready(function () {
    $('#cocktailsTable').DataTable({
        processing: true, //progress bar
        serverSide: true, //server side processing
        filter: true, //disable search box
        orderMulti: false, //multiple column sort
        order: [2, "asc"], // override default sort column and direction
        responsive: false, // supposed to adds responsive, but needs further investigation
        ajax: {
            url: '/magician/ingredients/index',
            type: 'POST',
            dataSrc: 'data'
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
                    return '<img class="img-thumbnail" height="75px" width="75px" src="' + full.imagePath + '"/>';
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
                    return '<a href="#" class="btn btn-danger" onclick=DeleteData("' + row.id + '","' + row.name + '"); >Delete</a>';
                },
                orderable: false
            }
        ]
    });

    // -------------------------------------------
    // Identical script, but for Admin style table
    // \/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
    $('#cocktailsMagicianTable').DataTable({
        processing: true, //progress bar
        serverSide: true, //server side processing
        filter: true, //disable search box
        orderMulti: false, //multiple column sort
        order: [2, "asc"], // override default sort column and direction
        responsive: false, // supposed to adds responsive, but needs further investigation
        ajax: {
            url: '/magician/ingredients/index',
            type: 'POST',
            dataSrc: 'data'
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
                    return '<img class="img-thumbnail" height="75px" width="75px" src="' + full.imagePath + + '" href="/cocktails/details/' + full.id + '"/>';
                },
                orderable: false
            },
            {
                // Rating collumn
                name: 'AverageRating',
                data: 'AverageRating',
                orderable: true
            },
            {
                // Name collumn
                name: 'name',
                render: function (data, type, full, meta) {
                    return '<a class="btn btn-success" href="/cocktails/details/' + full.id + '">' + full.name + '</a>';
                },
                orderable: true
            },
            {
                // Ingredients collumn
                data: "Ingredients",
                render: function (data, type, row) {
                    var buttons = '';
                    for (i = 0; i < data.length; i++) {
                        buttons += '<a style="margin: 0 2px;" class="btn btn-outline-secondary" href ="/ingredients/details/' + data[i].id + '">' + data[i].name + '</a>';
                    }
                    return buttons;
                },
                orderable: false,
            },
            {
                // Unlist checkbox
                render: function (data, type, full, meta) {
                    var checked = '';
                    if (full.IsUnlisted === 'true') {
                        checked = 'checked';
                    }
                    return '<a class="btn btn-info" href="/magician/ingredients/edit/' + full.id + '">Edit</a>';
                    <input type="checkbox" class="check-box" asp-for="@item.IsUnlisted" id="@item.Id" />
                },
                orderable: false,
                visible: true // TODO: function that checks role
            },
            {
                // Edit button
                render: function (data, type, full, meta) {
                    return '<a class="btn btn-info" href="/magician/ingredients/edit/' + full.id + '">Edit</a>';
                },
                orderable: false,
                visible: true // TODO: function that checks role
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