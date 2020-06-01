$(document).ready(function () {
    $('#cocktailsTable').DataTable({
        processing: true, //progress bar
        serverSide: true, //server side processing
        filter: true, //disable search box
        orderMulti: false, //multiple column sort
        ajax: {
            url: '/magician/ingredients/CocktailsTable',
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
                    return '<a class="btn btn-success" href="/cocktails/details/' + full.id + '">' + full.name + '</a>';
                },
            },
            {
                // Edit button
                render: function (data, type, full, meta) {
                    return '<a class="btn btn-info" href="/magician/cocktails/edit/' + full.id + '">Edit</a>';
                },
                orderable: false
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
    if (confirm("Are you sure you want to remove this ingredient from cocktail " + name + "?")) {
        Delete(id);
    } else {
        return false;
    }
}


function Delete(Id) {
    var url = "ingredients/RemoveFromCocktail/";
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();

    $.post(url,
        {
            CocktailId: Id,
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