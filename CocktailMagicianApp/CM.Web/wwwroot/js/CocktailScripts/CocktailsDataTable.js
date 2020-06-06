$(document).ready(function () {
    // -------------------------------------------
    // Identical script, but for Admin style table
    // \/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
    $('#cocktailsTable').DataTable({
        processing: true, //progress bar
        serverSide: true, //server side processing
        filter: true, //disable search box
        orderMulti: false, //multiple column sort
        order: [2, "asc"], // override default sort column and direction
        responsive: false, // supposed to adds responsive, but needs further investigation
        ajax: {
            url: '/cocktails/index',
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
                    return '<a href="/cocktails/details/ ' + full.id + '"><img class="img-thumbnail" height="100" width="auto" src="' + full.imagePath + '"/>' + '</a >';
                },
                orderable: false
            },
            {
                // Rating collumn
                name: 'averageRating',
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
                        buttons += '<a style="margin: 0 2px;" class="btn btn-outline-secondary" href ="/ingredients/details/' + data[i].id + '">' + data[i].name + '</a>';
                    }
                    //console.log(buttons);
                    return buttons;
                },
                orderable: false,
            }
        ]
    });

});