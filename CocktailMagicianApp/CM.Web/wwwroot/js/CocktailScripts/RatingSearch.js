$.fn.dataTable.ext.search.push(
    function (settings, data, dataIndex) {
        var min = parseInt($('#min').val(), 10);
        var max = parseInt($('#max').val(), 10);
        var age = parseFloat(data[1]) || 0; // use data for the rating column

        if ((isNaN(min) && isNaN(max)) ||
            (isNaN(min) && age <= max) ||
            (min <= age && isNaN(max)) ||
            (min <= age && age <= max)) {
            return true;
        }
        return false;
    }
);

$(document).ready(function () {
    var table = $('table.display').DataTable();

    // Event listener to the two range filtering inputs to redraw on input
    $('#min, #max').keyup(function () {
        table.draw();
    });
});