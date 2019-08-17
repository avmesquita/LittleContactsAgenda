$(document).ready(function () {
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
			console.log(d);
			return true;
		},
		error: function (d) {
			console.log(d);
			return false;
		}
	});
}