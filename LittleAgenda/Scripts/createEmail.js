$(document).ready(function () {
	$('#btnCreateEmail').click(function () {
		CreateEmail();
	});
});

function CreateEmail() {
	var data = {
		EmailId: $('#EmailId').val(),
		ContactId: $('#ContactId').val(),
		EmailContent: $('#EmailContent').val(),
		ContactType: $('#ContactType').val()
	};
	$.ajax({
		url: '/Emails/Create/',
		type: 'POST',
		data: JSON.stringify(data),
		dataType: 'JSON',
		contentType: 'application/json',
		success: function (d) {			
			return validationsummary(d);
		},
		error: function (d) {
			validationsummary(d.statusText);
			console.log(d.statusText);
			return false;
		}
	});
}

