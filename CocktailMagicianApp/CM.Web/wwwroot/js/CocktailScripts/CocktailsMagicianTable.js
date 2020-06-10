$(document).ready(function () {
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
            url: '/cocktails/index',
            type: 'POST',
            dataSrc: 'data',
            // send additional data for min/max rating
            data: function (dtParms) {
                dtParms.minRating = $('#min').val();
                dtParms.maxRating = $('#max').val();
                return dtParms;
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
                    return '<a href="/cocktails/details/ ' + full.id + '">' +
                                '<img height="100" width="auto" src="' + full.imagePath + '"/>' + //class="img-thumbnail"
                           '</a >';
                },
                orderable: false
            },
            {
                // Rating collumn
                name: 'rating',
                data: 'averageRating',
                render: function (data, type, full, meta) {
                    return full.averageRating;
                },
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
                data: "ingredients",
                render: function (data, type, row) {
                    var buttons = '';
                    for (i = 0; i < data.length; i++) {
                        buttons += '<a style="margin: 0 2px;" class="btn btn-outline-secondary" href ="/magician/ingredients/details/' + data[i].id + '">' + data[i].name + '</a>';
                    }
                    //console.log(buttons);
                    return buttons;
                },
                orderable: false,
            },
            {
                // Edit button
                render: function (data, type, full, meta) {
                    return '<a class="btn btn-info" href="/magician/cocktails/edit/' + full.id + '">Edit</a>';
                },
                orderable: false,
                visible: true
            },
            {
                // Unlist checkbox
                render: function (data, type, full, meta) {
                    var checked = '';
                    if (full.isUnlisted) {
                        checked = 'checked';
                    }
                    return '<input type="checkbox" class="check-box" onMouseDown=Unlist("' + full.id + '","' + encodeURIComponent(full.name) + '","' + !full.isUnlisted + '"); ' + checked + '></input>';
                },
                orderable: false,
                visible: true
            }
        ]
    });

});

function Unlist(id, name, state) {
    if (confirm("Change listing for " + decodeURIComponent(name) + "?")) {
        UnlistConfirmed(id, state);
    } else {
        return false;
    }
}


function UnlistConfirmed(id, state) {
    var url = "magician/cocktails/updatelisting/";
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();

    $.post(url,
        {
            id: id,
            state: state,
            __RequestVerificationToken: token
        },
        function (data) {
        if (data) {
            oTable = $('#cocktailsMagicianTable').DataTable();
            oTable.draw();
        } else {
            alert("Something Went Wrong!");
        }
    });
}  