$(document).ready(function () {
    $('#table_id').DataTable({
        serverSide: true,
        ajax: {
            url: '/ingredients/home/indextable',
            type: 'POST',
            dataSrc: ''
        }
    });
});