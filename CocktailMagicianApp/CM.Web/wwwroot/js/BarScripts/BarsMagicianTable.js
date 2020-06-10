$(document).ready(function () {
	$('#barsTable').DataTable({
		order: [2, "asc"], // override default sort column and direction
		processing: true, //progress bar
		serverSide: true, //server side processing
		filter: true, //disable search box
		orderMulti: false, //multiple column sort
		ajax: {
			url: '/bars/indextable',
			type: 'POST',
			dataSrc: 'data'
		},
		oLanguage: {
			sProcessing: '<div class="spinner-border text-danger" role="status"></div>'
		},
		columnDefs: [{
			orderable: false,
			className: 'select-checkbox',
			targets: 7
		}],
		select: {
			style: 'os',
			selector: 'td:first-child'
		},
		columns: [
			{
				// Thumbnail
				data: "imagePath",
				render: function (url, type, full) {
					return '<a href="/bars/details/ ' + full.id + '"><img height="100" width="auto" src="' + full.imagePath + '"/>'+
					'</a >';
				},
				orderable: false
			},
			{
				// Name collumn
				name: 'name',
				render: function (data, type, full, meta) {
					return '<a href="/bars/details/' + full.id + '">' + full.name + '</a>';
					//return '<a asp-action="Details" asp-route-id="' + full.id + '">' + full.name + '</a>';
				},
				orderable: false
			},
			{
				// Country collumn
				name: 'country',
				render: function (data, type, full, meta) {
					return full.country;
				},
				orderable: true
			},

			{
				// City collumn
				name: 'city',
				render: function (data, type, full, meta) {
					return full.city;
				},
				orderable: true

			},
			{
				// Address collumn
				name: 'street',
				render: function (data, type, full, meta) {
					return full.street;
				},
				orderable: true

			},
			{
				// Rating collumn
				name: 'averageRating',
				render: function (data, type, full, meta) {
					return full.averageRating;
				},
				orderable: true

			},
			{
				// Edit button
				render: function (data, type, full, meta) {
					return '<a class="btn btn-info" href="magician/bars/edit/' + full.id + '">Edit</a>';
				},
				orderable: false,
				visible: true
			},
			{
				// Delete button
				render: function (data, type, full, meta) {
					var checked = '';
					if (full.isUnlisted) {
						checked = 'checked';
					}
					return '<input type="checkbox" ' + checked + ' onMouseDown=DeleteData("' + full.id + '");> ';
					//return '<a href="#" class="btn btn-danger" onclick=DeleteData("' + row.id + '","' + row.name + '"); >Delete</a>';
				},
				orderable: false
			}
		]
	});
});

function DeleteData(id) {

	if (confirm("Are you sure you want to change listing for this bar?")) {
		Delete(id);
	}
	else {
		return false;
	}
}


function Delete(Id) {
	var url = "bars/UpdateListing/";
	var form = $('#__AjaxAntiForgeryForm');
	var token = $('input[name="__RequestVerificationToken"]', form).val();

	$.post(url,
		{
			Id: Id,
			__RequestVerificationToken: token
		},
		function (data) {
			if (data) {
				oTable = $('#barsTable').DataTable();
				oTable.draw();
			} else {
				alert("Something Went Wrong!");
			}
		});
}  