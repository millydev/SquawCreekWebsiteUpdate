var uri = '../api/events';
var nav;
var calendar;

$(document).ready(function () {
	nav = new DayPilot.Navigator("nav");
	nav.showMonths = 3;
	nav.skipMonths = 3;
	nav.selectMode = "week";
	nav.onTimeRangeSelected = function (args) {
		calendar.startDate = args.day;
		calendar.update();
		loadEvents();
	};
	nav.init();

	calendar = new DayPilot.Calendar("calendar");
	calendar.theme = "calendar_default";
	calendar.viewType = "Week";

	// Show an event
	calendar.onEventClick = function (args) {
		alert("Event Text: " + args.e.text() + "\nStart Date/Time:" + args.e.start().value + "\nEnd Date/Time:" + args.e.end().value);
	};

	calendar.init();

	loadEvents();

});

function loadEvents() {
	var start = calendar.visibleStart();
	var end = calendar.visibleEnd();
	$.ajax({
		type: 'POST',
		url: uri,
		data: '{action:"filter", text:"", start:"' + start + '", end:"' + end + '" }', // or JSON.stringify ({name: 'jonas'}),
		success: function (data) {
			calendar.events.list = data;
			calendar.update();
		},
		contentType: "application/json",
		dataType: 'json'
	});
}