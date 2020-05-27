$(document).ready(function () {
    $('#ingredientsTable').DataTable({
        serverSide: true,
        ajax: {
            url: '/ingredients/home/indextable',
            type: 'POST',
            dataSrc: ''
        }
    });
});