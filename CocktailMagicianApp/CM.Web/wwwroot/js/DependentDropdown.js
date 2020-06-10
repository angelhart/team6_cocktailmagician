$(document).ready(function () {
	$("#CountryID").change(function () {
		$.get("/Magician/Addresses/GetCountryCities", { countryID: $("#CountryID").val() })
			.done(function (data) {
				$("#CityID").empty();
				$.each(data, function (index, row) {
					$("#CityID").append("<option value='" + row.id + "'>" + row.name + "</option>")
				});
			});
	});
});
