$(document).ready(function () {
	$('#crawlersTable').DataTable({
		order: [2, "asc"], // override default sort column and direction
		processing: true, //progress bar
		serverSide: true, //server side processing
		filter: true, //disable search box
		orderMulti: false, //multiple column sort
		order: [1, "asc"], // override default sort column and direction
		ajax: {
			url: '/bars/indextable',
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
		]
	});
});

