$(document).ready(function () {
	$('#CountryID').change(function () {
		var url = "../Controllers/BarsController/GetCountryCitiesAsync";
		var source = "#CountryID";
		$.getJSON(url, { CountryID: $(source).val() }, function (data) {
			var items = ' ';
			$('#CityID').empty();
			$.each(data, function (i, city) {
				item += "<option value='" + city.value + "'>" + city.text + "</option>";
			});
			$('#CityID').html(items);
		});
	});
});